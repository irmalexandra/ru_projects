using System.Collections.Generic;
using Exterminator.Models.Entities;

namespace Exterminator.Repositories.Data
{
    public class GhostbusterDbContext : IGhostbusterDbContext
    {
        private readonly List<Ghostbuster> _ghostBusters = new List<Ghostbuster> 
        {
            new Ghostbuster
            {
                Id = 1,
                Name = "Mr. T",
                Expertize = "Ghost catcher"
            },
            new Ghostbuster
            {
                Id = 2,
                Name = "Mr. B",
                Expertize = "Ghoul strangler"
            },
            new Ghostbuster
            {
                Id = 3,
                Name = "Mr. C",
                Expertize = "Monster encager"
            },
            new Ghostbuster
            {
                Id = 4,
                Name = "Mr. S",
                Expertize = "Zombie exploder"
            }
        };

        public List<Ghostbuster> Ghostbusters => _ghostBusters;
    }
}