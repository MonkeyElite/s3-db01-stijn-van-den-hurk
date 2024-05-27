using ServerManagerCore.Models;

namespace ServerManagerCore.Interfaces
{
    public interface ISessionRepository
    {
        List<Session> GetSessions();
        Session GetSessionById(int id);
        Session CreateSession(Session request);
        Session UpdateSession(Session request);
        bool DeleteSession(int id);
    }
}
