using Exterminator.Repositories.Implementations;
using Exterminator.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Exterminator.WebApi.Controllers
{
    
    [Route("api/logs")]
    public class LogController : Controller
    {
        private LogService _logService;
        
        public LogController()
        {
            var logRepository = new LogRepository();
            _logService = new LogService(logRepository);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllLogs()
        {
            return Ok(_logService.GetAllLogs());
        }
    }
}