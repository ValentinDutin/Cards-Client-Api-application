using CardsServer.Data.Context;
using CommonFiles.Models;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.Data.Services
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _appDbContext;
        public CardRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Card>> GetCardsAsync()
        {
            return await _appDbContext.Cards.ToListAsync() ?? [];
        }
        public async Task AddCardAsync(Card item)
        {
            await _appDbContext.Cards.AddAsync(item);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Card> GetCardByIdAsync(Guid id)
        {
            return await _appDbContext.Cards.FindAsync(id) ?? new();
        }
        public async Task<bool> DeleteCardByIdAsync(Guid id)
        {
            var result = await _appDbContext.Cards.FindAsync(id);
            if (result == null) return false;
            _appDbContext.Cards.Remove(result);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAllCardsAsync()
        {
            _appDbContext.Cards.RemoveRange(_appDbContext.Cards);
            await _appDbContext.SaveChangesAsync();
        }
    }
}