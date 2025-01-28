using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockComment.Data;
using StockComment.Interfaces;
using StockComment.Models;
using StockComment.Models.Dtos;

namespace StockComment.Repository
{
    public class StockRepository : IStockInterface
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
           return await _context.Stocks.FindAsync(id);
        }
        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stock)
        {
            var existingStock= await _context.Stocks.FindAsync(id);
            if(existingStock==null)
            {
                return null;
            }
            existingStock.Symbol=stock.Symbol;
            existingStock.MarketCap = stock.MarketCap;
            existingStock.Purchase = stock.Purchase;
            existingStock.Industry=stock.Industry;
            existingStock.CompanyName = stock.CompanyName;
            existingStock.LastDiv=stock.LastDiv;
            await _context.SaveChangesAsync();
            return existingStock;



        }
        public async Task<Stock> Delete(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}
