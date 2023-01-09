using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(string messages) : base(true, messages)
        {
        }
        public SuccessResult() : base(true)
        {

        }
    }
}
