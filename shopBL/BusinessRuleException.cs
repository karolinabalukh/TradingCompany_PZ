using System;
using System.Collections.Generic;

namespace shopBL
{
    public class BusinessRuleException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public BusinessRuleException(string error) : this(new List<string> { error })
        {
        }

        public BusinessRuleException(IReadOnlyList<string> errors)
            : base(string.Join(Environment.NewLine, errors))
        {
            Errors = errors;
        }
    }
}