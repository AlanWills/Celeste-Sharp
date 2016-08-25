using System;

namespace Celeste
{
    /// <summary>
    /// An attribute used to indicate that a C# function should be associated with a particular script token
    /// </summary>
    public class ScriptCommand : Attribute
    {
        #region Properties and Fields

        /// <summary>
        /// The key for our dictionary which will correspond to the token in script whih we will associate the function with
        /// </summary>
        public string Token { get; private set; }

        #endregion

        public ScriptCommand(string token)
        {
            Token = token;
        }
    }
}
