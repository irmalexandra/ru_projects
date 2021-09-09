using System.Collections.Generic;
using Exterminator.Models;
using Exterminator.Models.Dtos;

namespace Exterminator.Repositories.Interfaces
{
    public interface ILogRepository
    {
         void LogToDatabase(ExceptionModel exception);
         IEnumerable<LogDto> GetAllLogs();
    }
}