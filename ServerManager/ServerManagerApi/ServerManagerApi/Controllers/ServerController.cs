using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerManagerApi.Models.Server;
using ServerManagerCore.Models;
using ServerManagerCore.Services;

namespace ServerManagerApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ServerService _serverService;

        public ServerController(ServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpGet]
        public ActionResult<List<Server>> Get()
        {
            try
            {
                List<Server> servers = _serverService.GetServers();
                return Ok(servers);
            }
            catch (Exception ex)
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
                Server server = _serverService.GetServerById(id);
                if (server == null)
                {
                    return NotFound();
                }

                ServerViewModel serverViewModel = new ServerViewModel(server.Title, server.Description, server.GameName, server.Ip, server.Port, server.Password);
                return Ok(serverViewModel);
            }
            catch (Exception ex)
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
            }

            try
            {
                Server createdServer = _serverService.CreateServer(new Server(server.Title, server.Description, server.GameName, server.Ip, server.Port, server.Password));
                ServerViewModel createdServerViewModel = new ServerViewModel(createdServer.Title, createdServer.Description, createdServer.GameName, createdServer.Ip, createdServer.Port, createdServer.Password);
                return CreatedAtAction(nameof(Get), new { id = createdServer.Id }, createdServerViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
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
            }

            try
            {
                Server existingServer = _serverService.GetServerById(id);
                if (existingServer == null)
                {
                    return NotFound();
                }

                Server updatedServer = _serverService.UpdateServer(server);
                ServerViewModel updatedServerViewModel = new ServerViewModel(updatedServer.Title, updatedServer.Description, updatedServer.GameName, updatedServer.Ip, updatedServer.Port, updatedServer.Password);
                return Ok(updatedServerViewModel);
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
            Server existingServer = _serverService.GetServerById(id);

            if (existingServer == null)
            {
                return NotFound();
            }

            try
            {
                _serverService.DeleteServer(id);
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
