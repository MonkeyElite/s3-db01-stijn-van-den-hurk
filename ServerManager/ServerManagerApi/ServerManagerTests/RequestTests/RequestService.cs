using Moq;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerTests.RequestTests
{
    public class RequestServiceTests
    {
        [Fact]
        public void GetRequests_ReturnsListOfRequests()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);
            var expectedRequests = new List<Request>();

            mockRepository.Setup(repo => repo.GetRequests()).Returns(expectedRequests);

            // Act
            var result = service.GetRequests();

            // Assert
            Assert.Equal(expectedRequests, result);
        }

        [Fact]
        public void GetRequestById_WithValidId_ReturnsRequest()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);
            var expectedRequest = new Request("Test Request", "Test Description") { Id = 1 };

            mockRepository.Setup(repo => repo.GetRequestById(1)).Returns(expectedRequest);

            // Act
            var result = service.GetRequestById(1);

            // Assert
            Assert.Equal(expectedRequest, result);
        }

        [Fact]
        public void GetRequestById_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetRequestById(0));
        }

        [Fact]
        public void CreateRequest_WithValidRequest_ReturnsCreatedRequest()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);
            var request = new Request("Test Request", "Test Description");

            mockRepository.Setup(repo => repo.CreateRequest(request)).Returns(request);

            // Act
            var result = service.CreateRequest(request);

            // Assert
            Assert.Equal(request, result);
        }

        [Fact]
        public void CreateRequest_WithNullRequest_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.CreateRequest(null));
        }

        [Fact]
        public void UpdateRequest_WithValidRequest_ReturnsUpdatedRequest()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);
            var requestId = 1;
            var request = new Request("Test Request", "Test Description") { Id = requestId };

            mockRepository.Setup(repo => repo.UpdateRequest(request)).Returns(request);

            // Act
            var result = service.UpdateRequest(request);

            // Assert
            Assert.Equal(request, result);
        }

        [Fact]
        public void UpdateRequest_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);
            var request = new Request("Test Request", "Test Description") { Id = 1 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateRequest(request));
        }

        [Fact]
        public void UpdateRequest_WithNullRequest_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateRequest(null));
        }

        [Fact]
        public void DeleteRequest_WithValidId_ReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);
            var requestId = 1;

            mockRepository.Setup(repo => repo.DeleteRequest(requestId)).Returns(true);

            // Act
            var result = service.DeleteRequest(requestId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteRequest_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<IRequestRepository>();
            var service = new RequestService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.DeleteRequest(0));
        }
    }
}
