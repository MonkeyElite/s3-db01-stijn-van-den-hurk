using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.ViewModels.Request;
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
        public ActionResult<Request> Get()
        {
            try
            {
                List<Request> requests = _requestService.GetRequests();
                return Ok(requests);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Request> Get(int id)
        {
            try
            {
                Request request = _requestService.GetRequestById(id);

                if (request == null)
                {
                    return NotFound();
                }

                return Ok(request);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<RequestViewModel> Post([FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                RequestViewModel createRequest = new (_requestService.CreateRequest(new Request(request.Title, request.Description)));
                return CreatedAtAction(nameof(Get), createRequest);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                var existingRequest = _requestService.GetRequestById(id);

                if (existingRequest == null)
                {
                    return NotFound();
                }

                var updatedRequest = _requestService.UpdateRequest(id, request);
                return Ok(updatedRequest);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingRequest = _requestService.GetRequestById(id);

            if (existingRequest == null)
            {
                return NotFound();
            }

            try
            {
                _requestService.DeleteRequest(id);
                return Ok();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
