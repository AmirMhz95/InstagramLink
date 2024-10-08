using Microsoft.AspNetCore.Mvc;
using InstagramLink.Models;
using InstagramLink.Services;

namespace InstagramLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnPageLinksController : ControllerBase
    {
        private readonly IOnPageLinksService _service;

        public OnPageLinksController(IOnPageLinksService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAllLinks()
        {
            var links = _service.GetAllLinks();
            return Ok(links);
        }

        [HttpGet("{id}")]
        public IActionResult GetLinkById(int id)
        {
            var link = _service.GetLinkById(id);
            if (link == null)
            {
                return NotFound("Link not found");
            }
            return Ok(link);
        }

        [HttpPost]
        public IActionResult AddLink([FromBody] OnPageLink link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.AddLink(link);
            if (result)
            {
                return Ok("Link added successfully");
            }
            return BadRequest("Link addition failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLink(int id, [FromBody] OnPageLink link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            link.Id = id;
            var result = _service.UpdateLink(link);
            if (result)
            {
                return Ok("Link updated successfully");
            }
            return BadRequest("Link update failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLink(int id)
        {
            var result = _service.DeleteLink(id);
            if (result)
            {
                return Ok("Link deleted successfully");
            }
            return BadRequest("Link deletion failed");
        }
    }
}
