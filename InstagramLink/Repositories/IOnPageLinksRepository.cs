using System.Collections.Generic;
using InstagramLink.Models;

namespace InstagramLink.Repositories
{
    public interface IOnPageLinksRepository
    {
        List<OnPageLink> GetAllLinks();
        OnPageLink GetLinkById(int id);
        bool AddLink(OnPageLink link);
        bool UpdateLink(OnPageLink link);
        bool DeleteLink(int id);
    }
}