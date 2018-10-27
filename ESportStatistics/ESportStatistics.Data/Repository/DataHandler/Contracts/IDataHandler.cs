using ESportStatistics.Data.Models;
using ESportStatistics.Data.Repository.Contracts;
using System;

namespace ESportStatistics.Data.Repository.DataHandler.Contracts
{
    public interface IDataHandler : IDisposable
    {
        IDeletableGenericRepository<Spell> Spells { get; }

        IDeletableGenericRepository<Item> Items { get; }

        IDeletableGenericRepository<Champion> Champions { get; }

        IDeletableGenericRepository<Serie> Series { get; }

        IDeletableGenericRepository<Mastery> Masteries { get; }

        IDeletableGenericRepository<Player> Players { get; }

        IDeletableGenericRepository<Team> Teams { get; }

        IDeletableGenericRepository<Tournament> Tournaments { get; }

        IDeletableGenericRepository<League> Leagues { get; }

        IDeletableGenericRepository<Match> Matches { get; }

        int SaveChanges();
    }
}
