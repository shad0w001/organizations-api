using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Abstractions
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? value, bool IsSuccess, Error error) : base(IsSuccess, error)
        {
            _value = value;
        }
        protected internal Result(bool IsSuccess, Error error) : base(IsSuccess, error)
        {
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("A Failure object does not have an accessible value field.");
    }
}
