using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;
using SiuntuPristatymas.Models;
using System.Diagnostics;

namespace SiuntuPristatymas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {

            _context = context;

        }


        public IActionResult Index()
        {

            if (User.Identity.Name == null)
            {
                return Redirect("Identity/Account/Login");
            }
            else
            {
                ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();


                if (user.Role == Data.Enums.RolesEnum.Admin)
                {
                    return Redirect("Parcel/Index");
                }
                else if (user.Role == Data.Enums.RolesEnum.Courier)
                {
                    return Redirect("CourierDelivery/Index");
                }

            }
            return Redirect("Identity/Account/Login");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}