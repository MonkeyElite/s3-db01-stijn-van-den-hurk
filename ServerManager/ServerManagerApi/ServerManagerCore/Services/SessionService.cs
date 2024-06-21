using ServerManagerCore.Models;
using ServerManagerCore.Interfaces;

namespace ServerManagerCore.Services
{
    public class SessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public List<Session> GetSessions()
        {
            return _sessionRepository.GetSessions();
        }

        public Session GetSessionById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _sessionRepository.GetSessionById(id);
        }

        public Session CreateSession(Session session)
        {
            ValidateSession(session);
            return _sessionRepository.CreateSession(session);
        }

        public Session UpdateSession(Session session)
        {
            ValidateSession(session);
            if (session.Id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _sessionRepository.UpdateSession(session);
        }

        public bool DeleteSession(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _sessionRepository.DeleteSession(id);
        }

        public void AddUserToSession(int sessionId, int userId)
        {
            if (sessionId <= 0 || userId <= 0)
            {
                throw new ArgumentException("Invalid session or user ID.");
            }

            _sessionRepository.AddUserToSession(sessionId, userId);
        }

        public void RemoveUserFromSession(int sessionId, int userId)
        {
            if (sessionId <= 0 || userId <= 0)
            {
                throw new ArgumentException("Invalid session or user ID.");
            }

            _sessionRepository.RemoveUserFromSession(sessionId, userId);
        }

        public List<User> GetAppliedUsers(int sessionId)
        {
            if (sessionId <= 0)
            {
                throw new ArgumentException("Invalid session ID.");
            }

            return _sessionRepository.GetAppliedUsers(sessionId);
        }

        private void ValidateSession(Session session)
        {
            if (session == null || string.IsNullOrWhiteSpace(session.Title) || string.IsNullOrWhiteSpace(session.Description) || session.StartTime == DateTime.MinValue || session.EndTime == DateTime.MinValue || session.ServerId <= 0)
            {
                throw new ArgumentException("Missing session information.");
            }

            if (session.StartTime > session.EndTime)
            {
                throw new ArgumentException("End Time must be greater than Start Time");
            }
        }
    }
}
