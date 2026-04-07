using Microsoft.EntityFrameworkCore;
using SlotMachine;
using SlotMachine.Models;
using Task = SlotMachine.Models.Task;

namespace SlotMachineTest.Integration.Seeders;

public class DefaultSeeder : ISeeder
{
    protected virtual List<Task> Users =>
    [
        new() { Id = 1, Name = "admin", Description = "a5f8e89441b090fcdefb5664c00beb82950e8157", Finished = true },
        new() { Id = 2, Name = "game1", Description = "f47f85345dc0cf18182113d4663fef15ae458ac0", Finished = false },
        new() { Id = 3, Name = "barman1", Description = "f47f85345dc0cf18182113d4663fef15ae458ac0", Finished = false }
    ];


    public void Clear(ApplicationDbContext dbContext)
    {
        var tables = GetAll().Select(x => x.GetType()).Distinct().Reverse();
        foreach (var table in tables)
        {
            var myClassTableName = dbContext.Model.FindEntityType(table);

            if (myClassTableName != null)
                dbContext.Database.ExecuteSqlRaw(
                    "DELETE FROM " + myClassTableName.GetTableName()
                );
        }
    }

    public void Seed(ApplicationDbContext dbContext)
    {
        GetAll().ForEach(x =>
        {
            dbContext.AddRange(x);
            dbContext.SaveChanges();
        });
    }


    protected virtual List<object> GetAll()
    {
        var retD = new List<object>();
        retD.AddRange(Users);
        return retD;
    }
}