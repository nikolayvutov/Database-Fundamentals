using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class NonUnicodeAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = "Value can not be null";
            
            if (value == null)
            {
                return new ValidationResult(errorMessage);
            }

            string text = (string) value;
            string errorMsg = "Value can not contains unicode characters";
            
            
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                {
                    return new ValidationResult(errorMsg);
                }
                
                return ValidationResult.Success;
            }
        }
    }
}