using AutoMapper;
using AutoMapper.QueryableExtensions;
using FundHub.Data.Data;
using FundHub.Data.Data.DTOs.ResponseDTO;
using FundHub.Data.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FundHub.Services.Services.Repositories.NewsRepository;

public class NewsRepository : INewsRepository
{
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _hostEnv;
    private readonly IMapper _mapper;

    public NewsRepository(DataContext db, IMapper mapper, IWebHostEnvironment hostEnv)
    {
        _db = db;
        _hostEnv = hostEnv;
        _mapper = mapper;
    }

    public async Task<List<NewsResponseDTO>> GetNews()
    {
        return await _db.News.ProjectTo<NewsResponseDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<NewsResponseDTO> GetNewsArticle(string newsId)
    {
        return await _db.News.ProjectTo<NewsResponseDTO>(_mapper.ConfigurationProvider).FirstAsync(n => n.Id == Guid.Parse(newsId));
    }

    public async Task CreateNewsFolders()
    {
        try
        {
            List<News> newsList = await _db.News.ToListAsync();
            foreach (var news in newsList)
            {
                var newsFolderPath = Path.Combine(_hostEnv.ContentRootPath, "Storage", "News",
                    $"{news.Id}", "Images");
                Directory.CreateDirectory(newsFolderPath); 
            }
            Console.WriteLine("Created News folders successfully");
        }
        catch (Exception err)
        {
            throw err;
        }
    }

    public async Task AddNews(News newsEntry)
    {
        await _db.News.AddAsync(newsEntry);
    }
}