using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using ServerManagerApi.Models.Server;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using System.Net;
using System.Text;

namespace ServerManagerTests.ServerTests
{
    public class ServerControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IServerRepository> _serverRepositoryMock;

        public ServerControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _serverRepositoryMock = new Mock<IServerRepository>();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<IServerRepository>();

                    services.AddSingleton(_serverRepositoryMock.Object);
                });
            });
        }

        private HttpClient CreateClient()
        {
            return _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WithListOfServers()
        {
            // Arrange
            var client = CreateClient();
            var servers = new List<Server>
            {
                new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 }
            };
            _serverRepositoryMock.Setup(repo => repo.GetServers()).Returns(servers);

            // Act
            var response = await client.GetAsync("/api/Server");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ServerViewModel>>(responseString);
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFoundResult_WhenServerNotFound()
        {
            // Arrange
            var client = CreateClient();
            int nonExistingId = 1;
            _serverRepositoryMock.Setup(repo => repo.GetServerById(nonExistingId)).Returns((Server)null);

            // Act
            var response = await client.GetAsync($"/api/Server/{nonExistingId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedAtActionResult_WhenServerIsValid()
        {
            // Arrange
            var client = CreateClient();
            var server = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1");
            var createdServer = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            _serverRepositoryMock.Setup(repo => repo.CreateServer(It.IsAny<Server>())).Returns(createdServer);
            var serverViewModel = new ServerViewModel(server.Title, server.Description, server.GameName, server.Ip, server.Port, server.Password);

            var content = new StringContent(JsonConvert.SerializeObject(serverViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Server", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServerViewModel>(responseString);
            result.Title.Should().Be(createdServer.Title);
            result.Description.Should().Be(createdServer.Description);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var client = CreateClient();
            var serverViewModel = new ServerViewModel(null, "Description", "Game", "127.0.0.1", 8080, "password");
            var content = new StringContent(JsonConvert.SerializeObject(serverViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Server", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_ShouldReturnOkResult_WhenServerIsValid()
        {
            // Arrange
            var client = CreateClient();
            var server = new Server("Updated Title", "Updated Description", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            var existingServer = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            _serverRepositoryMock.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns(existingServer);
            _serverRepositoryMock.Setup(repo => repo.UpdateServer(It.IsAny<Server>())).Returns(server);

            var serverViewModel = new ServerViewModel(server.Title, server.Description, server.GameName, server.Ip, server.Port, server.Password);
            var content = new StringContent(JsonConvert.SerializeObject(serverViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("/api/Server/1", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServerViewModel>(responseString);
            result.Title.Should().Be(server.Title);
            result.Description.Should().Be(server.Description);
        }

        [Fact]
        public async Task Put_ShouldReturnNotFoundResult_WhenServerNotFound()
        {
            // Arrange
            var client = CreateClient();
            var serverViewModel = new ServerViewModel("Updated Title", "Updated Description", "Game1", "127.0.0.1", 8080, "password1");
            var content = new StringContent(JsonConvert.SerializeObject(serverViewModel), Encoding.UTF8, "application/json");
            _serverRepositoryMock.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns((Server)null);

            // Act
            var response = await client.PutAsync("/api/Server/1", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkResult_WhenServerIsDeleted()
        {
            // Arrange
            var client = CreateClient();
            var existingServer = new Server("Title1", "Description1", "Game1", "127.0.0.1", 8080, "password1") { Id = 1 };
            _serverRepositoryMock.Setup(repo => repo.GetServerById(It.IsAny<int>())).Returns(existingServer);
            _serverRepositoryMock.Setup(repo => repo.DeleteServer(It.IsAny<int>())).Returns(true);

            // Act
            var response = await client.DeleteAsync("/api/Server/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundResult_WhenServerNotFound()
        {
            // Arrange
            var client = CreateClient();
            int nonExistingId = 1;
            _serverRepositoryMock.Setup(repo => repo.GetServerById(nonExistingId)).Returns((Server)null);

            // Act
            var response = await client.DeleteAsync($"/api/Server/{nonExistingId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
