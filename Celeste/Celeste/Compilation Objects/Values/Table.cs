using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Represents a dictionary where the key and value can be anything
    /// </summary>
    internal class Table : Value
    {
        #region Properties and Fields

        private Dictionary<object, object> DictionaryRef { get; set; }

        private static string startDelimiter = "{";
        private static string endDelimiter = "}";

        bool gotKey, gotEquality, gotValue;

        #endregion

        public Table() :
            base(new Dictionary<object, object>())
        {
            DictionaryRef = _Value as Dictionary<object, object>;
        }

        /// <summary>
        /// Attempts to parse the inputted token as a table value
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsTable(string token)
        {
            // If our token starts with our table indicator we have a table value type
            return token.StartsWith(startDelimiter);
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // We know the token will at least start with the start delimiter
            if (token.Length > 1)
            {
                // In here if the start delimiter and another token are smushed together with no space
                // Split the start delimiter and the rest of the token and add the rest of the token to the start of our tokens list
                string rest = token.Remove(0, 1);
                tokens.AddFirst(rest);
            }

            // We keep parsing until we find a closing character for our list
            bool foundClosing = false;
            while (!foundClosing)
            {
                Debug.Assert(tokens.Count > 0 || lines.Count > 0, "Table requires a closing " + endDelimiter);

                // Create a new set of tokens if we have run out for this line
                if (tokens.Count == 0)
                {
                    CelesteCompiler.TokenizeNextLine();
                }

                string nextToken = CelesteCompiler.PopToken();
                if (nextToken == AssignmentOperator.scriptToken)
                {
                    string valueToken = tokens.First.Value;
                    if (valueToken.EndsWith(endDelimiter) && valueToken.Length > 1)
                    {
                        valueToken = valueToken.Remove(valueToken.Length - 1, 1);
                        tokens.First.Value = valueToken;
                        foundClosing = true;
                    }
                }

                if (nextToken == endDelimiter)
                {
                    foundClosing = true;
                }
                else if (CelesteCompiler.CompileToken(nextToken))
                {
                    CompiledStatement compiledStatement = parent.ChildCompiledStatements[parent.ChildCount - 1];

                    if (!gotKey)
                    {
                        // The statement we created for our key MUST be a value type
                        Debug.Assert(compiledStatement is Value, "Key in table must be a valid value type");
                        gotKey = true;
                    }
                    else if (!gotEquality)
                    {
                        // The statement we created after our key MUST be the equality operator
                        Debug.Assert(compiledStatement is AssignmentOperator, "Equality expected after key");
                        gotEquality = true;
                        gotValue = compiledStatement.ChildCount == 2;
                    }
                    
                    if (gotValue)
                    {
                        // We have an equals parented under the 'parent' parameter (along with this table), with the key and value parented under that
                        // We insert the key and value into our dictionary, discard the equals statement and adjust our flags
                        Debug.Assert(parent.ChildCount >= 2);
                        Debug.Assert(parent.ChildCompiledStatements[parent.ChildCount - 1].ChildCount == 2);
                        Debug.Assert(parent.ChildCompiledStatements[parent.ChildCount - 1] is AssignmentOperator);

                        AssignmentOperator equals = parent.ChildCompiledStatements[parent.ChildCount - 1] as AssignmentOperator;
                        Debug.Assert(equals.ChildCompiledStatements[0] is Value);
                        Debug.Assert(equals.ChildCompiledStatements[1] is Value);

                        DictionaryRef.Add((equals.ChildCompiledStatements[0] as Value)._Value, (equals.ChildCompiledStatements[1] as Value)._Value);
                        parent.ChildCompiledStatements.RemoveAt(parent.ChildCount - 1);

                        gotKey = false;
                        gotEquality = false;
                        gotValue = false;
                    }
                }
                else
                {
                    // Error message if we cannot parse the next token
                    Debug.Fail("Operator invalid on token: " + token);
                }
            }

            Debug.Assert(!gotKey && !gotEquality && !gotValue, "Incomplete key value pair in table");
        }

        #endregion
    }
}
