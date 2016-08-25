using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// A special implementation of a list to do nested scope cleanup/maintenance
    /// </summary>
    internal class ScopeManager : List<Scope>
    {
        /// <summary>
        /// Removes the inputted scope, but also any nested scopes with the inputted scope as a parent
        /// </summary>
        /// <param name="scopeToRemove"></param>
        new internal void Remove(Scope scopeToRemove)
        {
            List<Scope> scopesToRemove = new List<Scope>();
            scopesToRemove.AddRange(FindAll(x => x.ParentScope != null && x.ParentScope == scopeToRemove));
            
            foreach (Scope scope in scopesToRemove)
            {
                Remove(scope);
            }

            scopesToRemove.Clear();

            base.Remove(scopeToRemove);
        }
    }
}