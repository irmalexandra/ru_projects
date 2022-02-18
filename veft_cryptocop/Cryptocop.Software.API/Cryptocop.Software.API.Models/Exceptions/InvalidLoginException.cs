using System;

namespace TechnicalRadiation.Models.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Login information was incorrect.")
        {
            
        }

        public InvalidLoginException(string message) : base(message)
        {
            
        }
        
        public InvalidLoginException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}