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
            try
            {
                var requests = _dbContext.Requests.ToList();

                return requests;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Request GetRequestById(int id)
        {
            try
            {
                Request? request = GetRequests().FirstOrDefault(r => r.Id == id);

                return request;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Request CreateRequest(Request request)
        {
            try
            {
                _dbContext.Requests.Add(request);
                _dbContext.SaveChanges();

                return GetRequestById(request.Id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public Request UpdateRequest(int id, Request request)
        {
            try
            {
                var existingRequest = _dbContext.Requests.FirstOrDefault(r => r.Id == id);
                if (existingRequest != null)
                {
                    existingRequest.Title = request.Title;
                    existingRequest.Description = request.Description;
                    _dbContext.SaveChanges();
                }

                return GetRequestById(id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public bool DeleteRequest(int id)
        {
            try
            {
                var requestToDelete = _dbContext.Requests.FirstOrDefault(r => r.Id == id);
                if (requestToDelete != null)
                {
                    _dbContext.Requests.Remove(requestToDelete);
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
