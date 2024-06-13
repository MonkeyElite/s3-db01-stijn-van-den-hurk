using Microsoft.AspNetCore.Mvc;
using Moq;
using YourNamespace.Controllers;
using ServerManagerCore.Models;
using ServerManagerCore.Services;
using ServerManagerApi.Models.Session;
using ServerManagerCore.Interfaces;

namespace ServerManagerTests.SessionTests
{
    public class SessionControllerTests
    {
        private readonly Mock<ISessionRepository> _mockSessionRepository;
        private readonly SessionService _sessionService;
        private readonly SessionController _controller;

        public SessionControllerTests()
        {
            _mockSessionRepository = new Mock<ISessionRepository>();
            _sessionService = new SessionService(_mockSessionRepository.Object);
            _controller = new SessionController(_sessionService);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfSessions()
        {
            // Arrange
            List<Session> sessions = new List<Session>
            {
                new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 }
            };
            _mockSessionRepository.Setup(repo => repo.GetSessions()).Returns(sessions);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Session>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void Get_ReturnsNotFoundResult_WhenSessionNotFound()
        {
            // Arrange
            _mockSessionRepository.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns((Session)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_ReturnsCreatedAtActionResult_WhenSessionIsValid()
        {
            // Arrange
            Session session = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1);
            Session createdSession = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            _mockSessionRepository.Setup(repo => repo.CreateSession(It.IsAny<Session>())).Returns(createdSession);

            // Act
            var result = _controller.Post(new SessionViewModel (session));

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<SessionViewModel>(createdAtActionResult.Value);
            Assert.Equal(createdSession.Title, returnValue.Title);
            Assert.Equal(createdSession.Description, returnValue.Description);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = _controller.Post(new SessionViewModel(new Session("", "Description", DateTime.Now, DateTime.Now.AddHours(1), 1)));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Put_ReturnsOkResult_WhenSessionIsValid()
        {
            // Arrange
            Session session = new Session("Updated Title", "Updated Description", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            Session existingSession = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            _mockSessionRepository.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns(existingSession);
            _mockSessionRepository.Setup(repo => repo.UpdateSession(It.IsAny<Session>())).Returns(session);

            // Act
            var result = _controller.Put(1, new SessionViewModel( session));

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Session>(okResult.Value);
            Assert.Equal(session.Title, returnValue.Title);
            Assert.Equal(session.Description, returnValue.Description);
        }

        [Fact]
        public void Put_ReturnsNotFoundResult_WhenSessionNotFound()
        {
            // Arrange
            _mockSessionRepository.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns((Session)null);

            // Act
            var result = _controller.Put(1, new SessionViewModel(new Session("Updated Title", "Updated Description", DateTime.Now, DateTime.Now.AddHours(1), 1)));

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOkResult_WhenSessionIsDeleted()
        {
            // Arrange
            Session existingSession = new Session("Title1", "Description1", DateTime.Now, DateTime.Now.AddHours(1), 1) { Id = 1 };
            _mockSessionRepository.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns(existingSession);
            _mockSessionRepository.Setup(repo => repo.DeleteSession(It.IsAny<int>())).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockSessionRepository.Verify(repo => repo.DeleteSession(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFoundResult_WhenSessionNotFound()
        {
            // Arrange
            _mockSessionRepository.Setup(repo => repo.GetSessionById(It.IsAny<int>())).Returns((Session)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
