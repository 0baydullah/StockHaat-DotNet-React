using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface IPortfolioRepo
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(AppUser user, string symbol);
    }
}
