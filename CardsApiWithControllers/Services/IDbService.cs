using Common.Models;
namespace CardsApiWithControllers.Services

{
    public interface IDbService
    {
        public List<Card> GetData();
        public bool InsertCard(Card item);
    }
}
