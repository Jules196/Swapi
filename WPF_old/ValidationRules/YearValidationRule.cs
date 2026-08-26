using System.Windows.Controls;

namespace SwapiFrontEndWPF.ValidationRules;

public class YearValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
        if (value is string str && !string.IsNullOrEmpty(str))
        {
            str = str.ToLower().Replace(" ", "");
            if (str.Equals("unknown"))
            {
                return ValidationResult.ValidResult;
            }
            else if (str.Length >= 4 && str.EndsWith("bby") || str.EndsWith("aby"))
            {
                return ValidationResult.ValidResult;
            }
        }
        
        return new ValidationResult(false, Resources.Resources.YearValidationFormatError);
    }
}