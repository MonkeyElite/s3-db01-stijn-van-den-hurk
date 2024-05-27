using ServerManagerCore.Models;
using ServerManagerCore.Interfaces;

namespace ServerManagerCore.Services
{
    public class SessionService(ISessionRepository sessionRepository)
    {
        private readonly ISessionRepository _sessionRepository = sessionRepository;

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
            if (session == null || session.Title == null || session.Description == null)
            {
                throw new ArgumentException("Missing session information.");
            }

            return _sessionRepository.CreateSession(session);
        }

        public Session UpdateSession(Session session)
        {
            if (session.Id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            if (session == null || session.Title == null || session.Description == null)
            {
                throw new ArgumentException("Missing session information.");
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
    }
}
