using System.Collections.Generic;
using Exterminator.Models;
using Exterminator.Models.Dtos;

namespace Exterminator.Services.Interfaces
{
    public interface ILogService
    {
         void LogToDatabase(ExceptionModel exception);
         // TODO: Should contain a method which retrieves all logs (LogDto) ordered by timestamp (descending)
    }
}