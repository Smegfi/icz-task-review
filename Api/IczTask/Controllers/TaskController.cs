using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = IczTask.Models.Task;

namespace IczTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ApplicationDbContext dbContext, ILogger<TaskController> logger) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public Task Create(Task task)
    {
        
        var retD = dbContext.Tasks.Add(task).Entity;
        dbContext.SaveChanges();
        return retD;
    }


    [HttpPut]
    [Authorize(Roles = "Admin")]
    public Task Update(Task task)
    {
        var retD = dbContext.Tasks.Update(task).Entity;
        dbContext.SaveChanges();
        return retD;
    }


    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public Task GetById(int id)
    {
        return dbContext.Tasks.Single(x => x.Id == id);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IEnumerable<Task> GetAll()
    {
        return dbContext.Tasks.ToList();
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public void Delete(int id)
    {
        var user = dbContext.Tasks.Single(x => x.Id == id);
        dbContext.Tasks.Remove(user);
        dbContext.SaveChanges();
    }
}