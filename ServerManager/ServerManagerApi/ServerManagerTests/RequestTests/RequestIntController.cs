using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using ServerManagerApi.ViewModels.Request;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using System.Net;
using System.Text;

namespace ServerManagerTests.RequestTests
{
    public class RequestControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRequestRepository> _requestRepositoryMock;

        public RequestControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _requestRepositoryMock = new Mock<IRequestRepository>();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<IRequestRepository>();

                    services.AddSingleton(_requestRepositoryMock.Object);
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
        public async Task Delete_ShouldReturnNotFoundResult_WhenRequestNotFound()
        {
            // Arrange
            var client = CreateClient();
            int nonExistingId = 1;
            _requestRepositoryMock.Setup(repo => repo.GetRequestById(nonExistingId)).Returns((Request)null);

            // Act
            var response = await client.DeleteAsync($"/api/Request/{nonExistingId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WhenRequestsExist()
        {
            // Arrange
            var client = CreateClient();
            var requests = new List<Request> { new Request("Test", "Test Description") { Id = 1 } };
            _requestRepositoryMock.Setup(repo => repo.GetRequests()).Returns(requests);

            // Act
            var response = await client.GetAsync("/api/Request");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Request>>(responseString);
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFoundResult_WhenNoRequestsExist()
        {
            // Arrange
            var client = CreateClient();
            var requests = new List<Request>();
            _requestRepositoryMock.Setup(repo => repo.GetRequests()).Returns(requests);

            // Act
            var response = await client.GetAsync("/api/Request");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedAtActionResult_WhenRequestIsValid()
        {
            // Arrange
            var client = CreateClient();
            var request = new Request("Test", "Test Description");
            var createdRequest = new Request("Test", "Test Description") { Id = 1 };
            _requestRepositoryMock.Setup(repo => repo.CreateRequest(It.IsAny<Request>())).Returns(createdRequest);
            var requestViewModel = new RequestViewModel(request.Title, request.Description);

            var content = new StringContent(JsonConvert.SerializeObject(requestViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Request", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RequestViewModel>(responseString);
            result.Title.Should().Be(createdRequest.Title);
            result.Description.Should().Be(createdRequest.Description);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var client = CreateClient();
            var requestViewModel = new RequestViewModel(null, "Test Description");
            var content = new StringContent(JsonConvert.SerializeObject(requestViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Request", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_ShouldReturnOkResult_WhenRequestIsValid()
        {
            // Arrange
            var client = CreateClient();
            var request = new Request("Updated Title", "Updated Description") { Id = 1 };
            var existingRequest = new Request("Test", "Test Description") { Id = 1 };
            _requestRepositoryMock.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns(existingRequest);
            _requestRepositoryMock.Setup(repo => repo.UpdateRequest(It.IsAny<Request>())).Returns(request);

            var requestViewModel = new RequestViewModel(request.Title, request.Description);
            var content = new StringContent(JsonConvert.SerializeObject(requestViewModel), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("/api/Request/1", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Request>(responseString);
            result.Title.Should().Be(request.Title);
            result.Description.Should().Be(request.Description);
        }

        [Fact]
        public async Task Put_ShouldReturnNotFoundResult_WhenRequestNotFound()
        {
            // Arrange
            var client = CreateClient();
            var requestViewModel = new RequestViewModel("Updated Title", "Updated Description");
            var content = new StringContent(JsonConvert.SerializeObject(requestViewModel), Encoding.UTF8, "application/json");
            _requestRepositoryMock.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns((Request)null);

            // Act
            var response = await client.PutAsync("/api/Request/1", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkResult_WhenRequestIsDeleted()
        {
            // Arrange
            var client = CreateClient();
            var existingRequest = new Request("Test", "Test Description") { Id = 1 };
            _requestRepositoryMock.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns(existingRequest);
            _requestRepositoryMock.Setup(repo => repo.DeleteRequest(It.IsAny<int>())).Returns(true);

            // Act
            var response = await client.DeleteAsync("/api/Request/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
