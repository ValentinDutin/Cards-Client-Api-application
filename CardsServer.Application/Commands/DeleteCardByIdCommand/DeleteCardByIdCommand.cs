using MediatR;

namespace CardsServer.Application.Commands.DeleteCardByIdCommand
{
    public class DeleteCardByIdCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteCardByIdCommand(Guid id) { Id = id; }
    }
}