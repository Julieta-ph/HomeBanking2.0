using HomeBanking2._0.Models;
using HomeBanking2._0.Repositories;
using System.Security.Cryptography;

namespace HomeBanking2._0.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public void AddCard(Card card)
        {
            try
            {
                _cardRepository.AddCard(card);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo guardar la tarjeta");
            }

        }

        public int GenerateCVV()
        {
            return RandomNumberGenerator.GetInt32(100, 999);
        }

        public string GenerateNumberCard(long clientId)
        {
            var numberCardRandom = "";
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    numberCardRandom += RandomNumberGenerator.GetInt32(1000, 9999);
                    if (1 < 3)
                    {
                        numberCardRandom += "-";
                    }
                }

                //buscar como agregar que le primer bloque de 4 sea el mismo para nombrar el banco

                // el segundo bloque sea el mismo para credito o debito 

                // sacar el guion del ultimo bloque


            } while (_cardRepository.GetCardByNumber(clientId, numberCardRandom) != null);

            return numberCardRandom;
        }

        public IEnumerable<Card> GetAllCardsByClient(long clientId)
        {
            try
            {
                return _cardRepository.GetAllCardsByClient(clientId);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo guardar la tarjeta");
            }
        }

        public IEnumerable<Card> GetAllCardsByType(long clientId, string type)
        {
            try
            {
                return _cardRepository.GetAllCardsByType(clientId, type);
            }
            catch
            {
                throw new Exception("No se pudieron obtener las tarjetas");
            }
        }

        
    }
}
