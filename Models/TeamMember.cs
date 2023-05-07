using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebSite_ASP.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }

        [ValidateNever]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

    }
}
