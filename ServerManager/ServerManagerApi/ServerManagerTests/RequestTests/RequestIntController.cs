using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerManagerApi.Controllers;
using ServerManagerApi.ViewModels.Request;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerTests.RequestTests
{
    public class RequestControllerTests
    {
        private readonly Mock<IRequestRepository> _mockRequestRepository;
        private readonly RequestService _requestService;
        private readonly RequestController _controller;

        public RequestControllerTests()
        {
            _mockRequestRepository = new Mock<IRequestRepository>();
            _requestService = new RequestService(_mockRequestRepository.Object);
            _controller = new RequestController(_requestService);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfRequests()
        {
            // Arrange
            var requests = new List<Request> { new Request("Test", "Test Description") { Id = 1 } };
            _mockRequestRepository.Setup(repo => repo.GetRequests()).Returns(requests);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Request>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void Get_ReturnsNotFoundResult_WhenRequestNotFound()
        {
            // Arrange
            _mockRequestRepository.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns((Request)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_ReturnsCreatedAtActionResult_WhenRequestIsValid()
        {
            // Arrange
            var request = new Request("Test", "Test Description");
            var createdRequest = new Request("Test", "Test Description") { Id = 1 };
            _mockRequestRepository.Setup(repo => repo.CreateRequest(It.IsAny<Request>())).Returns(createdRequest);

            // Act
            var result = _controller.Post(request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<RequestViewModel>(createdAtActionResult.Value);
            Assert.Equal(createdRequest.Title, returnValue.Title);
            Assert.Equal(createdRequest.Description, returnValue.Description);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = _controller.Post(new Request("", "Test Description"));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Put_ReturnsOkResult_WhenRequestIsValid()
        {
            // Arrange
            var request = new Request("Updated Title", "Updated Description") { Id = 1 };
            var existingRequest = new Request("Test", "Test Description") { Id = 1 };
            _mockRequestRepository.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns(existingRequest);
            _mockRequestRepository.Setup(repo => repo.UpdateRequest(It.IsAny<Request>())).Returns(request);

            // Act
            var result = _controller.Put(1, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Request>(okResult.Value);
            Assert.Equal(request.Title, returnValue.Title);
            Assert.Equal(request.Description, returnValue.Description);
        }

        [Fact]
        public void Put_ReturnsNotFoundResult_WhenRequestNotFound()
        {
            // Arrange
            _mockRequestRepository.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns((Request)null);

            // Act
            var result = _controller.Put(1, new Request("Updated Title", "Updated Description"));

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOkResult_WhenRequestIsDeleted()
        {
            // Arrange
            var existingRequest = new Request("Test", "Test Description") { Id = 1 };
            _mockRequestRepository.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns(existingRequest);
            _mockRequestRepository.Setup(repo => repo.DeleteRequest(It.IsAny<int>())).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockRequestRepository.Verify(repo => repo.DeleteRequest(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFoundResult_WhenRequestNotFound()
        {
            // Arrange
            _mockRequestRepository.Setup(repo => repo.GetRequestById(It.IsAny<int>())).Returns((Request)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
