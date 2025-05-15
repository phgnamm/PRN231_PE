using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;

namespace PRN231PE_SP24_123890_DangPhuongNam_BE.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class StyleController : ODataController
    {

        StyleService _service = new();
        [EnableQuery]
        [HttpGet()]
        public IActionResult GetCLubs()
        {
            try
            {
                var _service = new StyleService();
                return Ok(_service.GetAll().AsQueryable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
