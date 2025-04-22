namespace Salih_ToDoApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Salih_ToDoApi.Data;
    using Salih_ToDoApi.Model;

    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext context;

        public TodoController(TodoDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            return await this.context.Todos.ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoById(int id)
        {
            var todo = await this.context.Todos.FindAsync(id).ConfigureAwait(false);
            return todo == null ? this.NotFound() : this.Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem item)
        {
            if (item == null)
            {
                return this.BadRequest("Item is null");
            }

            this.context.Todos.Add(item);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetTodoById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItem item)
        {
            if (id != item.Id)
            {
                return this.BadRequest();
            }

            var existing = await this.context.Todos.FindAsync(id).ConfigureAwait(false);
            if (existing == null)
            {
                return this.NotFound();
            }

            existing.Title = item.Title;
            existing.Description = item.Description;

            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await this.context.Todos.FindAsync(id).ConfigureAwait(false);
            if (todo == null)
            {
                return this.NotFound();
            }

            this.context.Todos.Remove(todo);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }
    }
}
