using FundHub.Data.Data.Models;
using FundHub.Services.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace FundHubAPI.Controllers;

[Route("Mail")]
public class MailController : BaseController
{
    private readonly IMail _mailservice;

    public MailController(IMail mailservice)
    {
        _mailservice = mailservice;
    }
    
    [HttpPost("send")]
    public async Task<bool> SendMail(MailRequest mailtosend)
    {
        return await _mailservice.SendMail(mailtosend);
    }
}