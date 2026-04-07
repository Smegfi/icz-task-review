using IczTask;

namespace IczTaskTest.Integration.Seeders;

public interface ISeeder
{
    void Clear(ApplicationDbContext applicationDbContext);
    void Seed(ApplicationDbContext dbContext);
}