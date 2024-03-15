using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;

namespace ServerManagerCore.Services
{
    public class RequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public List<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
        }

        public Request GetRequestById(int id)
        {
            return _requestRepository.GetRequestById(id);
        }

        public Request CreateRequest(Request request)
        {
            return _requestRepository.CreateRequest(request);
        }

        public Request UpdateRequest(int id, Request request)
        {
            return _requestRepository.UpdateRequest(id, request);
        }

        public bool DeleteRequest(int id)
        {
            return _requestRepository.DeleteRequest(id);
        }
    }
}
