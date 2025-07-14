using api.Models;
using System.Collections.Generic;

namespace api.Interfaces
{
    public interface IPortfolioRepo
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}
