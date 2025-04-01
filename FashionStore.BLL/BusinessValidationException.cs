using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FashionStore.BLL
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(List<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public List<ValidationFailure> Errors { get; }
    }
}
