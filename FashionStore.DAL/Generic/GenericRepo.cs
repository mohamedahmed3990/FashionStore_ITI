
using FashionStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.DAL.Generic;

public class GenericRepo<T> : IGenericRepo<T>
    where T : class
{
    private readonly AppDbContext _context;

    public GenericRepo(AppDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>()
            .Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>()
            .Remove(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>()
            .FindAsync(id);
    }

    public void Update(T entity)
    {
    }
}
