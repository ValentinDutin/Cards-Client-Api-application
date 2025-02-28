using CardsServer.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.Application.Commands.DeleteCardByIdCommand
{
    public class DeleteCardByIdCommandHandler : IRequestHandler<DeleteCardByIdCommand, bool>
    {
        private CardsRepository _cardsRepository;
        public DeleteCardByIdCommandHandler(CardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository;
        }
        public async Task<bool> Handle(DeleteCardByIdCommand request, CancellationToken cancellationToken)
        {
            return await _cardsRepository.DeleteCardByIdAsync(request.Id);
        }
    }
}
