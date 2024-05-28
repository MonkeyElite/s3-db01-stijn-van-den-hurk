using ServerManagerCore.Models;
using ServerManagerCore.Interfaces;
using System.ComponentModel.DataAnnotations;

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
            if (session == null || session.Title == null || session.Description == null || session.StartTime == DateTime.MinValue || session.EndTime == DateTime.MinValue || session.ServerId <= 0)
            {
                throw new ArgumentException("Missing session information.");
            }

            if (session.StartTime > session.EndTime)
            {
                throw new ArgumentException("End Time must be greater than Start Time");
            }

            return _sessionRepository.CreateSession(session);
        }

        public Session UpdateSession(Session session)
        {
            if (session == null || session.Title == null || session.Description == null)
            {
                throw new ArgumentException("Missing session information.");
            }

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
    }
}
