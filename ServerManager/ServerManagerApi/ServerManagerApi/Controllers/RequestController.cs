using Microsoft.AspNetCore.Mvc;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public ActionResult<List<Request>> Get()
        {
            var requests = _requestService.GetRequests();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public ActionResult<Request> Get(int id)
        {
            var request = _requestService.GetRequestById(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        [HttpPost]
        public ActionResult<Request> Post([FromBody] Request request)
        {
            _requestService.CreateRequest(request);
            return CreatedAtAction(nameof(Get), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Request request)
        {
            var existingRequest = _requestService.GetRequestById(id);
            if (existingRequest == null)
            {
                return NotFound();
            }
            var updatedRequest = _requestService.UpdateRequest(id, request);
            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingRequest = _requestService.GetRequestById(id);
            if (existingRequest == null)
            {
                return NotFound();
            }
            _requestService.DeleteRequest(id);
            return NoContent();
        }
    }
}
