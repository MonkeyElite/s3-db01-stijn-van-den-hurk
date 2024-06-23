using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerDAL.Data;

namespace ServerManagerDAL.Repositories
{
    public class ServerRepository(ApplicationDbContext dbContext) : IServerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public List<Server> GetServers()
        {
            try
            {
                var requests = _dbContext.Servers.ToList();

                return requests;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Server GetServerById(int id)
        {
            try
            {
                Server? request = GetServers().FirstOrDefault(r => r.Id == id);

                return request;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Server CreateServer(Server request)
        {
            try
            {
                _dbContext.Servers.Add(request);
                _dbContext.SaveChanges();

                return GetServerById(request.Id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public Server UpdateServer(Server request)
        {
            try
            {
                var existingServer = _dbContext.Servers.FirstOrDefault(r => r.Id == request.Id);
                if (existingServer != null)
                {
                    existingServer.Title = request.Title;
                    existingServer.Description = request.Description;
                    _dbContext.SaveChanges();
                }

                return GetServerById(request.Id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public bool DeleteServer(int id)
        {
            try
            {
                var requestToDelete = _dbContext.Servers.FirstOrDefault(r => r.Id == id);
                if (requestToDelete != null)
                {
                    _dbContext.Servers.Remove(requestToDelete);
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
