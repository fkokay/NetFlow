using Microsoft.AspNetCore.Mvc;

namespace NetFlow.Api.Controllers.Netsis
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
