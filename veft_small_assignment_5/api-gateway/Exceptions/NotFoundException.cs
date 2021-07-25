using System;

namespace api_gateway.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Model not found") {}
    }
}