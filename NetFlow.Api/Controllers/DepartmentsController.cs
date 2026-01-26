using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Firms;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Departments;
using NetFlow.ReadModel.Firms;

namespace NetFlow.Api.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentReadService _read;

        public DepartmentsController(DepartmentReadService read)
        {
            _read = read;
        }
        [HttpGet]
        public async Task<IActionResult> PadgeList([FromQuery] PagedRequest pagedRequest) => Ok(await _read.ListAsync(pagedRequest));


        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> List() => Ok(await _read.GetDepartmentListAsync());
    }
}
