using HomeBanking2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking2._0.Repositories.Implementations
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(HomeBankingContext repositoryContext) : base(repositoryContext) { }

        //modificar y agregar los metodos

        public void AddCard(Card card)
        {
            Create(card);
            SaveChanges();
        }

        public IEnumerable<Card> GetAllCards()
        {
            return FindAll()
                .ToList();
        }

        public IEnumerable<Card> GetAllCardsByClient(long clientId)
        {
            return FindByCondition(c => c.ClientId == clientId)
                .ToList();
        }

        public Card GetCardById(long id)
        {
            return FindByCondition(c => c.Id == id)
            .FirstOrDefault();
        }

        public Card GetCardByNumber(long clientId, string number)
        {
            return FindByCondition(c => c.Number == number && c.ClientId == clientId)
                .FirstOrDefault();
        }

        public IEnumerable<Card> GetAllCardsByType(long clientId, string type)
        {
            return FindByCondition(c => c.Type == type && c.ClientId == clientId)
                .ToList();
        }

    }
}
