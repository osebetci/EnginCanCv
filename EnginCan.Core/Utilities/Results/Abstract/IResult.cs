using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string Messages { get; }
    }
}
