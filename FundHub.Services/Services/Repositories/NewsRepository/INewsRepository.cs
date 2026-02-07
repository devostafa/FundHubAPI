using FundHub.Data.Data.DTOs.ResponseDTO;
using FundHub.Data.Data.Models;

namespace FundHub.Services.Services.Repositories.NewsRepository;

public interface INewsRepository
{
    public Task<List<NewsResponseDTO>> GetNews();
    public Task<NewsResponseDTO> GetNewsArticle(string newsId);
    public Task CreateNewsFolders();
    public Task AddNews(News newsEntry);
}