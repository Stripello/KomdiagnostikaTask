using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookCathalog.Models.Validators
{
    public class TextRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string tmp;
            try
            {
                tmp = (string)value;
            }
            catch (InvalidCastException e)
            {
                return new ValidationResult(false, e.Message);
            }
            if (string.IsNullOrEmpty(tmp))
            {
                return new ValidationResult(false, "Empty string is not valid as name.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
