using System;
using System.Collections;
using System.Collections.Generic;

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
        public object Value
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
        private Reference ValueImpl { get; set; }

        /// <summary>
        /// A reference to our stored object's type
        /// </summary>
        protected Type Type { get; private set; }

        #endregion

        public CelesteObject(object value)
        {
            ValueImpl = new Reference(value);
            Type = value != null ? value.GetType() : null;
        }

        public CelesteObject(Reference value)
        {
            ValueImpl = value;
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
        public List<CelesteObject> AsTable()
        {
            return Value as List<CelesteObject>;
        }

        /// <summary>
        /// Returns whether our stored object is convertable to a number
        /// </summary>
        /// <returns></returns>
        public bool IsNumber()
        {
            return Value is int || Value is float;
        }

        /// <summary>
        /// Returns whether our stored object is convertable to a string
        /// </summary>
        /// <returns></returns>
        public bool IsString()
        {
            return Value is char || Value is string;
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
        /// Returns whether our stored object is convertable to a list of the inputted type
        /// </summary>
        /// <returns></returns>
        public bool IsList<T>()
        {
            if (Type == null || Type.IsPrimitive)
            {
                return false;
            }

            return Value is List<T>;
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
            return (Value is CelesteTable) || (Value is Dictionary<object, object>);
        }

        #endregion
    }
}
