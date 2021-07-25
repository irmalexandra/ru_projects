using System;

namespace api_gateway.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("A bad request") {}
    }
}