using LE.Common.Api.Paginators.Tasks;
using LE.Common.Api.Tasks;
using LE.Infrastructure.Services;
using LE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService context)
        {
            _service = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TaskPaginatorRequest request)
        {
            var userId = GetUserId();
            var result = await _service.GetAsync(userId, request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = GetUserId();
            var result = await _service.GetAsync(userId, id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskDto value)
        {
            var userId = GetUserId();
            var result = await _service.CreateAsync(userId, value);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TaskDto value)
        {
            var userId = GetUserId();
            await _service.UpdateAsync(userId, id, value);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            await _service.DeleteAsync(userId, id);

            return NoContent();
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(item => item.Type == UserService.UserId);

            if (userIdClaim is null)
                throw new UnauthorizedAccessException();

            return new Guid(userIdClaim.Value);
        }
    }
}
