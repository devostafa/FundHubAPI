using FundHub.Data.Data.Models;
using FundHub.Services.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace FundHubAPI.Controllers;

[Route("Mail")]
public class MailController(IMail mailService) : BaseController
{
    [HttpPost("send")]
    public async Task<bool> SendMail(MailRequest mailRequest)
    {
        return await mailService.SendMail(mailRequest);
    }
}