using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Core.Utilities.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult(string messages) : base(false, messages)
        {
        }

        public ErrorResult() : base(false)
        {

        }
    }
}
