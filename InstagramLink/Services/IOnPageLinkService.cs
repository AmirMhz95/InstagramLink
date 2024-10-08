using System.Collections.Generic;
using InstagramLink.Models;

namespace InstagramLink.Services
{
    public interface IOnPageLinksService
    {
        List<OnPageLink> GetAllLinks();
        OnPageLink GetLinkById(int id);
        bool AddLink(OnPageLink link);
        bool UpdateLink(OnPageLink link);
        bool DeleteLink(int id);
    }
}
