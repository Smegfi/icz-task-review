using Microsoft.EntityFrameworkCore;
using Task = IczTask.Models.Task;

namespace IczTask;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Task> Tasks { get; set; } = null!;
}