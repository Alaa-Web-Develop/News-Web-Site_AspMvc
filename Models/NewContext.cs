using Microsoft.EntityFrameworkCore;
using NewsWebSite_ASP.Data;

namespace NewsWebSite_ASP.Models
{
    public class NewContext:DbContext
    {
        //ctor
        public NewContext(DbContextOptions<NewContext> options)
                    : base(options)
        {
        }

        //Tables
        public DbSet<Cateogry> Cateogries { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<TeamMember> Teammembers { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
