using FluentValidation.Results;
using EnginCan.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Extensions
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
