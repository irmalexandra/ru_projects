using System;

namespace TechnicalRadiation.Models.Exceptions
{
    public class ArgumentOutOfRangeException : Exception
    {
        public ArgumentOutOfRangeException() : base("Argument is out of range.")
        {
            
        }

        public ArgumentOutOfRangeException(string message) : base(message)
        {
            
        }

        public ArgumentOutOfRangeException(string message, Exception inner) : base(message, inner)
        {
            
        }   
    }
}