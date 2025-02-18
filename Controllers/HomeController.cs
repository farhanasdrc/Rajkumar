using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rajkumar.Data;
using Rajkumar.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Rajkumar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager,ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            this.db = db;
        }

        public IActionResult Index()
        {
            var Users=db.Users.ToList();
            return View(Users);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
       
        public IActionResult EditUser(string id, IdentityUser model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.UserName = model.UserName;

            db.Users.Update(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteConfirmed(string id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
