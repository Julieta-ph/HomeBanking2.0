using HomeBanking2._0.Models;
    
 namespace HomeBanking2._0.Repositories
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAllCards();
        IEnumerable<Card> GetAllCardsByClient(long clientId);

        IEnumerable<Card> GetAllCardsByType(long clientId, string type); //por id de cliente y por tipo de tarjeta

        Card GetCardById(long id);

        Card GetCardByNumber(long clientId, string number); //por id de cliente y por numero de tarjeta

        void AddCard(Card card);
    }
}
