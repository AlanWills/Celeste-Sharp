using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A dictionary of /object, object\ pairs.  Supports embedded lists allowing us to build up a non typeset table
    /// </summary>
    public class CelesteTable : CelesteObject
    {
        /// <summary>
        /// A reference to our StoredObject specifically stored as the Dictionary
        /// </summary>
        private Dictionary<object, object> Table { get; set; }

        public CelesteTable() :
            base(new Dictionary<object, object>())
        {
            Table = Value as Dictionary<object, object>;
        }

        #region Utility Functions

        /// <summary>
        /// Adds a new pair to this dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(object key, object value)
        {
            Debug.Assert(!Table.ContainsKey(key));
            Table.Add(key, value);
        }

        /// <summary>
        /// Obtain an element in the table indexed by the inputted key and return it casted to the inputted type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(object key)
        {
            Debug.Assert(Table[key] != null);
            if (Table[key] is T)
            {
                return (T)Table[key];
            }
            else
            {
                return (T)Convert.ChangeType(Table[key], typeof(T));
            }
        }
        
        #endregion
    }
}