using Microsoft.AspNetCore.Mvc;

namespace NetFlow.Api.Controllers.Netsis
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
