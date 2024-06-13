using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.Models.Session;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult<List<Session>> Get()
        {
            try
            {
                List<Session> sessions = _sessionService.GetSessions();
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SessionViewModel> Get(int id)
        {
            try
            {
                Session session = _sessionService.GetSessionById(id);
                if (session == null)
                {
                    return NotFound();
                }

                SessionViewModel sessionViewModel = new SessionViewModel(session.Title, session.Description, session.StartTime, session.EndTime, session.ServerId);
                return Ok(sessionViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<SessionViewModel> Post([FromBody] SessionViewModel session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Session createdSession = _sessionService.CreateSession(new Session(session.Title, session.Description, session.StartTime, session.EndTime, session.ServerId));
                SessionViewModel createdSessionViewModel = new SessionViewModel(createdSession.Title, createdSession.Description, createdSession.StartTime, createdSession.EndTime, createdSession.ServerId);
                return CreatedAtAction(nameof(Get), new { id = createdSession.Id }, createdSessionViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SessionViewModel sessionViewModel)
        {
            Session session = new Session(sessionViewModel.Title, sessionViewModel.Description, sessionViewModel.StartTime, sessionViewModel.EndTime, sessionViewModel.ServerId);
            session.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Session existingSession = _sessionService.GetSessionById(id);
                if (existingSession == null)
                {
                    return NotFound();
                }

                Session updatedSession = _sessionService.UpdateSession(session);
                SessionViewModel updatedSessionViewModel = new SessionViewModel(updatedSession.Title, updatedSession.Description, updatedSession.StartTime, updatedSession.EndTime, updatedSession.ServerId);
                return Ok(updatedSessionViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Session existingSession = _sessionService.GetSessionById(id);

            if (existingSession == null)
            {
                return NotFound();
            }

            try
            {
                _sessionService.DeleteSession(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
