using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.Models;
using Services;

namespace PRN231PE_SP24_123890_DangPhuongNam_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatercolorsPaintingController : ODataController
    {
        WatercolorsPaintingService _service = new();

        [EnableQuery]
        [HttpGet]
        [Authorize(Roles = "1,3,4")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetAll().AsQueryable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult Add([FromBody] WatercolorsPainting footballPlayer)
        {
            try
            {
                _service.Add(footballPlayer);
                return Ok(footballPlayer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult Update(string id, [FromBody] WatercolorsPainting footballPlayer)
        {
            try
            {
                _service.Update(id, footballPlayer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]    
        public IActionResult Delete(string id)  
        {
            try
            {
                _service.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

