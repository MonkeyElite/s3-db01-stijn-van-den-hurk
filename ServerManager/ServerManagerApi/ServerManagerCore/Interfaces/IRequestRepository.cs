using ServerManagerCore.Models;

namespace ServerManagerCore.Interfaces
{
    public interface IRequestRepository
    {
        List<Request> GetRequests();
        Request GetRequestById(int id);
        Request CreateRequest(Request request);
        Request UpdateRequest(int id, Request request);
        bool DeleteRequest(int id);
    }
}
