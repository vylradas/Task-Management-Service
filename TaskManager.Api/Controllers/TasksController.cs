using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Repositories;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskRepository _repository;

        public TasksController(TaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<TaskItem> Get(Guid id)
        {
            var task = _repository.GetById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            _repository.Add(task);
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }
    }
}
