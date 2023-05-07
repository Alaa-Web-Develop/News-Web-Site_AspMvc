using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebSite_ASP.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("News Date")]
        public DateTime Date { get; set; }

        [ValidateNever]
        public string Image { get; set; }
        public string Topic { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }  

        //Rel : one new in one cat , FK

        [ForeignKey("Cateogries")]
        public int CateogryId { get; set; }

        [ValidateNever]
        public Cateogry Cateogry { get; set; }
        //Reach to cat in news  in many :
        //void print()
        //{
        //    News n = new News();
        //    n.Cateogry.Name 
        //}
    }
}
