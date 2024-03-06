using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;

namespace ServerManagerCore.Services
{
    public class RequestService
    {
        private readonly IRequestService _requestService;

        public RequestService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public List<Request> GetRequests()
        {
            List<Request> requests = _requestService.GetRequests();

            return requests;
        }
    }
}
