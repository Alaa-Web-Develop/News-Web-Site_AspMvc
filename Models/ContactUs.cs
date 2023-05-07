using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsWebSite_ASP.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [StringLength(10)]
        [DisplayName("Name of Contact")]

        public string Name { get; set; }

        [Required]

        public string Message { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

    }
}
