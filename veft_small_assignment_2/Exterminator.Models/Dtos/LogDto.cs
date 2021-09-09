using System;

namespace Exterminator.Models.Dtos
{
    public class LogDto
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
    }
}