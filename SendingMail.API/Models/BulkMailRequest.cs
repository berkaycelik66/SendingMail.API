using System.ComponentModel.DataAnnotations;

namespace SendingMail.API.Models
{
    public class BulkMailRequest
    {
        [Required(ErrorMessage = "The mails field is required.")]
        public required List<string> Mails { get; set; }

        [Required(ErrorMessage = "The subject field is required.")]
        public required string Subject { get; set; }

        [Required(ErrorMessage = "The message field is required.")]
        public required string Message { get; set; }
    }
}
