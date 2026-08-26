using System.Windows.Controls;

namespace SwapiFrontEndWPF.ValidationRules;

public class HeightValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
        if (value is string str && !string.IsNullOrEmpty(str))
        {
            if (double.TryParse(str, out double height) && height >= 0)
            {
                return ValidationResult.ValidResult;
            }
        }

        return new ValidationResult(false, Resources.Resources.HeightValidationError);
    }
}
