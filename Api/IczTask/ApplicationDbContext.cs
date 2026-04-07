using Microsoft.EntityFrameworkCore;
using Task = SlotMachine.Models.Task;

namespace SlotMachine;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Task> Tasks { get; set; } = null!;
}