using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        PaymentCardDto AddPaymentCard(string email, PaymentCardInputModel paymentCard);
        IEnumerable<PaymentCardDto> GetStoredPaymentCards(string email);

        public string GetPaymentCardNumber(int paymentCardId);
    }
}