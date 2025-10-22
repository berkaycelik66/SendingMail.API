using System.ComponentModel.DataAnnotations;

namespace SendingMail.API.Models
{
    public class SingleMailRequest
    {
        [Required(ErrorMessage = "The mail field is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public required string Mail {  get; set; }

        [Required(ErrorMessage = "The subject field is required.")]
        public required string Subject { get; set; }

        [Required(ErrorMessage = "The message field is required.")]
        public required string Message { get; set; }
    }
}
