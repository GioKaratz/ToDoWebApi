using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Data;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using ToDoWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using ToDoWebApi.Data.Service;

namespace ToDoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IToDoRepository _repo;

        public TodosController(IToDoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get()
        {
            if (User == null)
            {
                return BadRequest("User is null");
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user ID");
            }

            var todos = await _repo.GetTodos(userId);
            return Ok(todos);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetById(int id)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user ID");
            }

            var todo = await _repo.GetTodoById(userId, id);
            if (todo == null)
            {
                return StatusCode(404, new NotFoundException("Todo not found"));
            }

            return Ok(todo);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(ToDoCreate todoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user ID");
            }

            var todo = await _repo.CreateTodo(userId, todoCreate);
            return Ok(todo);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> Update(int id, ToDoUpdate todoUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string userRole = User.FindFirstValue(ClaimTypes.Role);
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Invalid user ID");
                }

                var todo = await _repo.UpdateTodo(userId, id, todoUpdate);
                return Ok(todo);
            }
            catch (Exception)
            {

                return StatusCode(404, new NotFoundException("Todo not found"));
            }
        }

        [HttpDelete, Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string userRole = User.FindFirstValue(ClaimTypes.Role);
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Invalid user ID");
                }

                var todo = await _repo.DeleteTodo(userId, id);
                return Ok(todo);
            }
            catch (Exception)
            {
                return StatusCode(404, new NotFoundException("Todo not found"));
            }
        }
    }
}
