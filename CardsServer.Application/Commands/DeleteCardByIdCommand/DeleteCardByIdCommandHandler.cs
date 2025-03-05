using CardsServer.Data.Services;
using MediatR;

namespace CardsServer.Application.Commands.DeleteCardByIdCommand
{
    public class DeleteCardByIdCommandHandler : IRequestHandler<DeleteCardByIdCommand, bool>
    {
        private readonly ICardRepository _cardRepository;
        public DeleteCardByIdCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<bool> Handle(DeleteCardByIdCommand request, CancellationToken cancellationToken)
        {
            return await _cardRepository.DeleteCardByIdAsync(request.Id);
        }
    }
}