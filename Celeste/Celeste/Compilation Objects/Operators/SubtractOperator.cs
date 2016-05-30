using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Celeste
{
    /// <summary>
    /// The subtract operator
    /// </summary>
    internal class SubtractOperator : BinaryOperator
    {
        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // If we have a child equality operator (it can only be in the first position, we swap it with this operator - this should act first
            // This means we have an expression of the form:    A = B - C
            if (ChildCompiledStatements[0] is EqualityOperator)
            {
                CompiledStatement equality = ChildCompiledStatements[0];

                // We start with:
                //
                //              parent
                //             /    
                //        Subtract      
                //         /    \
                //      Equals   C
                //     /      \
                //    A        B

                MoveChildAtIndex(0, parent);
                // Move the equals to the parent - we now have:
                //
                //         parent
                //        /    \
                //  Subtract    Equals
                //    /        /      \
                //   C        A        B

                Debug.Assert(parent.ChildCompiledStatements[parent.ChildCount - 2] == this);
                parent.MoveChildAtIndex(parent.ChildCount - 2, equality);
                // Move the add to the equality operator - we now have:
                //
                //         parent
                //             \
                //              Equals
                //             /   |   \
                //            A    B   Subtract
                //                         \
                //                          C

                Debug.Assert(equality.ChildCount == 3);
                equality.MoveChildAtIndex(1, this);
                // Move the B to under this - we now have:
                //
                //         parent
                //             \
                //              Equals
                //             /      \
                //            A      Subtract
                //                    /   \
                //                   C     B

                Debug.Assert(ChildCount == 2);
                MoveChildAtIndex(0, this);
                // Extract and then reinsert C into this - this swaps C and B to give:
                //
                //         parent
                //             \
                //              Equals
                //             /      \
                //            A      Subtract
                //                    /   \
                //                   B     C
            }
        }

        /// <summary>
        /// Removes the two objects at the top of the stack and subtracts the top most object on the stack from the other.
        /// Then, pushes the result on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we are el-bonerino-ed
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for subtract operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our subtraction result in a CelesteObject, so just push the actual value of the subtraction
            if (lhs.IsNumber() && rhs.IsNumber())
            {
                CelesteStack.Push(lhs.As<float>() - rhs.As<float>());
            }
            else if (lhs.IsString() && rhs.IsString())
            {
                string lhsAsString = lhs.As<string>();
                string rhsAsString = rhs.As<string>();

                if (lhsAsString.Contains(rhsAsString))
                {
                    // Push the lhs string having removed the rhs string
                    CelesteStack.Push(lhsAsString.Remove(lhsAsString.IndexOf(rhsAsString), rhsAsString.Length));
                }
                else
                {
                    // If our lhs does not contain our rhs, we just push the original unaltered lhs string
                    CelesteStack.Push(lhsAsString);
                }
            }
            else if (lhs.IsList() && rhs.IsList())
            {
                // The subtract operator for lists removes all elements in the first list who are equal to an element in the second, either by reference or value
                List<object> lhsList = lhs.AsList();
                List<object> rhsList = rhs.AsList();

                // Remove ALL occurrences in the list of any object in the rhs list - otherwise there is non-deterministic behaviour in which instance to remove
                lhsList.RemoveAll(x => rhsList.Exists(y => y.Equals(x) || y.ValueEquals(x)));
                CelesteStack.Push(lhs);
            }
            else if (lhs.IsTable() && rhs.IsList())
            {
                // Subtraction of tables does not make sense
                // What does make sense, is subtracting elements in a table using a list of keys
                // Subtracting a list from a table will remove any elements in the table with a matching key as an element in our list

                Dictionary<object, object> lhsTable = lhs.AsTable();
                List<object> rhsList = rhs.AsList();

                foreach (object obj in rhsList)
                {
                    if (lhsTable.Contains(new KeyValuePair<object, object>(obj, null), new TableKeyComparer()))
                    {
                        lhsTable.Remove(lhsTable.First(x => x.Key.Equals(obj) || x.Key.ValueEquals(obj)));
                    }
                }
                
                CelesteStack.Push(lhs);
            }
            else
            {
                Debug.Fail("Invalid parameters to subtract operation.");
            }
        }

        #endregion
    }
}
