using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommandRequest, ResponseModel<bool>>
    {


        public Task<ResponseModel<bool>> Handle(SendEmailCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
