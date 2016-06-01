using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// An abstract class which represents an operator which acts on two objects and returns an object of the same type
    /// </summary>
    internal abstract class BinaryOperator : CompiledStatement
    {
        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            Debug.Assert(parent.ChildCount > 0, "No value found for the left hand side of operator: " + token);

            // Take the previous statement and add it as a sub statement of the newly created operator - we will check for validity in the Compile function
            parent.MoveChildAtIndex(parent.ChildCount - 1, this);

            // Add this operator to the tree
            parent.Add(this);

            // Now check that there is another element after our operator that we can act on
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of operator: " + token);

            // Get the next token that appears on the right hand side of this operator
            string rhsOfOperatorToken = CelesteCompiler.PopToken();

            // Parse the next token which we will act on
            if (CelesteCompiler.CompileToken(rhsOfOperatorToken))
            {
                // Take the value that has been added to the root and add it under this operator instead
                parent.MoveChildAtIndex(parent.ChildCount - 1, this);
            }
            else
            {
                // Error message if we cannot parse the next token
                Debug.Fail("Operator invalid on token: " + token);
            }
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// If our first child is a binary operator, we swap it with this binary operator.
        /// This has the effect of evaluating this before the child operator.
        /// Useful when you wish to evaluate this operator before an assignment operator for example.
        /// </summary>
        protected void SwapWithChildBinaryOperator(CompiledStatement parent)
        {
            Debug.Assert(ChildCount > 0);
            Debug.Assert(ChildCompiledStatements[0] is AssignmentOperator);

            CompiledStatement child = ChildCompiledStatements[0];

            // We start with:
            //
            //              parent
            //             /    
            //          this      
            //         /    \
            //      child    C
            //     /     \
            //    A       B

            MoveChildAtIndex(0, parent);
            // Move the child to the parent - we now have:
            //
            //         parent
            //        /    \
            //     this      child
            //    /         /     \
            //   C         A       B

            Debug.Assert(parent.ChildCompiledStatements[parent.ChildCount - 2] == this);
            parent.MoveChildAtIndex(parent.ChildCount - 2, child);
            // Move this to be underneath the child operator - we now have:
            //
            //         parent
            //             \
            //              child
            //             /  |   \
            //            A   B    this
            //                         \
            //                           C

            Debug.Assert(child.ChildCount == 3);
            child.MoveChildAtIndex(1, this);
            // Move the B to under this - we now have:
            //
            //         parent
            //             \
            //              child
            //             /     \
            //            A       this
            //                   /    \
            //                  C      B

            Debug.Assert(ChildCount == 2);
            MoveChildAtIndex(0, this);
            // Extract and then reinsert C into this - this swaps C and B to give:
            //
            //         parent
            //             \
            //              child
            //             /     \
            //            A       this
            //                   /    \
            //                  B      C
        }

        #endregion
    }
}
