﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A keyword which will be compiled out to be an and binary operator
    /// </summary>
    internal class AndKeyword : Keyword
    {
        internal static string scriptToken = "and";

        #region Virtual Functions

        /// <summary>
        /// Adds an and binary operator with the previous compiled statement and next token's compiled object to the parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            Debug.Assert(parent.ChildCount > 0, "Must have an object on the left side of the 'and' keyword");

            // Now check that there is another element after our keyword that we can use as the variable name
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of the 'and' keyword");

            // Create an and operator and compile - this will do all the work of moving statements around in the parent
            // It will also add itself to the parent tree
            AndOperator andOperator = new AndOperator();
            andOperator.Compile(parent, AndOperator.scriptToken, tokens, lines);
        }

        #endregion
    }
}
