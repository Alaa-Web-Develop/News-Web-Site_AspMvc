using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsWebSite_ASP.Models;
using System.Diagnostics;

namespace NewsWebSite_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        NewContext db;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ActivatorUtilitiesConstructor]
        public HomeController(NewContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var result = db.Cateogries.ToList();
            return View(result); //view Data from DB
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult saveContact(ContactUs model)
        {
            //check validation
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Contact", model);
            
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //Messages
        public IActionResult Messages()
        {
            return View(db.ContactUs.ToList());
        }

        public IActionResult TeamMemberIndex()
        {
            return View(db.Teammembers.ToList());
        }

        public IActionResult News(int id)
        {
            //Send Cateogry Name to News Heading
            Cateogry selectedcat = db.Cateogries.FirstOrDefault(x => x.Id == id);
            //ViewBag.catNm = selectedcat.Name;
            ViewData["catnm"] = selectedcat.Name;

            var result = db.News.Where(x => x.CateogryId == id).OrderByDescending(x => x.Date).ToList();
            return View(result);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult DeleteNew(int id)
        {
            News selected = db.News.Find(id);
            db.News.Remove(selected);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}