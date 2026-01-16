using Microsoft.AspNetCore.Mvc;

namespace NetFlow.Api.Controllers.Netsis
{
    public class WarehousesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
