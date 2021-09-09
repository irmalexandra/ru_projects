using System.Collections.Generic;
using Exterminator.Models.Dtos;
using Exterminator.Models.InputModels;

namespace Exterminator.Services.Interfaces
{
    public interface IGhostbusterService
    {
         IEnumerable<GhostbusterDto> GetAllGhostbusters(string expertize = "");
         GhostbusterDto GetGhostbusterById(int id);
         int CreateGhostbuster(GhostbusterInputModel ghostbuster);
    }
}