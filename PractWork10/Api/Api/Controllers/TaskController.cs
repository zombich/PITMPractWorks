using Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public static List<MyTask> Tasks { get; set; } = new()
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
        /// <summary>
        /// Получить список всех задач.
        /// </summary>
        /// <remarks>
        /// Пример корректного ответа:
        ///     GET /api/Task
        ///     [
        ///       {
        ///         "id": 1,
        ///         "name": "сделать круто",
        ///         "description": "крутое задание",
        ///         "endOfTask": "2026-10-01",
        ///         "status": 0
        ///       }
        ///     ]
        /// </remarks>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet]
        public ActionResult<IEnumerable<MyTask>> Get()
        {

            return Ok(Tasks);
        }

        // GET api/<TaskController>/5
        /// <summary>
        /// Получить задачу по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/Task/5
        /// Пример корректного ответа:
        ///     {
        ///       "id": 5,
        ///       "name": "ааааааааааа",
        ///       "description": "я топ",
        ///       "endOfTask": "2025-01-12",
        ///       "status": 1
        ///     }
        /// </remarks>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Задача не найдена</response>
        [HttpGet("{id}")]
        public ActionResult<MyTask> Get(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task is null)
                return NotFound();

            return Ok(task);
        }

        // POST api/<TaskController>
        /// <summary>
        /// Создать новую задачу.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     POST /api/Task
        ///     {
        ///        "name": "дело123",
        ///        "description": "аписание",
        ///        "endOfTask": "2025-12-31",
        ///        "status": 0
        ///     }
        /// </remarks>
        /// <response code="201">Создана новая задача</response>
        /// <response code="400">Неверный запрос</response>
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
        /// <summary>
        /// Обновить задачу по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     PUT /api/Task/3
        ///     {
        ///       "name": "ооо",
        ///       "description": "моя оборона",
        ///       "endOfTask": "2026-01-01",
        ///       "status": 2
        ///     }
        /// </remarks>
        /// <response code="204">Обновление выполнено успешно (No Content)</response>
        /// <response code="400">Неверные данные</response>
        /// <response code="404">Задача не найдена</response>
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
        /// <summary>
        /// Удалить задачу по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     DELETE /api/Task/7
        /// </remarks>
        /// <response code="204">Задача успешно удалена</response>
        /// <response code="404">Задача не найдена</response>
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
