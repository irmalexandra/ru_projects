using System;

namespace Cryptocop.Software.API.Repositories.Helpers
{
    public static class PaymentCardHelper
    {
        public static string MaskPaymentCard(this string paymentCard, int start, int maskLength, char maskCharacter = '*')
        {
            if (start > paymentCard.Length -1)
            {
                throw new ArgumentException("Start position is greater than string length");
            }

            if (maskLength > paymentCard.Length)
            {
                throw new ArgumentException("Mask length is greater than string length");
            }

            if (start + maskLength > paymentCard.Length)
            {
                throw new ArgumentException("Start position and mask length imply more characters than are present");
            }

            var mask = new string(maskCharacter, maskLength);
            var unMaskStart = paymentCard.Substring(0, start);
            var unMaskEnd = paymentCard.Substring(start + maskLength, paymentCard.Length - maskLength);

            return unMaskStart + mask + unMaskEnd;
        }
    }
}