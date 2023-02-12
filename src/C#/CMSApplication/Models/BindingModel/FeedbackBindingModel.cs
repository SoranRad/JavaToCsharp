using System.ComponentModel.DataAnnotations;
using CMSApplication.Services.Abstraction;

namespace CMSApplication.Models.BindingModel
{
    public class FeedbackBindingModel : IValidatableObject
    {
        [Required]
        public string   FullName        { get; set; }
        
        [Required]
        [EmailAddress]
        public string   Email           { get; set; }
        
        [Required]
        [StringLength(10, ErrorMessage = "ContactNumber length can't be more than 10.")]
        public string   ContactNumber { get; set; }

        [Required]
        public byte     Rate            { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "Commnets length can't be more than 200.")]
        public string   Commnets        { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result          = new List<ValidationResult>();
            var feedbackService = validationContext.GetService<IFeedbackService>();
            var isContactUnique = feedbackService.IsContactNumberUnique(ContactNumber).GetAwaiter().GetResult();

            if (!isContactUnique)
            {
                result.Add(new ValidationResult("The ContactNumber must be unique", new[] { nameof(ContactNumber) }));
            } 

            return result;
        }
    }
}
