using System.ComponentModel.DataAnnotations;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.ValidationAttributes
{
    public class BirthdayValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue && dateValue >= DateTime.Parse("2007-01-01"))
            {
                return new ValidationResult("Value for birthday < 01-01-2007");
            }
            return ValidationResult.Success;
        }

    }
}
