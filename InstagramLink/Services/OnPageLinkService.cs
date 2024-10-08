using System.Collections.Generic;
using InstagramLink.Models;
using InstagramLink.Repositories;
using Microsoft.Extensions.Logging;

namespace InstagramLink.Services
{
    public class OnPageLinksService : IOnPageLinksService
    {
        private readonly IOnPageLinksRepository _repository;
        private readonly ILogger<OnPageLinksService> _logger;

        public OnPageLinksService(IOnPageLinksRepository repository, ILogger<OnPageLinksService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public List<OnPageLink> GetAllLinks()
        {
            return _repository.GetAllLinks();
        }

        public OnPageLink GetLinkById(int id)
        {
            return _repository.GetLinkById(id);
        }

        public bool AddLink(OnPageLink link)
        {
            return _repository.AddLink(link);
        }

        public bool UpdateLink(OnPageLink link)
        {
            return _repository.UpdateLink(link);
        }

        public bool DeleteLink(int id)
        {
            return _repository.DeleteLink(id);
        }
    }
}

