namespace Celeste
{
    /// <summary>
    /// A reference wrapper around a stored object to allow references to change the value
    /// </summary>
    public class Reference
    {
        /// <summary>
        /// The actual value that is being stored
        /// </summary>
        public object Value { get; set; }

        public Reference(object value)
        {
            Value = value;
        }
    }
}
