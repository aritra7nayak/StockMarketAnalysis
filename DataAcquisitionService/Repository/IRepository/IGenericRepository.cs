﻿using DataAcquisitionService.Models;

namespace DataAcquisitionService.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteForParent<TChild>(int parentId, string parentIdFieldName) where TChild : class;

    }
}
