using Moq;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerTests.SessionTests
{
    public class SessionServiceTests
    {
        [Fact]
        public void GetSessions_ReturnsListOfSessions()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var expectedSessions = new List<Session>();

            mockRepository.Setup(repo => repo.GetSessions()).Returns(expectedSessions);

            // Act
            var result = service.GetSessions();

            // Assert
            Assert.Equal(expectedSessions, result);
        }

        [Fact]
        public void GetSessionById_WithValidId_ReturnsSession()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var expectedSession = new Session("Test Session", "Test Description", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1) { Id = 1 };

            mockRepository.Setup(repo => repo.GetSessionById(1)).Returns(expectedSession);

            // Act
            var result = service.GetSessionById(1);

            // Assert
            Assert.Equal(expectedSession, result);
        }

        [Fact]
        public void GetSessionById_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetSessionById(0));
        }

        [Fact]
        public void CreateSession_WithValidSession_ReturnsCreatedSession()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var session = new Session("Test Session", "Test Description", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1);

            mockRepository.Setup(repo => repo.CreateSession(session)).Returns(session);

            // Act
            var result = service.CreateSession(session);

            // Assert
            Assert.Equal(session, result);
        }

        [Fact]
        public void CreateSession_WithNullSession_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.CreateSession(null));
        }

        [Fact]
        public void CreateSession_WithInvalidSession_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var session = new Session("Test Session", "Test Description", DateTime.UtcNow.AddHours(1), DateTime.UtcNow, 1);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => service.CreateSession(session));
            Assert.Contains("End Time must be greater than Start Time", ex.Message);
        }

        [Fact]
        public void UpdateSession_WithValidSession_ReturnsUpdatedSession()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var session = new Session("Test Session", "Test Description", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 1) { Id = 1 };

            mockRepository.Setup(repo => repo.UpdateSession(session)).Returns(session);

            // Act
            var result = service.UpdateSession(session);

            // Assert
            Assert.Equal(session, result);
        }

        [Fact]
        public void UpdateSession_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var session = new Session("Test Session", "Test Description", DateTime.UtcNow, DateTime.UtcNow.AddHours(1), 0);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateSession(session));
        }

        [Fact]
        public void UpdateSession_WithNullSession_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateSession(null));
        }

        [Fact]
        public void DeleteSession_WithValidId_ReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);
            var sessionId = 1;

            mockRepository.Setup(repo => repo.DeleteSession(sessionId)).Returns(true);

            // Act
            var result = service.DeleteSession(sessionId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteSession_WithInvalidId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ISessionRepository>();
            var service = new SessionService(mockRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.DeleteSession(0));
        }
    }
}
