using InventoryOrderManagement.Application.Common.Extensions;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Repositories;

public class CommandRepository<T> : ICommandRepository<T> where T : BaseEntity
{
    protected readonly CommandContext _context;
    
    public CommandRepository(CommandContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAtUtc = DateTime.UtcNow;
        await _context.AddAsync(entity, cancellationToken);
    }

    public void Create(T entity)
    {
        entity.CreatedAtUtc = DateTime.UtcNow;
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedAtUtc = DateTime.UtcNow;
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedAtUtc = DateTime.UtcNow;
        _context.Update(entity);
    }

    public void Purge(T entity)
    {
        _context.Remove(entity);
    }

    public virtual async Task<T?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(id, out var guidId))
        {
            return await _context.Set<T>().ApplyIsDeletedFilter().SingleOrDefaultAsync(x => x.Id == guidId, cancellationToken);
        }
        return null;
    }

    public T? Get(string id)
    {
        if (Guid.TryParse(id, out var guidId))
        {
            return _context.Set<T>().ApplyIsDeletedFilter().SingleOrDefault(x => x.Id == guidId);
        }
        return null;
    }

    public virtual IQueryable<T> GetQuery()
    {
        return _context.Set<T>().AsQueryable();
    }
}