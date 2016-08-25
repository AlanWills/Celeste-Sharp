using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Represents a string within our script
    /// </summary>
    [CompilableValue]
    internal class String : Value
    {
        #region Properties and Fields

        private static string startDelimiter = "\"";
        private static string endDelimiter = "\"";

        #endregion

        /// <summary>
        /// This must remain public for use in the Compiler
        /// </summary>
        public String() :
            base("")
        {

        }

        /// <summary>
        /// Attempts to parse the inputted token as a string value
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsString(string token)
        {
            // If our token starts with our string indicator we have a string value type
            return token.StartsWith(startDelimiter);
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            string fullString = "";

            // If our token is of the form "something" we have our full string
            if (token.EndsWith(endDelimiter))
            {
                // Remove the '"' from the start and the end
                fullString = token.Substring(1, token.Length - 2);
            }
            else
            {
                // Our full string is going to be split over many tokens - we need to keep adding tokens until we hit our second '"'
                // Remove the '"' from the first token
                fullString = token.Remove(0, 1);

                // Need more tokens left over to make our full string
                Debug.Assert(tokens.Count > 0, "No \" found to end the string: " + fullString);
                string currentToken;

                // We are going to keep removing elements, so need to check whether we still have tokens left to check
                while (tokens.Count > 0)
                {
                    currentToken = CelesteCompiler.PopToken();

                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        // If the next token we are moving through contains '"' we are done finding our full string
                        if (currentToken.Contains(endDelimiter))
                        {
                            // Add the substring without the end string character to our full string
                            int index = currentToken.IndexOf(endDelimiter);
                            if (index > 0)
                            {
                                // Concatenate the contents of this token if it was more than just the end delimiter
                                fullString += " " + currentToken.Substring(0, index);   
                            }

                            if (index < currentToken.Length - 1)
                            {
                                // The end delimiter was in the middle of the token and we have other stuff that should be popped back into the tokens list
                                // index + 1 because we don't want to add the end delimiter
                                tokens.AddFirst(currentToken.Substring(index + 1));
                            }

                            break;
                        }
                        else
                        {
                            fullString += " " + currentToken;
                        }
                    }

                    // We should only ever get here if we have tokens left
                    // If this assert triggers, it means that we have run out of tokens, but not found a close to our string
                    Debug.Assert(tokens.Count > 0, "No \" found to end the string: " + fullString);
                }
            }

            _Value = fullString;
        }

        #endregion
    }
}
