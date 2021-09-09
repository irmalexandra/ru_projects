using System.Collections.Generic;
using Exterminator.Models.Entities;

namespace Exterminator.Repositories.Data
{
    public interface IGhostbusterDbContext
    {
        List<Ghostbuster> Ghostbusters { get; }
    }
}