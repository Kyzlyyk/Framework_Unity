using Kyzlyk.Core.Builders;
using System;
using System.Runtime.Serialization;

namespace Kyzlyk.Core.Exceptions
{
    [Serializable]
    public class ComparisonException : Exception
    {
        private ComparisonException() { }
        private ComparisonException(string message) : base(message) { }
        private ComparisonException(string message, Exception inner) : base(message, inner) { }
        protected ComparisonException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }

        public static ComparisonException Throw(object parameter1, object parameter2, ComparisonType comparisonType)
        {
            LabelBuilder labelBuilder = new(comparisonType.ToString()); 

            string comparisonString = labelBuilder
                .Separate()
                .ToLower()
                .ToString();
            
            throw new ComparisonException($"'{parameter1}' must be {comparisonString} to '{parameter2}'!");
        }
    }

    public enum ComparisonType
    {
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Equals,
    }
}
