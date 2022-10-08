using Microsoft.EntityFrameworkCore;
using Population.SQLite.App.Domain.Entities;

namespace Population.SQLite.App.Application.Common.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<Actuals> Actual { get; set; }
        DbSet<Estimates> Estimate { get; set; }
    }
}
