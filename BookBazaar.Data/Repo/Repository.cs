﻿using System.Linq.Expressions;
using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookBazaar.Data.Repo;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDataContext _context;
    private readonly DbSet<T> _dbSet;

    protected Repository(AppDataContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<ICollection<T>> RetrieveAllAsync(Expression<Func<T, bool>>? filter = null,
        string? includedProperties = null)
    {
        IQueryable<T> queryable = _dbSet;

        if (includedProperties is not null)
        {
            foreach (var property in
                     includedProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryable = queryable.Include(property);
            }
        }

        if (filter is null)
        {
            return await queryable.ToListAsync();
        }

        return await queryable.Where(filter).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includedProperties = null,
        bool detached = false)
    {
        IQueryable<T> queryable = _dbSet;
        queryable = queryable.Where(filter);


        if (includedProperties is not null)
        {
            foreach (var property in
                     includedProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryable = queryable.Include(property);
            }
        }

        T? entity = await queryable.FirstOrDefaultAsync();

        if (detached && entity is not null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity!;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}