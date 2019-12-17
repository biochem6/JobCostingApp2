using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace JobCostingApp
{
    class HeaderValidator : AbstractValidator<JobItemHeader>
    {
        public HeaderValidator()
        {
            RuleFor(p => p.Employee)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Select an {PropertyName}");

            RuleFor(p => p.TotalTime)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty()
               .WithMessage("Enter the Total Time")
               .Must(TypeCheck)
               .WithMessage("Input must be a Number");


            RuleFor(p => p.DateTime)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty()
               .WithMessage("Please enter a date: MM/DD/YY")
               .Must(DateChecker)
               .WithMessage("Format date as: MM/DD/YY");
        }


        private bool DateChecker(string iDate)
        {
            DateTime dt = new DateTime();
            bool isValid = DateTime.TryParseExact(
                                    iDate,
                                    "mm/dd/yy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out dt);
            return !isValid ? false : true;
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
    }
}
