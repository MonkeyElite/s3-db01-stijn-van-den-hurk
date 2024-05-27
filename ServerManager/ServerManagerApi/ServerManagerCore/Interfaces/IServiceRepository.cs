using ServerManagerCore.Models;

namespace ServerManagerCore.Interfaces
{
    public interface IServerRepository
    {
        List<Server> GetServers();
        Server GetServerById(int id);
        Server CreateServer(Server request);
        Server UpdateServer(Server request);
        bool DeleteServer(int id);
    }
}
