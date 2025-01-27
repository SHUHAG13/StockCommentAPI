﻿using StockComment.Models;
using StockComment.Models.Dtos;

namespace StockComment.Interfaces
{
    public interface IStockInterface
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock> UpdateAsync(int id,UpdateStockRequestDto stock);
        Task<Stock> Delete(int id);

    }
}
