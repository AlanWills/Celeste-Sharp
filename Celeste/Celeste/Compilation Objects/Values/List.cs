using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Represents a compiled list 
    /// </summary>
    internal class List : Value
    {
        #region Properties and Fields

        private List<object> ListRef { get; set; }

        private static string startDelimiter = "[";
        private static string endDelimiter = "]";

        #endregion

        public List() :
            base(new List<object>())
        {
            ListRef = _Value as List<object>;
        }

        /// <summary>
        /// Attempts to parse the inputted token as a list value
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsList(string token)
        {
            // If our token starts with our list indicator we have a list value type
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
                Debug.Assert(tokens.Count > 0 || lines.Count > 0, "List requires a closing " + endDelimiter);

                // Create a new set of tokens if we have run out for this line
                if (tokens.Count == 0)
                {
                    CelesteCompiler.TokenizeNextLine();
                }

                string nextToken = CelesteCompiler.PopToken();
                if (nextToken.EndsWith(endDelimiter) && nextToken.Length > 1)
                {
                    // Our token has the end delimiter squashed at the end of it, so we split the token in two and add the delimiter to the tokens list
                    // This will mean we still compile this token and finish our list the next iteration round
                    nextToken = nextToken.Remove(nextToken.Length - 1);
                    tokens.AddFirst(endDelimiter);
                }

                if (nextToken == endDelimiter)
                {
                    foundClosing = true;
                }
                else if (CelesteCompiler.CompileToken(nextToken))
                {
                    // Take the value that has been created from compiling this token and add it to our list
                    // Then remove the compiled statement - we do not want objects in our list pushed onto the stack
                    CompiledStatement valueCreated = parent.ChildCompiledStatements[parent.ChildCount - 1];
                    parent.RemoveAt(parent.ChildCount - 1);

                    // The value we created MUST be a value
                    Debug.Assert(valueCreated is Value);
                    ListRef.Add((valueCreated as Value)._Value);
                }
                else
                {
                    // Error message if we cannot parse the next token
                    Debug.Fail("Error parsing token: " + token + " in list");
                }
            }
        }

        #endregion
    }
}