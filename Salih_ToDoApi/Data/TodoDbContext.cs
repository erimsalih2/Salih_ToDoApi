namespace Salih_ToDoApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using Salih_ToDoApi.Model;

    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
