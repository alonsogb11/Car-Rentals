using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProcesoRentaDevolucionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
