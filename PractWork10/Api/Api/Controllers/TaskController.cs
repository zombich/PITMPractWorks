using Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public List<MyTask> Tasks { get; set; } = new()
        {
            new MyTask() { Id = 1, EndOfTask = new DateOnly(2026,10,1), Description = "крутое задание", Name="сделать круто", Status= Status.During},
            new MyTask() { Id = 2, EndOfTask = new DateOnly(2025,9,12), Description = "не крутое задание", Name="сделать не круто", Status= Status.Completed},
            new MyTask() { Id = 3, EndOfTask = new DateOnly(2025,12,12), Description = "бим бим", Name="бам бам", Status= Status.During},
            new MyTask() { Id = 4, EndOfTask = new DateOnly(2025,11,12), Description = "123123", Name="йцукен", Status= Status.Completed},
            new MyTask() { Id = 5, EndOfTask = new DateOnly(2025,1,12), Description = "я топ", Name="ааааааааааа", Status= Status.Completed},
            new MyTask() { Id = 6, EndOfTask = new DateOnly(2025,9,10), Description = "не задание", Name="не делать", Status= Status.Cancelled},
            new MyTask() { Id = 7, EndOfTask = new DateOnly(2025,12,12), Description = "статья 228", Name="сесть", Status= Status.Cancelled},
            new MyTask() { Id = 12, EndOfTask = new DateOnly(2025,12,12), Description = "абубачир", Name="найти человека", Status= Status.During},
        };

        // GET: api/<TaskController>
        [HttpGet]
        public ActionResult<IEnumerable<MyTask>> Get()
        {
            return Ok(Tasks);
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public ActionResult<MyTask> Get(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();

            return Ok(task);
        }

        // POST api/<TaskController>
        [HttpPost]
        public ActionResult Post([FromBody] MyTask task)
        {
            if (task is null)
                return BadRequest();

            task.Id = Tasks.Count();
            Tasks.Add(task);

            return Created();
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MyTask task)
        {
            var index = Tasks.FindIndex(t=> t.Id == id);
            if (index < 0)
            { 
                return NotFound();
            }

            Tasks[index].Name = task.Name;
            Tasks[index].Description = task.Description;
            Tasks[index].EndOfTask = task.EndOfTask;
            Tasks[index].Status = task.Status;

            return NoContent();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);

            if (task is null)
                return NotFound();

            Tasks.Remove(task);

            return NoContent();
        }
    }
}
