using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerDAL.Data;

namespace ServerManagerDAL.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Request> GetRequests()
        {
            var requests = _dbContext.Requests.ToList();

            return requests;
        }

        public Request GetRequestById(int id)
        {
            Request request = GetRequests().FirstOrDefault(r => r.Id == id);

            return request;
        }

        public Request CreateRequest(Request request)
        {
            _dbContext.Requests.Add(request);
            _dbContext.SaveChanges();

            return GetRequestById(request.Id);
        }

        public Request UpdateRequest(int id, Request request)
        {
            var existingRequest = _dbContext.Requests.FirstOrDefault(r => r.Id == id);
            if (existingRequest != null)
            {
                existingRequest.Title = request.Title;
                existingRequest.Description = request.Description;
                _dbContext.SaveChanges();
            }

            return GetRequestById(id);
        }

        public bool DeleteRequest(int id)
        {
            var requestToDelete = _dbContext.Requests.FirstOrDefault(r => r.Id == id);
            if (requestToDelete != null)
            {
                _dbContext.Requests.Remove(requestToDelete);
                _dbContext.SaveChanges();
            }

            return true;
        }
    }
}
