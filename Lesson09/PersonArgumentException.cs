using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson09
{
    public class PersonArgumentException : ArgumentException
    {
        public PersonArgumentException() : base() { }

        public PersonArgumentException(string message) : base(message) { }

        public PersonArgumentException(string message, Exception inner) : base(message, inner) { }

        public PersonArgumentException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public int Value { get; }
        public PersonArgumentException(string message, int val)
        {
            Value = val;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nValue: {Value}";
        }
    }
}
