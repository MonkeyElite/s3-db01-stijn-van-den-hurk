using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.ViewModels.Request;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RequestViewModel>> Get()
        {
            try
            {
                var requests = _requestService.GetRequests()
                    .Select(request => new RequestViewModel(request.Title, request.Description))
                    .ToList();

                if (requests.Count == 0)
                {
                    return NotFound();
                }

                return Ok(requests);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<RequestViewModel> Get(int id)
        {
            try
            {
                var request = _requestService.GetRequestById(id);

                if (request == null)
                {
                    return NotFound();
                }

                return Ok(new RequestViewModel(request.Title, request.Description));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<RequestViewModel> Post([FromBody] RequestViewModel requestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdRequest = _requestService.CreateRequest(new Request(requestViewModel.Title, requestViewModel.Description));
                var createdViewModel = new RequestViewModel(createdRequest.Title, createdRequest.Description);
                return CreatedAtAction(nameof(Get), new { id = createdRequest.Id }, createdViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RequestViewModel requestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingRequest = _requestService.GetRequestById(id);

                if (existingRequest == null)
                {
                    return NotFound();
                }

                existingRequest.Title = requestViewModel.Title;
                existingRequest.Description = requestViewModel.Description;

                var updatedRequest = _requestService.UpdateRequest(existingRequest);
                var updatedViewModel = new RequestViewModel(updatedRequest.Title, updatedRequest.Description);

                return Ok(updatedViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
