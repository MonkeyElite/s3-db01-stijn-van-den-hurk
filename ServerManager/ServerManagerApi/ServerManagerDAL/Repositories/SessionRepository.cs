using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerDAL.Data;

namespace ServerManagerDAL.Repositories
{
    public class SessionRepository(ApplicationDbContext dbContext) : ISessionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public List<Session> GetSessions()
        {
            try
            {
                var requests = _dbContext.Sessions.ToList();

                return requests;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Session GetSessionById(int id)
        {
            try
            {
                Session? request = GetSessions().FirstOrDefault(r => r.Id == id);

                return request;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Session CreateSession(Session request)
        {
            try
            {
                _dbContext.Sessions.Add(request);
                _dbContext.SaveChanges();

                return GetSessionById(request.Id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public Session UpdateSession(Session request)
        {
            try
            {
                var existingSession = _dbContext.Sessions.FirstOrDefault(r => r.Id == request.Id);
                if (existingSession != null)
                {
                    existingSession.Title = request.Title;
                    existingSession.Description = request.Description;
                    _dbContext.SaveChanges();
                }

                return GetSessionById(request.Id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public bool DeleteSession(int id)
        {
            try
            {
                var requestToDelete = _dbContext.Sessions.FirstOrDefault(r => r.Id == id);
                if (requestToDelete != null)
                {
                    _dbContext.Sessions.Remove(requestToDelete);
                    _dbContext.SaveChanges();
                }

                return true;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
