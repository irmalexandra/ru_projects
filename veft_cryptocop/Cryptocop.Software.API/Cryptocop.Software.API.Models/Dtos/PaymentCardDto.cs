namespace Cryptocop.Software.API.Models.Dtos
{
    public class PaymentCardDto
    {
        public int Id { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}