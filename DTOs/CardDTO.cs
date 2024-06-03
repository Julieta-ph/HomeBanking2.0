using HomeBanking2._0.Models;

namespace HomeBanking2._0.DTOs
{
    public class CardDTO
    {
        public long Id { get; set; }
        public string CardHolder { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
        public int Cvv { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public CardDTO(Card Card)
        {
            Id = Card.Id;
            CardHolder = Card.CardHolder;
            Type = Card.Type;
            Color = Card.Color;
            Number = Card.Number;
            Cvv = Card.Cvv;
            FromDate = Card.FromDate;
            ThruDate = Card.ThruDate;


        }
    }
}
