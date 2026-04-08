using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using TaskEntity = IczTask.Models.Task;

namespace IczTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ApplicationDbContext dbContext, HybridCache cache) : ControllerBase
{
    private const string TaskCacheTag = "tasks";

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TaskEntity>> Create([FromBody] TaskEntity task, CancellationToken cancellationToken)
    {
        var retD = dbContext.Tasks.Add(task).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync(TaskCacheTag, cancellationToken);
        return retD;
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TaskEntity>> Update([FromBody] TaskEntity task, CancellationToken cancellationToken)
    {
        var retD = dbContext.Tasks.Update(task).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync(TaskCacheTag, cancellationToken);
        return retD;
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TaskEntity>> GetById(int id, CancellationToken cancellationToken)
    {
        var entity = await cache.GetOrCreateAsync(
            $"task:{id}",
            async cancel => await dbContext.Tasks.SingleAsync(x => x.Id == id, cancel),
            tags: [TaskCacheTag],
            cancellationToken: cancellationToken);
        return entity;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<TaskEntity>>> GetAll(
        [FromQuery] string? namefilter,
        CancellationToken cancellationToken)
    {
        var filterKey = namefilter ?? string.Empty;
        var list = await cache.GetOrCreateAsync(
            $"tasks:list:{filterKey}",
            async cancel => await dbContext.Tasks
                .Where(t => string.IsNullOrEmpty(namefilter) || t.Name.Contains(namefilter))
                .ToListAsync(cancel),
            tags: [TaskCacheTag],
            cancellationToken: cancellationToken);
        return list;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Tasks.SingleAsync(x => x.Id == id, cancellationToken);
        dbContext.Tasks.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        await cache.RemoveByTagAsync(TaskCacheTag, cancellationToken);
        return NoContent();
    }
}


