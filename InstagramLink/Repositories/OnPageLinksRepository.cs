using System.Collections.Generic;
using System.Linq;
using InstagramLink.Data;
using InstagramLink.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstagramLink.Repositories
{
    public class OnPageLinksRepository : IOnPageLinksRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OnPageLinksRepository> _logger;

        public OnPageLinksRepository(ApplicationDbContext context, ILogger<OnPageLinksRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<OnPageLink> GetAllLinks()
        {
            return _context.OnPageLinks.ToList();
        }

        public OnPageLink GetLinkById(int id)
        {
            return _context.OnPageLinks.Find(id);
        }

        public bool AddLink(OnPageLink link)
        {
            if (_context.OnPageLinks.Any(l => l.Id == link.Id))
            {
                _logger.LogWarning("Link already exists: {LinkId}", link.Id);
                return false;
            }
            _context.OnPageLinks.Add(link);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateLink(OnPageLink link)
        {
            var existingLink = _context.OnPageLinks.Find(link.Id);
            if (existingLink == null)
            {
                _logger.LogWarning("Link not found: {LinkId}", link.Id);
                return false;
            }
            existingLink.Title = link.Title;
            existingLink.Url = link.Url;
            _context.OnPageLinks.Update(existingLink);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteLink(int id)
        {
            var link = _context.OnPageLinks.Find(id);
            if (link == null)
            {
                _logger.LogWarning("Link not found: {LinkId}", id);
                return false;
            }
            _context.OnPageLinks.Remove(link);
            return _context.SaveChanges() > 0;
        }
    }
}
