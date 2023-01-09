using System;
using System.Collections.Generic;
using System.Text;
using EnginCan.Core.Utilities.Results.Abstract;

namespace EnginCan.Core.Utilities.Results.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
