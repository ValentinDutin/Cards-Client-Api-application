using CommonFiles.Models;

namespace CardsClient.Services
{
    public interface IHttpClientService
    {
        Task PostCardAsync(Card model);
        Task<List<Card>> GetCardsAsync();
        Task DeleteCardByIdAsync(Guid id);
        Task DeleteAllCardsAsync();
    }
}
