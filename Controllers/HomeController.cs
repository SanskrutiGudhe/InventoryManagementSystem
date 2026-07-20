using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: / (Root landing page)
        public IActionResult Index()
        {
            return View();
        }
    }
}
