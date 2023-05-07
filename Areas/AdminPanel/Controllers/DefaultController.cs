using Microsoft.AspNetCore.Mvc;
using NewsWebSite_ASP.Models;

namespace NewsWebSite_ASP.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class DefaultController : Controller
    {
        NewContext db;
        public DefaultController(NewContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            int newsCount = db.News.Count();
            int catgsCount = db.Cateogries.Count();
            int contactmsgs = db.ContactUs.Count();
            int teamCount = db.Teammembers.Count();

            //Anonymous Obj :
            return View(new AdminPanelStatics { newsC = newsCount, catgsC = catgsCount, contac = contactmsgs, teamC = teamCount });
        }
    }
    public class AdminPanelStatics
    {
        public int newsC { get; set; }
        public int catgsC { get; set; }
        public int contac { get; set; }
        public int teamC { get; set; }

    }
}
