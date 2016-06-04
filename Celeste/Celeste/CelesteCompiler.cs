using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
        private static Dictionary<Func<string, bool>, Type> RegisteredValues = new Dictionary<Func<string, bool>, Type>()
        {
            { Number.IsNumber, typeof(Number) },
            { String.IsString, typeof(String) },
            { Bool.IsBool, typeof(Bool) },
            { List.IsList, typeof(List) },
            { Table.IsTable, typeof(Table) },
        };

        /// <summary>
        /// A dictionary of registered binary operators we will use when parsing our script
        /// </summary>
        private static Dictionary<string, Type> RegisteredOperators = new Dictionary<string, Type>()
        {
            { AddOperator.scriptToken, typeof(AddOperator) },
            { SubtractOperator.scriptToken, typeof(SubtractOperator) },
            { MultiplyOperator.scriptToken, typeof(MultiplyOperator) },
            { DivideOperator.scriptToken, typeof(DivideOperator) },
            { AssignmentOperator.scriptToken, typeof(AssignmentOperator) },
            { EqualityOperator.scriptToken, typeof(EqualityOperator) },
            { NonEqualityOperator.scriptToken, typeof(NonEqualityOperator) },
            { OrOperator.scriptToken, typeof(OrOperator) },
            { AndOperator.scriptToken, typeof(AndOperator) },
        };

        /// <summary>
        /// A dictionary of registered keywords we will use when parsing our script
        /// </summary>
        private static Dictionary<string, Type> RegisteredKeywords = new Dictionary<string, Type>()
        {
            { ScopedKeyword.scriptToken, typeof(ScopedKeyword) },
            { GlobalKeyword.scriptToken, typeof(GlobalKeyword) },
            { NullKeyword.scriptToken, typeof(NullKeyword) },
            { FunctionKeyword.scriptToken, typeof(FunctionKeyword) },
            { EqualsKeyword.scriptToken, typeof(EqualsKeyword) },
            { AndKeyword.scriptToken, typeof(AndKeyword) },
            { OrKeyword.scriptToken, typeof(OrKeyword) },
            { NotKeyword.scriptToken, typeof(NotKeyword) },
        };

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

        #endregion

        /// <summary>
        /// Compile the script in the inputted stream.
        /// Returns the result of the compilation and the compiled statement tree.
        /// </summary>
        /// <param name="celesteScriptStream"></param>
        public static Tuple<bool, CompiledStatement> CompileScript(StreamReader celesteScriptStream)
        {
            return CompileScript(Parse(celesteScriptStream));
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

                bool tokenResult = CompileToken(token);
                Debug.Assert(tokenResult, "Compiling token: " + token + " failed");

                result = tokenResult && result;
            }

            return result;
        }

        /// <summary>
        /// Compiles an individual token
        /// </summary>
        /// <param name="token"></param>
        public static bool CompileToken(string token)
        {
            if (CompileAsValue(RootStatement, token))
            {
                return true;
            }
            else if (CompileAsVariable(RootStatement, token))
            {
                return true;
            }
            else if (CompileAsKeyword(RootStatement, token))
            {
                return true;
            }
            else if (CompileAsBinaryOperator(RootStatement, token))
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
        /// Runs through each type to see if we have a valid value our token fits
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool CompileAsValue(CompiledStatement parent, string token)
        {
            foreach (KeyValuePair<Func<string, bool>, Type> pair in RegisteredValues)
            {
                if (pair.Key(token))
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
            if (!RegisteredOperators.ContainsKey(token))
            {
                return false;
            }

            // Create an instance of our operator
            Create<BinaryOperator>(parent, token, RegisteredOperators[token]);

            return true;
        }

        #endregion
    }
}
