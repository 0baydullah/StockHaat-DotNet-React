using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, StockUpdateDto stockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
