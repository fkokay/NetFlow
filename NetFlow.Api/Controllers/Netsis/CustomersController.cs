using Microsoft.AspNetCore.Mvc;

namespace NetFlow.Api.Controllers.Netsis
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
