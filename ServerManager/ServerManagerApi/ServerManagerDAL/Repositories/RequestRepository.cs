using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerDAL.Data;

namespace ServerManagerDAL.Repositories
{
    public class RequestRepository : IRequestService
    {
        private readonly ApplicationDbContext _dbContext;

        public RequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Request> GetRequests()
        {
            List<Request> requests = new List<Request>()
            {
                new Request("Winner", "Doe ff winnen ofzo."),
                new Request("Verliezur", "Doe ff niet winnen ofzo.")
            };

            // var entityRequests = _dbContext.Requests.ToList();

            return requests;
        }
    }
}
