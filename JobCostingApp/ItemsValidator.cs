using FluentValidation;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCostingApp
{
    public class ItemsValidator : AbstractValidator<JobItem>
    {
        public ItemsValidator()
        {
            RuleFor(p => p.JobNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Please enter Job Number")
                .Must(TypeCheck)
                .WithMessage("Input must be a Number");


            //Detail number has a custom validator, remove non numeric characters and return List<int>
            RuleFor(p => p.DetailNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Please enter Detail Number/s");

            RuleFor(p => p.OperationCode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Please enter an Operation Code")
                .Must(OpsCodeCheck)
                .WithMessage("Input a valid Operation Code");

            RuleFor(p => p.RunTime)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Please enter Run-Time")
                .Must(TypeCheck)
                .WithMessage("Input must be a Number");

            //RuleFor(p => p.DetailNumber)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty()
            //    .Must(TypeCheck);

        }

        private bool TypeCheck(string iTotalTime)
        {
            double parsedValue;
            if (double.TryParse(iTotalTime, out parsedValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool OpsCodeCheck(string iOperationCode)
        {
            List<string> opsList = new List<string>() { "A", "D", "E", "F", "G", "K", "L", "M", "R", "S", "O" };

            if (opsList.Contains(iOperationCode))
            {
                return true;
            }
            return false;
        }
    }
}
