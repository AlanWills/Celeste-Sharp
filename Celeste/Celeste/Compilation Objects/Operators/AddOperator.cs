using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Celeste
{
    /// <summary>
    /// The add operator
    /// </summary>
    internal class AddOperator : BinaryOperator
    {
        private class Comparer<T> : IEqualityComparer<KeyValuePair<object, object>>
        {
            public bool Equals(KeyValuePair<object, object> x, KeyValuePair<object, object> y)
            {
                return ((T)x.Key).Equals((T)y.Key);
            }

            public int GetHashCode(KeyValuePair<object, object> obj)
            {
                int val = ((T)obj.Key).GetHashCode();
                return val;
            }
        }

        private class StringComparer : IEqualityComparer<object>
        {
            public new bool Equals(object x, object y)
            {
                return (string)x == (string)y;
            }

            public int GetHashCode(object obj)
            {
                return obj.GetHashCode();
            }
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // If we have a child equality operator (it can only be in the first position, we swap it with this operator - this should act first
            // This means we have an expression of the form:    A = B + C
            if (ChildCompiledStatements[0] is EqualityOperator)
            {
                CompiledStatement equality = ChildCompiledStatements[0];

                // We start with:
                //
                //              parent
                //             /    
                //          Add      
                //         /    \
                //      Equals   C
                //     /      \
                //    A        B

                MoveChildAtIndex(0, parent);
                // Move the equals to the parent - we now have:
                //
                //         parent
                //        /    \
                //     Add      Equals
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
                //            A    B    Add
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
                //            A        Add
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
                //            A        Add
                //                    /   \
                //                   B     C
            }
        }

        /// <summary>
        /// Removes the two objects at the top of the stack and adds them together.
        /// Then, pushes the result on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we are el-bonerino-ed
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for add operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our addition result in a CelesteObject, so just push the actual value of the addition
            if (lhs.IsNumber() && rhs.IsNumber())
            {
                CelesteStack.Push(lhs.As<float>() + rhs.As<float>());
            }
            else if (lhs.IsString() && rhs.IsString())
            {
                CelesteStack.Push(lhs.As<string>() + rhs.As<string>());
            }
            else if (lhs.IsList() && rhs.IsList())
            {
                lhs.AsList().AddRange(rhs.AsList());
                CelesteStack.Push(lhs);
            }
            else if (lhs.IsTable() && rhs.IsTable())
            {
                Dictionary<object, object> lhsTable = lhs.AsTable();
                Dictionary<object, object> rhsTable = rhs.AsTable();

                // Tables can be added, but only if they are all indexed by number or string completely
                // No other way can be used because the reference type of 'object' makes value equality difficult
                // These two types of adding are the only two we can realistically support

                // Check whether our lhs table has all number keys
                if (lhsTable.Keys.Count(x => x is float) == lhsTable.Keys.Count)
                {
                    // If it does, check the rhs table too
                    if (rhsTable.Keys.Count(x => x is float) == rhsTable.Keys.Count)
                    {
                        // We now go through and see if any keys overlap in the two tables
                        if (lhsTable.Intersect(rhsTable, new Comparer<float>()).Count() == 0)
                        {
                            foreach (KeyValuePair<object, object> pair in rhsTable)
                            {
                                lhsTable.Add((float)pair.Key, pair.Value);
                            }
                        }
                        else
                        {
                            Debug.Fail("Invalid parameters to add operation.  Right hand side table has at least one key the same as the left hand side table");
                        }
                    }
                    else
                    {
                        Debug.Fail("Invalid parameters to add operation.  Right hand side table has inconsistent key types (should be numbers)");
                    }
                }
                // Check whether our lhs table has all string keys
                else if (lhsTable.Keys.Count(x => x is string) == lhsTable.Keys.Count)
                {
                    // If it does, check the rhs table too
                    if (rhsTable.Keys.Count(x => x is string) == rhsTable.Keys.Count)
                    {
                        // We now go through and see if any keys overlap in the two tables
                        if (lhsTable.Intersect(rhsTable, new Comparer<string>()).Count() == 0)
                        {
                            foreach (KeyValuePair<object, object> pair in rhsTable)
                            {
                                lhsTable.Add((string)pair.Key, pair.Value);
                            }
                        }
                        else
                        {
                            Debug.Fail("Invalid parameters to add operation.  Right hand side table has at least one key the same as the left hand side table");
                        }
                    }
                    else
                    {
                        Debug.Fail("Invalid parameters to add operation.  Right hand side table has inconsistent key types (should be strings)");
                    }
                }
                else
                {
                    Debug.Fail("Invalid parameters to add operation.  Left hand side table has inconsistent key types (should be numbers or strings)");
                }

                CelesteStack.Push(lhs);
            }
            else
            {
                Debug.Fail("Invalid parameters to add operation.");
            }
        }

        #endregion
    }
}
