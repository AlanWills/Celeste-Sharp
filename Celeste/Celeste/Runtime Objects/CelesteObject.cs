using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A wrapper for dealing with pushing and popping objects from our Celeste stack
    /// </summary>
    public class CelesteObject
    {
        #region Properties and Fields

        /// <summary>
        /// A reference to our stored object we create this object with
        /// </summary>
        internal object Value
        {
            get { return ValueImpl.Value; }
            set
            {
                ValueImpl.Value = value;
                Type = value != null ? value.GetType() : null;
            }
        }

        /// <summary>
        /// The reference wrapper around the value we are storing
        /// </summary>
        internal Reference ValueImpl { get; set; }

        /// <summary>
        /// A reference to our stored object's type
        /// </summary>
        protected Type Type { get; private set; }

        /// <summary>
        /// A shorthand for accessing values with the corresponding key if this object represents a table.
        /// Will compare for reference equality of keys first and then try value equality.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this [object key]
        {
            get
            {
                Debug.Assert(IsTable());
                return AsTable().GetValue(key);
            }
        }

        #endregion

        public CelesteObject(object value)
        {
            ValueImpl = new Reference(value);
            Type = value != null ? value.GetType() : null;
        }

        public CelesteObject(Reference reference)
        {
            ValueImpl = reference;
        }

        #region Utility Functions

        /// <summary>
        /// Return our stored object as the inputted type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T As<T>()
        {
            if (Value is T)
            {
                return (T)Value;
            }
            else
            {
                return (T)Convert.ChangeType(Value, typeof(T));
            }
        }

        /// <summary>
        /// Return our stored object as a list of the inputted type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> AsList<T>()
        {
            return Value as List<T>;
        }

        /// <summary>
        /// Return our stored object as a table of CelesteObjects
        /// </summary>
        /// <returns></returns>
        public Dictionary<object, object> AsTable()
        {
            return (Dictionary<object, object>)Value;
        }

        /// <summary>
        /// Return our stored object as a reference
        /// </summary>
        /// <returns></returns>
        internal Reference AsReference()
        {
            return Value as Reference;
        }

        /// <summary>
        /// Returns our stored object as a function
        /// </summary>
        /// <returns></returns>
        internal Function AsFunction()
        {
            return Value as Function;
        }

        /// <summary>
        /// Returns whether our stored object is convertable to a number
        /// </summary>
        /// <returns></returns>
        public bool IsNumber()
        {
            return Value.IsNumber();
        }

        /// <summary>
        /// Returns whether our stored object is convertable to a bool
        /// </summary>
        /// <returns></returns>
        public bool IsBool()
        {
            return Value.IsBool();
        }

        /// <summary>
        /// Returns whether our stored object is convertable to a string
        /// </summary>
        /// <returns></returns>
        public bool IsString()
        {
            return Value.IsString();
        }

        /// <summary>
        /// Returns whether our stored object is convertable to a list
        /// </summary>
        /// <returns></returns>
        public bool IsList()
        {
            if (Type == null || Type.IsPrimitive)
            {
                return false;
            }

            return Value is IList &&
                   Value.GetType().IsGenericType &&
                   Value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
        
        /// <summary>
        /// Returns whether our stored object is convertable to a table of objects
        /// </summary>
        /// <returns></returns>
        public bool IsTable()
        {
            if (Type == null || Type.IsPrimitive)
            {
                return false;
            }

            // Check to see if our stored object is a table or dictionary of object, object
            return Value is Dictionary<object, object>;
        }

        /// <summary>
        /// Returns whether our stored object is actually a reference to another object
        /// </summary>
        /// <returns></returns>
        internal bool IsReference()
        {
            return Value is Reference;
        }

        /// <summary>
        /// Returns whether our stored object is actually a reference to a Function
        /// </summary>
        /// <returns></returns>
        public bool IsFunction()
        {
            return Value is Function;
        }
            
        #endregion
    }
}
