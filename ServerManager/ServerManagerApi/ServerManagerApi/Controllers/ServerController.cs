using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.Models.Server;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ServerController(ServerService serverService) : ControllerBase
    {
        private readonly ServerService _serverService = serverService;

        [HttpGet]
        public ActionResult<Server> Get()
        {
            try
            {
                List<Server> servers = _serverService.GetServers();
                return Ok(servers);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Server> Get(int id)
        {
            try
            {
                Server server = _serverService.GetServerById(id);

                if (server == null)
                {
                    return NotFound();
                }

                return Ok(server);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<ServerViewModel> Post([FromBody] Server server)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                ServerViewModel createServer = new (_serverService.CreateServer(new Server(server.Title, server.Description, server.GameName, server.Ip, server.Port, server.Password)));
                return CreatedAtAction(nameof(Get), createServer);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Server server)
        {
            server.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                var existingServer = _serverService.GetServerById(id);

                if (existingServer == null)
                {
                    return NotFound();
                }

                var updatedServer = _serverService.UpdateServer(server);
                return Ok(updatedServer);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingServer = _serverService.GetServerById(id);

            if (existingServer == null)
            {
                return NotFound();
            }

            try
            {
                _serverService.DeleteServer(id);
                return Ok();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Temp Solution
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
