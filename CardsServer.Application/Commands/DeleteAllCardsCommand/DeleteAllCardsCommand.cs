using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.Application.Commands.DeleteAllCardsCommand
{
    public class DeleteAllCardsCommand : IRequest<Unit>
    {
    }
}
