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
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ServerViewModel> Get(int id)
        {
            try
            {
                ServerViewModel server = new(_serverService.GetServerById(id));

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
        public ActionResult<ServerViewModel> Post([FromBody] ServerViewModel server)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                var createdServer = _serverService.CreateServer(new Server(server.Title, server.Description, server.GameName, server.Ip, server.Port, server.Password));
                var createServer = new ServerViewModel(createdServer);
                return CreatedAtAction(nameof(Get), new { id = createdServer.Id }, createServer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServerViewModel serverViewModel)
        {
            Server server = new Server(serverViewModel.Title, serverViewModel.Description, serverViewModel.GameName, serverViewModel.Ip, serverViewModel.Port, serverViewModel.Password);
            server.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            try
            {
                Server existingServer = _serverService.GetServerById(id);

                if (existingServer == null)
                {
                    return NotFound();
                }

                ServerViewModel updatedServer = new (_serverService.UpdateServer(server));
                return Ok(updatedServer);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Server existingServer = _serverService.GetServerById(id);

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
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
