using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SlotMachine;
using SlotMachineTest.Integration.Infrastructure;
using SlotMachineTest.Integration.Seeders;
using Task = SlotMachine.Models.Task;

namespace SlotMachineTest.Integration;

public class TaskTests
{
    private readonly HttpClient _client;

    private readonly CustomWebApplicationFactory<Program> _factory = new();

    public TaskTests()
    {
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }
        );
    }


    [SetUp]
    public void MockSetup()
    {
        if (TestContext.CurrentContext.Test.Properties.Get("Seeder") is string seeder)
        {
            var seederInstance = (ISeeder)Activator.CreateInstance(Type.GetType(seeder)!)!;
            seederInstance
                .Seed(
                    _factory
                        .Services.CreateScope()
                        .ServiceProvider.GetRequiredService<ApplicationDbContext>()
                );
        }

        if (TestContext.CurrentContext.Test.Properties.Get("MockUser") is string userInfo)
        {
            var parts = userInfo.Split(';');
            _factory
                .Services.GetRequiredService<TestAuthHandlerUserProvider>().SetUser(parts[0], parts[1].Split(","));
        }
    }


    [TearDown]
    public void MockTeardown()
    {
        if (TestContext.CurrentContext.Test.Properties.Get("Seeder") is string seeder)
        {
            var seederInstance = (ISeeder)Activator.CreateInstance(Type.GetType(seeder)!)!;
            seederInstance
                .Clear(
                    _factory
                        .Services.CreateScope()
                        .ServiceProvider.GetRequiredService<ApplicationDbContext>()
                );
        }
    }


    [OneTimeTearDown]
    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }


    [Test]
    [Property("Seeder", "IczTaskTest.Integration.Seeders.DefaultSeeder")]
    [Property("MockUser", "admin;Admin")]
    public async System.Threading.Tasks.Task Create()
    {
        var defaultPage = await _client.PostAsJsonAsync("/api/Task",
            new Task { Name = "User2", Description = "654321456", Finished = false });
        defaultPage.EnsureSuccessStatusCode();
    }
    
    
    [Test]
    [Property("Seeder", "IczTaskTest.Integration.Seeders.DefaultSeeder")]
    [Property("MockUser", "admin;Admin")]
    public async System.Threading.Tasks.Task Update()
    {
        var defaultPage = await _client.PutAsJsonAsync("/api/Task",
            new Task { Name = "User2", Description = "654321456", Finished = false });
        defaultPage.EnsureSuccessStatusCode();
    }
    
    

    [Test]
    [Property("Seeder", "IczTaskTest.Integration.Seeders.DefaultSeeder")]
    [Property("MockUser", "admin;Admin")]
    public async System.Threading.Tasks.Task GetById()
    {
        var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = dbContext.Tasks.First();

        var defaultPage = await _client.GetAsync($"/api/Task/{user.Id}");
        defaultPage.EnsureSuccessStatusCode();
    }

    [Test]
    [Property("Seeder", "IczTaskTest.Integration.Seeders.DefaultSeeder")]
    [Property("MockUser", "admin;Admin")]
    public async System.Threading.Tasks.Task GetAll()
    {
        var defaultPage = await _client.GetAsync("/api/Task");
        defaultPage.EnsureSuccessStatusCode();
    }



    [Test]
    [Property("Seeder", "IczTaskTest.Integration.Seeders.DefaultSeeder")]
    [Property("MockUser", "admin;Admin")]
    public async System.Threading.Tasks.Task Delete()
    {
        var defaultPage = await _client.DeleteAsync("/api/Task/1");
        defaultPage.EnsureSuccessStatusCode();
    }
}