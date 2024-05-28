using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;

namespace ServerManagerCore.Services
{
    public class ServerService(IServerRepository serverRepository)
    {
        private readonly IServerRepository _serverRepository = serverRepository;

        public List<Server> GetServers()
        {
            return _serverRepository.GetServers();
        }

        public Server GetServerById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _serverRepository.GetServerById(id);
        }

        public Server CreateServer(Server server)
        {
            if (server == null || server.Title == null || server.Description == null || server.GameName == null || server.Ip == null || server.Port == 0 || server.Password == null)
            {
                throw new ArgumentException("Missing server information.");
            }

            return _serverRepository.CreateServer(server);
        }

        public Server UpdateServer(Server server)
        {
            if (server == null || server.Title == null || server.Description == null)
            {
                throw new ArgumentException("Missing server information.");
            }

            if (server.Id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }            

            return _serverRepository.UpdateServer(server);
        }

        public bool DeleteServer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _serverRepository.DeleteServer(id);
        }
    }
}
