using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommandRequest, ResponseModel<bool>>
    {
        private readonly IEmailService _emailService;
        public SendEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<ResponseModel<bool>> Handle(SendEmailCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return new ResponseModel<bool>("Email sent failed. Request cant be null.", 400);

            await _emailService.SendEmailAsync(request);
            return new ResponseModel<bool>("Mail send successfuly.", 200);
        }
    }
}
