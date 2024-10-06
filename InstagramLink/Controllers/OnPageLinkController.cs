using Microsoft.AspNetCore.Mvc;
using InstagramLink.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstagramLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnPageLinksController : ControllerBase
    {
        private static List<OnPageLink> links = new List<OnPageLink>
        {
            new OnPageLink { Id = 1, Title = "GitHub", Url = "https://github.com" },
            new OnPageLink { Id = 2, Title = "LinkedIn", Url = "https://linkedin.com" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<OnPageLink>> GetLinks()
        {
            return Ok(links);
        }

        [HttpGet("{id}")]
        public ActionResult<OnPageLink> GetLink(int id)
        {
            var link = links.FirstOrDefault(l => l.Id == id);
            if (link == null)
            {
                return NotFound(new { Message = "Link not found", LinkId = id });
            }
            return Ok(link);
        }

        [HttpPost]
        public ActionResult<OnPageLink> AddLink(OnPageLink link)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            link.Id = links.Max(l => l.Id) + 1;
            links.Add(link);
            return CreatedAtAction(nameof(GetLink), new { id = link.Id }, link);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLink(int id, OnPageLink updatedLink)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var link = links.FirstOrDefault(l => l.Id == id);
            if (link == null)
            {
                return NotFound(new { Message = "Link not found", LinkId = id });
            }
            link.Title = updatedLink.Title;
            link.Url = updatedLink.Url;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLink(int id)
        {
            var link = links.FirstOrDefault(l => l.Id == id);
            if (link == null)
            {
                return NotFound(new { Message = "Link not found", LinkId = id });
            }
            links.Remove(link);
            return NoContent();
        }
    }
}