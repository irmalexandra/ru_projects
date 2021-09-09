using System;
using System.Collections.Generic;
using System.Linq;
using Exterminator.Models;
using Exterminator.Models.Dtos;
using Exterminator.Models.Entities;
using Exterminator.Repositories.Data;
using Exterminator.Repositories.Interfaces;

namespace Exterminator.Repositories.Implementations
{
    public class LogRepository : ILogRepository
    {
        private readonly LogDbContext _dbContext = new LogDbContext();

        public void LogToDatabase(ExceptionModel exception)
        {
            _dbContext.Logs.Add(new Log
            {
                ExceptionMessage = exception.ExceptionMessage,
                StackTrace = exception.StackTrace,
                Timestamp = DateTime.Now
            });
            _dbContext.SaveChanges();
        }
        
        public IEnumerable<LogDto> GetAllLogs()
        {
            using (var db = _dbContext)
            {
                return (from log in db.Logs
                    orderby log.Timestamp descending 
                    select new LogDto
                    {
                        Id = log.Id,
                        ExceptionMessage = log.ExceptionMessage,
                        StackTrace = log.StackTrace,
                        Timestamp = log.Timestamp

                    }).ToList();
            }
           
        }
    }
}
