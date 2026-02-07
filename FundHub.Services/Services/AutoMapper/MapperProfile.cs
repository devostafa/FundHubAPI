using AutoMapper;
using FundHub.Data.Data.DTOs;
using FundHub.Data.Data.DTOs.RequestDTO;
using FundHub.Data.Data.DTOs.ResponseDTO;
using FundHub.Data.Data.Models;
using FundHub.Services.Services.JWT.DTO;

namespace FundHub.Services.Services.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //model to DTO
        CreateMap<User,UserDTO>();
        CreateMap<Project,ProjectResponseDTO>();
        CreateMap<Donation, DonationResponseDTO>();
        CreateMap<News,NewsResponseDTO>();
        CreateMap<User,JWTRequestDTO>();
        CreateMap<RegisterRequestDTO, UserDTO>();
        CreateMap<Category,CategoryResponseDTO>();

        //DTO to Model
        CreateMap<ProjectRequestDTO, Project>();
        CreateMap<DonationRequestDTO, Donation>();
        CreateMap<DonationResponseDTO, Donation >();
        CreateMap<UserDTO,User>();
        
        //DTO to DTO
        CreateMap<UserDTO, JWTRequestDTO>();
    }
}