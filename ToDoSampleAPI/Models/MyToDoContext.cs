using Microsoft.EntityFrameworkCore;

namespace ToDoSampleAPI.Models
{
    public class MyTodoContext : DbContext
    {
        public MyTodoContext(DbContextOptions<MyTodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
