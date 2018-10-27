using System;

namespace ESportStatistics.Data.Models.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
