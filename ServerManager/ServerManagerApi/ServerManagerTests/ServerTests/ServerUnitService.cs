using Moq;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerTests.ServerTests
{
    public class ServerServiceTests
    {
        [Fact]
        public void GetServers_ReturnsListOfServers()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);
            var expectedServers = new List<Server>();

            mockRepository.Setup(repo => repo.GetServers()).Returns(expectedServers);

            // Act
            var result = service.GetServers();

            // Assert
            Assert.Equal(expectedServers, result);
        }

        [Fact]
        public void GetServerById_WithValidId_ReturnsServer()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);
            var expectedServer = new Server("Test Server", "Test Description", "Test Game", "127.0.0.1", 8080, "password123") { Id = 1 };

            mockRepository.Setup(repo => repo.GetServerById(1)).Returns(expectedServer);

            // Act
            var result = service.GetServerById(1);

            // Assert
            Assert.Equal(expectedServer, result);
        }

        [Fact]
        public void GetServerById_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetServerById(0));
        }

        [Fact]
        public void CreateServer_WithValidServer_ReturnsCreatedServer()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);
            var server = new Server("Test Server", "Test Description", "Test Game", "127.0.0.1", 8080, "password123");

            mockRepository.Setup(repo => repo.CreateServer(server)).Returns(server);

            // Act
            var result = service.CreateServer(server);

            // Assert
            Assert.Equal(server, result);
        }

        [Fact]
        public void CreateServer_WithNullServer_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.CreateServer(null));
        }

        [Fact]
        public void UpdateServer_WithValidServer_ReturnsUpdatedServer()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);
            var server = new Server("Test Server", "Test Description", "Test Game", "127.0.0.1", 8080, "password123") { Id = 1 };

            mockRepository.Setup(repo => repo.UpdateServer(server)).Returns(server);

            // Act
            var result = service.UpdateServer(server);

            // Assert
            Assert.Equal(server, result);
        }

        [Fact]
        public void UpdateServer_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);
            var server = new Server("Test Server", "Test Description", "Test Game", "127.0.0.1", 8080, "password123") { Id = 0 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateServer(server));
        }

        [Fact]
        public void UpdateServer_WithNullServer_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateServer(null));
        }

        [Fact]
        public void DeleteServer_WithValidId_ReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);
            var serverId = 1;

            mockRepository.Setup(repo => repo.DeleteServer(serverId)).Returns(true);

            // Act
            var result = service.DeleteServer(serverId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteServer_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IServerRepository>();
            var service = new ServerService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.DeleteServer(0));
        }
    }
}
