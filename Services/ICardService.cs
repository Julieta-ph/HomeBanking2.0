using HomeBanking2._0.Models;

namespace HomeBanking2._0.Services
{
    public interface ICardService
    {
        int GenerateCVV();
        string GenerateNumberCard(long clientId);

        IEnumerable<Card> GetAllCardsByType(long clientId, string type);

        IEnumerable<Card> GetAllCardsByClient(long clientId);

        void AddCard(Card card);

    }
}
