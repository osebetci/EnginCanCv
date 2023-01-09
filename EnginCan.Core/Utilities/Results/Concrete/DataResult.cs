using System;
using System.Collections.Generic;
using System.Text;
using EnginCan.Core.Utilities.Results.Abstract;

namespace EnginCan.Core.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string messages) : base(success, messages)
        {
            Data = data;
        }
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
