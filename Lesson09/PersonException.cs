using System;

namespace Lesson09
{
    [Serializable]
    public class PersonException : Exception
    {
        public PersonException() : base() { }

        public PersonException(string message) : base(message) { }

        public PersonException(string message, Exception inner) : base(message, inner) { }

        public PersonException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
