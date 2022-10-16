using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookCathalog.Models.Validators
{
    public class YearRules : ValidationRule
    {
        //Buisness must set maximal year above current.
        private const int yearAboveCurrent = 3; 
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int tmp;
            try
            {
                tmp = (int)value;
            }
            catch (InvalidCastException e)
            {
                return new ValidationResult(false, e.Message);
            }
            if (tmp < 0)
            {
                return new ValidationResult(false, "Can't set year B.C..");
            }
            if (tmp > DateTime.Now.Year + yearAboveCurrent )
            {
                return new ValidationResult(false, "This year is above current possible future years.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
