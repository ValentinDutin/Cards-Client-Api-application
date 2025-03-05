using CommonFiles.Models;

namespace CardsServer.Data.Services
{
    public interface ICardRepository
    {
        Task<List<Card>> GetCardsAsync();
        Task<Card> GetCardByIdAsync(Guid id);
        Task AddCardAsync(Card item);
        Task<bool> DeleteCardByIdAsync(Guid id);
        Task DeleteAllCardsAsync();
    }
}
