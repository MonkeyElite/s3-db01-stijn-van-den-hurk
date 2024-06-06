using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.Models.Session;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SessionController(SessionService sessionService) : ControllerBase
    {
        private readonly SessionService _sessionService = sessionService;

        [HttpGet]
        public ActionResult<Session> Get()
        {
            try
            {
                List<Session> sessions = _sessionService.GetSessions();
                return Ok(sessions);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Session> Get(int id)
        {
            try
            {
                Session session = _sessionService.GetSessionById(id);

                if (session == null)
                {
                    return NotFound();
                }

                return Ok(session);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<SessionViewModel> Post([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                SessionViewModel createSession = new (_sessionService.CreateSession(new Session(session.Title, session.Description, session.StartTime, session.EndTime, session.ServerId)));
                return CreatedAtAction(nameof(Get), createSession);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Session session)
        {
            session.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                var existingSession = _sessionService.GetSessionById(id);

                if (existingSession == null)
                {
                    return NotFound();
                }

                var updatedSession = _sessionService.UpdateSession(session);
                return Ok(updatedSession);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingSession = _sessionService.GetSessionById(id);

            if (existingSession == null)
            {
                return NotFound();
            }

            try
            {
                _sessionService.DeleteSession(id);
                return Ok();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
