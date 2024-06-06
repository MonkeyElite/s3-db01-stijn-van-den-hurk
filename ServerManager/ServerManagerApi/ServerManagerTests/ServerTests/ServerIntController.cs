using Microsoft.AspNetCore.Mvc;
using Moq;
using YourNamespace.Controllers;
using ServerManagerCore.Models;
using ServerManagerCore.Services;
using ServerManagerApi.Models.Server;
using ServerManagerCore.Interfaces;

namespace ServerManagerTests.ServerTests
{
    public class ServerControllerTests
    {
        private readonly Mock<IServerRepository> _mockServerRepository;
        private readonly ServerService _serverService;
        private readonly ServerController _controller;

        public ServerControllerTests()
        {
            _mockServerRepository = new Mock<IServerRepository>();
            _serverService = new ServerService(_mockServerRepository.Object);
            _controller = new ServerController(_serverService);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfServers()
        {
            // Arrange
            var servers = new List<Server>
            {
                new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 }
            };
            _mockServerRepository.Setup(repo => repo.GetServers()).Returns(servers);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Server>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void Get_ReturnsNotFoundResult_WhenServerNotFound()
        {
            // Arrange
            _mockServerRepository.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns((Server)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_ReturnsCreatedAtActionResult_WhenServerIsValid()
        {
            // Arrange
            var server = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1");
            var createdServer = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            _mockServerRepository.Setup(repo => repo.CreateServer(It.IsAny<Server>())).Returns(createdServer);

            // Act
            var result = _controller.Post(server);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ServerViewModel>(createdAtActionResult.Value);
            Assert.Equal(createdServer.Title, returnValue.Title);
            Assert.Equal(createdServer.Description, returnValue.Description);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = _controller.Post(new Server("", "Description", "Game", "127.0.0.1", 8080, "password"));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Put_ReturnsOkResult_WhenServerIsValid()
        {
            // Arrange
            var server = new Server("Updated Title", "Updated Description", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            var existingServer = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            _mockServerRepository.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns(existingServer);
            _mockServerRepository.Setup(repo => repo.UpdateServer(It.IsAny<Server>())).Returns(server);

            // Act
            var result = _controller.Put(1, server);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Server>(okResult.Value);
            Assert.Equal(server.Title, returnValue.Title);
            Assert.Equal(server.Description, returnValue.Description);
        }

        [Fact]
        public void Put_ReturnsNotFoundResult_WhenServerNotFound()
        {
            // Arrange
            _mockServerRepository.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns((Server)null);

            // Act
            var result = _controller.Put(1, new Server("Updated Title", "Updated Description", "Game1", "127.0.0.1", 8080, "password1"));

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOkResult_WhenServerIsDeleted()
        {
            // Arrange
            var existingServer = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            _mockServerRepository.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns(existingServer);
            _mockServerRepository.Setup(repo => repo.DeleteServer(It.IsAny<int>())).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockServerRepository.Verify(repo => repo.DeleteServer(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFoundResult_WhenServerNotFound()
        {
            // Arrange
            _mockServerRepository.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns((Server)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
