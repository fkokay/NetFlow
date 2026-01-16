using Microsoft.AspNetCore.Mvc;

namespace NetFlow.Api.Controllers.Netsis
{
    public class InvoicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
