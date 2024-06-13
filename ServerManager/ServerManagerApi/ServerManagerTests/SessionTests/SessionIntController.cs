using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using ServerManagerApi.Models.Session;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using System.Net;
using System.Text;

namespace ServerManagerTests.SessionTests
{
    public class SessionControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ISessionRepository> _sessionRepositoryMock;

        public SessionControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _sessionRepositoryMock = new Mock<ISessionRepository>();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<ISessionRepository>();

                    services.AddSingleton(_sessionRepositoryMock.Object);
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
        public async Task Get_ReturnsOkResult_WithListOfSessions()
        {
            // Arrange
            var client = CreateClient();
            var sessions = new List<Session>
            {
                new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 }
            };
            _sessionRepositoryMock.Setup(repo => repo.GetSessions()).Returns(sessions);

            // Act
            var response = await client.GetAsync("/api/Session");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SessionViewModel>>(responseString);
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundResult_WhenSessionNotFound()
        {
            // Arrange
            var client = CreateClient();
            _sessionRepositoryMock.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns((Session)null);

            // Act
            var response = await client.GetAsync("/api/Session/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WhenSessionIsValid()
        {
            // Arrange
            var client = CreateClient();
            var session = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1);
            var createdSession = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            _sessionRepositoryMock.Setup(repo => repo.CreateSession(It.IsAny<Session>())).Returns(createdSession);

            var sessionViewModel = new SessionViewModel(session.Title, session.Description, session.StartTime, session.EndTime, session.ServerId);
            var content = new StringContent(JsonConvert.SerializeObject(sessionViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Session", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SessionViewModel>(responseString);
            result.Title.Should().Be(createdSession.Title);
            result.Description.Should().Be(createdSession.Description);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var client = CreateClient();
            var sessionViewModel = new SessionViewModel(null, "Description", DateTime.Now, DateTime.Now.AddHours(1), 1);
            var content = new StringContent(JsonConvert.SerializeObject(sessionViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Session", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WhenSessionIsValid()
        {
            // Arrange
            var client = CreateClient();
            var session = new Session("Updated Title", "Updated Description", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            var existingSession = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            _sessionRepositoryMock.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns(existingSession);
            _sessionRepositoryMock.Setup(repo => repo.UpdateSession(It.IsAny<Session>())).Returns(session);

            var sessionViewModel = new SessionViewModel(session.Title, session.Description, session.StartTime, session.EndTime, session.ServerId);
            var content = new StringContent(JsonConvert.SerializeObject(sessionViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("/api/Session/1", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SessionViewModel>(responseString);
            result.Title.Should().Be(session.Title);
            result.Description.Should().Be(session.Description);
        }

        [Fact]
        public async Task Put_ReturnsNotFoundResult_WhenSessionNotFound()
        {
            // Arrange
            var client = CreateClient();
            var sessionViewModel = new SessionViewModel("Updated Title", "Updated Description", DateTime.Now, DateTime.Now.AddHours(1), 1);
            var content = new StringContent(JsonConvert.SerializeObject(sessionViewModel), Encoding.UTF8, "application/json");
            _sessionRepositoryMock.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns((Session)null);

            // Act
            var response = await client.PutAsync("/api/Session/1", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenSessionIsDeleted()
        {
            // Arrange
            var client = CreateClient();
            var existingSession = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            _sessionRepositoryMock.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns(existingSession);
            _sessionRepositoryMock.Setup(repo => repo.DeleteSession(It.IsAny<int>())).Returns(true);

            // Act
            var response = await client.DeleteAsync("/api/Session/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenSessionNotFound()
        {
            // Arrange
            var client = CreateClient();
            _sessionRepositoryMock.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns((Session)null);

            // Act
            var response = await client.DeleteAsync("/api/Session/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
