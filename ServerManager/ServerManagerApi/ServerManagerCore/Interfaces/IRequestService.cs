using ServerManagerCore.Models;

namespace ServerManagerCore.Interfaces
{
    public interface IRequestService
    {
        public List<Request> GetRequests();
    }
}
