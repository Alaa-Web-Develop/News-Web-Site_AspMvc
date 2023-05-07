using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NewsWebSite_ASP.Models
{
    public class Cateogry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ValidateNever]
        //Rel : Many one cat contain many news <List>
        public List<News> News { get; set; }
    }
}
