using System.Collections.Generic;
using Exterminator.Models.Dtos;
using Exterminator.Models.InputModels;

namespace Exterminator.Repositories.Interfaces
{
    public interface IGhostbusterRepository
    {
         IEnumerable<GhostbusterDto> GetAllGhostbusters(string expertize);
         GhostbusterDto GetGhostbusterById(int id);
         int CreateGhostbuster(GhostbusterInputModel ghostbuster);
         bool DoesExist(int id);
    }
}