using Xunit;
using Microsoft.AspNetCore.Mvc;
using InstagramLink.Controllers;
using InstagramLink.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using InstagramLink.Services;

namespace InstagramLink.Tests
{
    public class OnPageLinksControllerTests
    {
        private OnPageLinksController _controller;
        private List<OnPageLink> _links;
        private Mock<IOnPageLinksService> _mockService;

        public OnPageLinksControllerTests()
        {
            // Initialize the mock service
            _mockService = new Mock<IOnPageLinksService>();

            // Initialize the in-memory list of links
            _links = new List<OnPageLink>
            {
                new OnPageLink { Id = 1, Title = "GitHub", Url = "https://github.com" },
                new OnPageLink { Id = 2, Title = "LinkedIn", Url = "https://linkedin.com" }
            };

            // Setup mock service methods
            _mockService.Setup(service => service.GetAllLinks()).Returns(_links);
            _mockService.Setup(service => service.GetLinkById(It.IsAny<int>())).Returns((int id) => _links.FirstOrDefault(l => l.Id == id));
            _mockService.Setup(service => service.AddLink(It.IsAny<OnPageLink>())).Callback((OnPageLink link) =>
            {
                link.Id = _links.Count + 1;
                _links.Add(link);
            }).Returns((OnPageLink link) => link);
            _mockService.Setup(service => service.UpdateLink(It.IsAny<OnPageLink>())).Callback((OnPageLink link) =>
            {
                var existingLink = _links.FirstOrDefault(l => l.Id == link.Id);
                if (existingLink != null)
                {
                    existingLink.Title = link.Title;
                    existingLink.Url = link.Url;
                }
            });
            _mockService.Setup(service => service.DeleteLink(It.IsAny<int>())).Callback((int id) =>
            {
                var link = _links.FirstOrDefault(l => l.Id == id);
                if (link != null)
                {
                    _links.Remove(link);
                }
            });

            // Initialize the controller with the mock service
            _controller = new OnPageLinksController(_mockService.Object);
        }

        [Fact]
        public void GetLinks_ReturnsAllLinks()
        {
            // Act
            var result = _controller.GetAllLinks();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<OnPageLink>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetLink_ReturnsCorrectLink()
        {
            // Act
            var result = _controller.GetLinkById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<OnPageLink>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("GitHub", returnValue.Title);
        }

        [Fact]
        public void GetLink_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.GetLinkById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddLink_AddsNewLink()
        {
            // Arrange
            var newLink = new OnPageLink { Title = "StackOverflow", Url = "https://stackoverflow.com" };

            // Act
            var result = _controller.AddLink(newLink);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<OnPageLink>(actionResult.Value);
            Assert.Equal(3, returnValue.Id); // Assuming there were initially 2 links
            Assert.Equal("StackOverflow", returnValue.Title);
        }

        [Fact]
        public void AddLink_ReturnsBadRequest_ForInvalidModel()
        {
            // Arrange
            var newLink = new OnPageLink { Title = "", Url = "invalid-url" };
            _controller.ModelState.AddModelError("Title", "Required");
            _controller.ModelState.AddModelError("Url", "Invalid URL");

            // Act
            var result = _controller.AddLink(newLink);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<SerializableError>(actionResult.Value);
            Assert.True(returnValue.ContainsKey("Title"));
            Assert.True(returnValue.ContainsKey("Url"));
        }

        [Fact]
        public void UpdateLink_UpdatesExistingLink()
        {
            // Arrange
            var updatedLink = new OnPageLink { Title = "Updated GitHub", Url = "https://github.com/updated" };

            // Act
            var result = _controller.UpdateLink(1, updatedLink);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var link = _links.FirstOrDefault(l => l.Id == 1);
            Assert.Equal("Updated GitHub", link.Title);
            Assert.Equal("https://github.com/updated", link.Url);
        }

        [Fact]
        public void UpdateLink_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            var updatedLink = new OnPageLink { Title = "Updated GitHub", Url = "https://github.com/updated" };

            // Act
            var result = _controller.UpdateLink(99, updatedLink);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteLink_DeletesExistingLink()
        {
            // Act
            var result = _controller.DeleteLink(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(_links.FirstOrDefault(l => l.Id == 1));
        }

        [Fact]
        public void DeleteLink_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.DeleteLink(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}
