using AutoMapper;
using FundHub.Data.Data;
using FundHub.Data.Data.DTOs;
using FundHub.Data.Data.DTOs.RequestDTO;
using FundHub.Data.Data.Models;
using FundHub.Data.Services.PasswordHash;
using FundHub.Services.Services.JWT;
using FundHub.Services.Services.JWT.DTO;
using FundHub.Services.Services.Mail;
using FundHub.Services.Services.Repositories.UsersRepository;
using Microsoft.EntityFrameworkCore;

namespace FundHub.Services.Services.Authentication;

public class Authentication : IAuthentication
{
    private readonly IJWT _jwtService;
    private IMapper _mapper;
    private readonly IPasswordHash _passwordHashService;
    private readonly IUserRepository _usersRepo;
    private readonly IMail _mailService;

    public Authentication(IJWT jwtService, IMapper mapper, IPasswordHash passwordHashService, IUserRepository usersRepo, IMail mailService)
    {
        _jwtService = jwtService;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
        _usersRepo = usersRepo;
        _mailService = mailService;
    }
    
    public async Task<string> Login(LoginRequestDTO loginreq)
    {
        string token = "";
        //1st, check username in database
        bool checkuser = await _usersRepo.CheckUser(loginreq.Username);
        if (!checkuser)
        {
            return "username / password are wrong";
        }
        else
        {
            User loginuser = await _usersRepo.GetUserByName(loginreq.Username);
            JWTRequestDTO userjwtreq = _mapper.Map<JWTRequestDTO>(loginuser);
            //2nd verify password
            bool checkpassword = await VerifyPassword(loginreq.Password, loginuser.Hashedpassword) ;
            if (checkpassword)
            {
                token  = _jwtService.CreateToken(userjwtreq);
                return token;
            }
            else
            {
                return "username / password are wrong";
            }
        }
    }


    public async Task<bool> Register(RegisterRequestDTO registerreq)
    {
        if (await CheckUser(registerreq.Username))
        {
            return false;
        }
        else
        {
            //map new user data from registerreq
            UserToAddDTO  newUserDto = _mapper.Map<UserToAddDTO>(registerreq);
            //1-hash password
            string hashedpassword =  _passwordHashService.CreateHashedPassword(registerreq.Password);
            //2-assign hashedpassword to A NEW COPY OF newUserDTO
            newUserDto = newUserDto with { Hashedpassword = hashedpassword, Usertype = "user"};
            //3-add to database
            bool successfulAdd = await _usersRepo.AddUser(newUserDto);
            if (successfulAdd)
            {
                await MailSuccessfulRegistration(registerreq);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private async Task<bool> VerifyPassword(string passwordtoverify, string hashedpassword)
    {
        //1-extract salt from database user hashedpassword, pass string pattern SALT.HASHEDPASSWORD
        var extractedsavedpassword = hashedpassword.Split(".");
        var extractedsalt = extractedsavedpassword[0];
        var extractedhashedpass = extractedsavedpassword[1];
        //2-generate hashed password with given salt
        var passwordtotest = _passwordHashService.HashPasswordWithGivenSalt( extractedsalt, passwordtoverify);
        //3-compare
        if (passwordtotest == extractedhashedpass)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private async Task<bool> CheckUser(string username)
    {
        return await _usersRepo.CheckUser(username);
    }

    private async Task MailSuccessfulRegistration(RegisterRequestDTO registerRequest)
    {
        MailRequest successfulRegistrationNotify = new MailRequest()
        {
            Emailto = registerRequest.Email, 
            Subject = "Welcome to FundHub",
            Message = $"Dear {registerRequest.Username}, thank you for registering on FundHub\\n" +
                      $"We hope you enjoy the platform and support your community of people who wish to achieve their dreams and ideas"
        };
        await _mailService.SendMail(successfulRegistrationNotify);
    }
    
}