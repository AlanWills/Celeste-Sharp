using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Celeste
{
    /// <summary>
    /// Takes in a StreamReader as input and compiles it down to a statement tree for execution
    /// </summary>
    internal static class CelesteCompiler
    {
        #region Properties and Fields

        /// <summary>
        /// A dictionary of our registered types we will use to parse hard coded values
        /// </summary>
        private static Dictionary<MethodInfo, Type> RegisteredValues = new Dictionary<MethodInfo, Type>();

        /// <summary>
        /// A dictionary of registered binary operators we will use when parsing our script
        /// </summary>
        private static Dictionary<MethodInfo, Type> RegisteredOperators = new Dictionary<MethodInfo, Type>();

        /// <summary>
        /// A dictionary of registered keywords we will use when parsing our script
        /// </summary>
        private static Dictionary<string, Type> RegisteredKeywords = new Dictionary<string, Type>();

        /// <summary>
        /// The root statement in our tree
        /// </summary>
        private static CompiledStatement RootStatement { get; set; }

        /// <summary>
        /// An list of the current line's string split tokens
        /// </summary>
        private static LinkedList<string> Tokens = new LinkedList<string>();

        /// <summary>
        /// A list of the lines left to parse
        /// </summary>
        private static LinkedList<string> Lines = new LinkedList<string>();

        /// <summary>
        /// Used to work out whether all of the Reflected objects have been set up before we run any scripts
        /// </summary>
        private static bool InitComplete = false;

        #endregion

        /// <summary>
        /// Compile the script in the inputted stream.
        /// Returns the result of the compilation and the compiled statement tree.
        /// </summary>
        /// <param name="celesteScriptStream"></param>
        public static Tuple<bool, CompiledStatement> CompileScript(StreamReader celesteScriptStream)
        {
            Init();

            return CompileScript(Parse(celesteScriptStream));
        }

        // Because our Compiler is static, we have no constructor.
        // Therefore, to use reflection to pick up the compilable objects we need to call this function whenever we try to compile a script.
        // Foreach empty dictionary above, we will loop through our Assembly and attempt to populate the dictionary with
        // the appropriate objects for it.  This is done using Attributes and the class hierarchy
        public static void Init()
        {
            // We have already set everything up so just return
            if (InitComplete)
            {
                return;
            }

            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(Value)) && type.GetCustomAttribute<CompilableValue>() != null)
                {
                    MethodInfo isTypeMethod = type.GetMethod("Is" + type.Name);
                    Debug.Assert(isTypeMethod != null);

                    RegisteredValues.Add(isTypeMethod, type);
                }
                // Don't add abstract classes - we shouldn't be able to create those anyway!
                else if ((type.IsSubclassOf(typeof(BinaryOperator)) || type.IsSubclassOf(typeof(UnaryOperator))) &&
                    !type.IsAbstract)
                {
                    MethodInfo isTypeMethod = type.GetMethod("Is" + type.Name);
                    Debug.Assert(isTypeMethod != null);

                    RegisteredOperators.Add(isTypeMethod, type);
                }
                else if (type.IsSubclassOf(typeof(Keyword)))
                {
                    FieldInfo scriptTokenField = type.GetField("scriptToken", BindingFlags.Static | BindingFlags.NonPublic);
                    Debug.Assert(scriptTokenField != null);

                    // Need to make the script token public rather than internal... will this work?
                    string scriptToken = (string)scriptTokenField.GetValue(type);
                    Debug.Assert(!string.IsNullOrEmpty(scriptToken));

                    RegisteredKeywords.Add(scriptToken, type);
                }
                else if (type.GetCustomAttribute<HasScriptCommands>() != null)
                {
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        // Create the Delegate object from the attribute information and store it as a LocalVariable
                        // For now we will do this in the GlobalScope, but this will change
                        // Then it will be picked up and run like any other function
                        if (method.GetCustomAttribute<ScriptCommand>() != null)
                        {
                            ScriptCommand scriptCommandAttr = method.GetCustomAttribute<ScriptCommand>();
                            Debug.Assert(scriptCommandAttr != null);

                            Delegate celDelegate = new Delegate(scriptCommandAttr.Token, method);
                            CelesteStack.GlobalScope.AddLocalVariable(celDelegate);
                        }
                    }
                }
            }

            InitComplete = true;
        }

        #region Parsing Functions

        /// <summary>
        /// Takes in the opened stream and parses it.
        /// Then closes the stream and .
        /// </summary>
        /// <param name="celesteScriptStream"></param>
        private static List<string> Parse(StreamReader celesteScriptStream)
        {
            List<string> parsedScript = new List<string>();
            while (!celesteScriptStream.EndOfStream)
            {
                string line = celesteScriptStream.ReadLine().Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    parsedScript.Add(line);
                }
            }

            celesteScriptStream.Close();

            return parsedScript;
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Takes the next line and tokenizes it, populating the Tokens linked list
        /// </summary>
        /// <returns></returns>
        public static void TokenizeNextLine()
        {
            Debug.Assert(Lines.Count > 0, "No lines left to tokenize");

            Tokens.Clear();
            foreach (string splitString in Lines.First.Value.Split(' '))
            {
                Tokens.AddLast(splitString);
            }
            
            Lines.RemoveFirst();
        }

        /// <summary>
        /// Extracts and removes from the Tokens list the token at the front
        /// </summary>
        /// <returns></returns>
        public static string PopToken()
        {
            Debug.Assert(Tokens.Count > 0, "No tokens left to pop");
            string token = Tokens.First.Value;
            Tokens.RemoveFirst();

            return token;
        }

        /// <summary>
        /// Create an instance of the inputted compiled statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        private static void Create<T>(CompiledStatement parent, string token) where T : CompiledStatement
        {
            // Create an instance of our keyword
            T statement = (T)Activator.CreateInstance(typeof(T));
            statement.Compile(parent, token, Tokens, Lines);
        }

        /// <summary>
        /// Create an instance of the inputted compiled statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="type"></param>
        private static void Create<T>(CompiledStatement parent, string token, Type type) where T : CompiledStatement
        {
            // Create an instance of our keyword
            T statement = (T)Activator.CreateInstance(type);
            statement.Compile(parent, token, Tokens, Lines);
        }

        #endregion

        #region Compiling Functions

        /// <summary>
        /// Compiles the parsed file down into a statement tree
        /// </summary>
        /// <param name="parsedFile"></param>
        private static Tuple<bool, CompiledStatement> CompileScript(List<string> parsedFile)
        {
            RootStatement = new CompiledStatement();
            Tokens = new LinkedList<string>();
            Lines = new LinkedList<string>(parsedFile);

            while (Lines.Count > 0)
            {
                // As soon as we detect an error, we stop parsing the script and return a tuple with a false flag and a null compile tree
                if (!CompileLine())
                {
                    Debug.Fail("Error compiling script");
                    return new Tuple<bool, CompiledStatement>(false, null);
                }
            }

            return new Tuple<bool, CompiledStatement>(true, RootStatement);
        }

        /// <summary>
        /// Compiles the inputted line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool CompileLine()
        {
            // Tokenize the line
            TokenizeNextLine();

            bool result = true;

            while (Tokens.Count > 0)
            {
                string token = PopToken();

                bool tokenResult = CompileToken(token, RootStatement);
                Debug.Assert(tokenResult, "Compiling token: " + token + " failed");

                result = tokenResult && result;
            }

            return result;
        }

        /// <summary>
        /// Compiles an individual token
        /// </summary>
        /// <param name="token"></param>
        public static bool CompileToken(string token, CompiledStatement parent)
        {
            if (Delimiter.IsDelimiter(token))
            {
                // Don't stop if we find a delimiter, merely inline the appropriate token
                Create<Delimiter>(parent, token);
                token = PopToken();
            }

            if (CompileAsValue(parent, token))
            {
                return true;
            }
            else if (CompileAsVariable(parent, token))
            {
                return true;
            }
            else if (CompileAsFunction(parent, token))
            {
                return true;
            }
            else if (CompileAsKeyword(parent, token))
            {
                return true;
            }
            else if (CompileAsBinaryOperator(parent, token))
            {
                return true;
            }
            else
            {
                Debug.Fail("Unrecognised token");
                return false;
            }
        }

        /// <summary>
        /// Runs through each keyword to see if we have a valid keyword our token fits
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool CompileAsKeyword(CompiledStatement parent, string token)
        {
            if (!RegisteredKeywords.ContainsKey(token))
            {
                return false;
            }

            // Create an instance of our keyword
            Create<Keyword>(parent, token, RegisteredKeywords[token]);

            return true;
        }

        /// <summary>
        /// See if we have referenced a local variable that exists and if so, create a reference object to it in our expression tree.
        /// This will push a reference onto the stack
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool CompileAsVariable(CompiledStatement parent, string token)
        {
            if (!CelesteStack.CurrentScope.VariableExists(token, ScopeSearchOption.kUpwardsRecursive))
            {
                return false;
            }

            // Call compile on our variable we created - this will push a reference to it onto the stack
            CelesteStack.CurrentScope.GetLocalVariable(token).Compile(parent, token, Tokens, Lines);

            return true;
        }

        /// <summary>
        /// See if this inputted token is actually a function call.
        /// We cannot use CompileAsVariable, because to mark it as a call we use brackets and pass parameters.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool CompileAsFunction(CompiledStatement parent, string token)
        {
            int parameterStartDelimiterIndex = token.IndexOf(FunctionKeyword.parameterStartDelimiter);
            if (parameterStartDelimiterIndex > 0)
            {
                string functionName = token.Substring(0, parameterStartDelimiterIndex);

                if (CelesteStack.CurrentScope.VariableExists(functionName, ScopeSearchOption.kUpwardsRecursive))
                {
                    // Call compile - this will push a reference onto the stack and add references to the variables we will use as arguments
                    CelesteStack.CurrentScope.GetLocalVariable(functionName).Compile(parent, token, Tokens, Lines);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Runs through each type to see if we have a valid value our token fits
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool CompileAsValue(CompiledStatement parent, string token)
        {
            foreach (KeyValuePair<MethodInfo, Type> pair in RegisteredValues)
            {
                // Invoke the method we have picked up from reflection for seeing if this token is a valid value
                if ((bool)pair.Key.Invoke(null, new object[] { token }))
                {
                    Create<Value>(parent, token, pair.Value);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Attempt to parse the inputted token as an operator.
        /// Will rearrange the values to act on underneath the operator in our compiled tree.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="statements"></param>
        /// <returns></returns>
        private static bool CompileAsBinaryOperator(CompiledStatement parent, string token)
        {
            foreach (KeyValuePair<MethodInfo, Type> pair in RegisteredOperators)
            {
                if ((bool)pair.Key.Invoke(null, new object[] { token }))
                {
                    Create<BinaryOperator>(parent, token, pair.Value);

                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
