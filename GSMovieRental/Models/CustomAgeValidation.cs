using System;

using System.ComponentModel.DataAnnotations;

namespace GSMovieRental.Models
{
    public class CustomAgeValidation :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipTypeId == Customer.Unknown || customer.MembershipTypeId == Customer.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
            {
                return  new ValidationResult("Date of Birth is required");
            }
            int age = DateTime.Today.Year -customer.BirthDate.Value.Year;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Membership is only allowed if age > 17");
           
        }
    }
}