using System;

namespace Minibank.Core
{
    public class UserFriendlyException : Exception
    {
        public string Value { get; }
        public UserFriendlyException(string message, string value) : base(message)
        {
            Value = value;
        }

        public UserFriendlyException(string message) : base(message)
        {

        }
    }
}