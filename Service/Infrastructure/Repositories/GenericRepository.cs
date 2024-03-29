﻿using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UserDbContext _context;
        protected DbSet<T> _dbSet;
        protected IMemoryCache _cache;
        protected MemoryCacheEntryOptions _options;

        public GenericRepository(UserDbContext context, IMemoryCache cache)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _options = new MemoryCacheEntryOptions();
            _options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            _options.SlidingExpiration = TimeSpan.FromMinutes(10);
            _cache = cache;
        }
        public virtual void Add(T entity)
        {
           _dbSet.Add(entity);
        }

        public virtual void AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual List<T>? Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var result = _dbSet.Where(predicate).ToList();
            if (result == null) { return null; }
            return result;
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(string id)
        {
            var result = _dbSet.Find(id);
            if (result == null) { return null; }
            return result;
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
