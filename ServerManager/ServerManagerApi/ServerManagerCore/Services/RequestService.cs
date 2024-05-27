using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;

namespace ServerManagerCore.Services
{
    public class RequestService(IRequestRepository requestRepository)
    {
        private readonly IRequestRepository _requestRepository = requestRepository;

        public List<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
        }

        public Request GetRequestById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _requestRepository.GetRequestById(id);
        }

        public Request CreateRequest(Request request)
        {
            if (request == null || request.Title == null || request.Description == null)
            {
                throw new ArgumentException("Missing request information.");
            }

            return _requestRepository.CreateRequest(request);
        }

        public Request UpdateRequest(Request request)
        {
            if (request.Id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            if (request == null || request.Title == null || request.Description == null)
            {
                throw new ArgumentException("Missing request information.");
            }

            return _requestRepository.UpdateRequest(request);
        }

        public bool DeleteRequest(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id.");
            }

            return _requestRepository.DeleteRequest(id);
        }
    }
}
