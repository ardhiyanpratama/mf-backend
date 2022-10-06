﻿using mf_backend.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mf_backend.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //protected InstaContext _context;
        protected readonly ILogger _logger;
        protected DbSet<T> dbSet;

        public GenericRepository(ILogger logger)
        {
            //_context = context;
            //_logger = logger;
            //dbSet = _context.Set<T>();
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public virtual async Task<bool> AddRangeAsync(List<T> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var existing = await dbSet.FindAsync(id);
                if (existing is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }

                dbSet.Remove(existing);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var allProperties = typeof(T).GetProperties();
                var keyProperty = allProperties.FirstOrDefault(p => p.IsDefined(typeof(KeyAttribute), true));

                return keyProperty is null ? await dbSet.ToListAsync() : await dbSet.OrderBy(p => EF.Property<object>(p, keyProperty.Name)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<T>();
            }
        }

        public virtual async Task<T> GetAsync(long id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public virtual Task<bool> UpdateAsync(T entity)
        {
            try
            {
                dbSet.Update(entity);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateRangeAsync(List<T> entities)
        {
            try
            {
                dbSet.UpdateRange(entities);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}
