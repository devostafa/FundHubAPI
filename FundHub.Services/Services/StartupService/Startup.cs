using FundHub.Data.Data;
using FundHub.Services.Services.Repositories.NewsRepository;
using FundHub.Services.Services.Repositories.ProjectsRepository;
using FundHub.Services.Services.Repositories.UsersRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FundHub.Services.Services.StartupService;

public static class Startup
{
    public static void ExecuteStartupServices(IServiceProvider serviceProvider, IWebHostEnvironment webEnv)
    {
        var storageFolder = Path.Combine(webEnv.ContentRootPath, "Storage");
        Directory.CreateDirectory(storageFolder);
        
        var dbService = serviceProvider.GetRequiredService<DataContext>();
        dbService.Database.Migrate();
        
        var newsService = serviceProvider.GetRequiredService<INewsRepository>();
        newsService.CreateNewsFolders();
        
        var projectsService = serviceProvider.GetRequiredService<IProjectsRepository>();
        projectsService.CreateFolders();
        
        var usersService = serviceProvider.GetRequiredService<IUserRepository>();
        usersService.CreateFolders();
    }
}