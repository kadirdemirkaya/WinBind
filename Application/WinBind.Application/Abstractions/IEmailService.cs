using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Commands.Requests;

namespace WinBind.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(SendEmailCommandRequest request);
    }
}
