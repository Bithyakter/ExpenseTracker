using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DateValidation
{
    public class FutureDateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = DateTime.Parse(value.ToString());

            return date.Date <= DateTime.Now.Date;
        }
    }
}
