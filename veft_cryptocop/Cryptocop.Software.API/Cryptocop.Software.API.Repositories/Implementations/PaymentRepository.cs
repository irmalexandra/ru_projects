using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Interfaces;
using TechnicalRadiation.Models.Exceptions;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;

        public PaymentRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public PaymentCardDto AddPaymentCard(string email, PaymentCardInputModel paymentCard)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (currentUser == null) { throw new ResourceNotFoundException("User not found"); }

            var duplicate = _dbContext.PaymentCards.FirstOrDefault(card => card.CardNumber == paymentCard.CardNumber);
            if (duplicate != null) { throw new ResourceAlreadyExistsException(); }

            var paymentCardEntity = _mapper.Map<PaymentCard>(paymentCard);
            paymentCardEntity.UserId = currentUser.Id;
            _dbContext.PaymentCards.Add(paymentCardEntity);
            _dbContext.SaveChanges();

            return _mapper.Map<PaymentCardDto>(paymentCardEntity);
        }

        public IEnumerable<PaymentCardDto> GetStoredPaymentCards(string email)
        {
            return _dbContext.PaymentCards.Where(card => card.User.Email == email)
                .Select(card => _mapper.Map<PaymentCardDto>(card)).ToList();
        }

        public string GetPaymentCardNumber(int paymentCardId)
        {
            return _mapper.Map<PaymentCardDto>(
                _dbContext.PaymentCards.FirstOrDefault(card => card.Id == paymentCardId)).CardNumber;
        }
    }
}