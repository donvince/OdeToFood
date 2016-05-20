using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class ComplexityAttribute : ValidationAttribute
    {
        public ComplexityAttribute() : base("{0} isn't complicated enough yet")
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(errorMessage);
        }
    }

    public class RestaurantReview
    {
        public int Id { get; set; }

        [Range(1, 10)]
        [Required]
        public int Rating { get; set; }
        
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "anonymous")]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }
    }
}