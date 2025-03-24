using InventoryOrderManagement.Application.Common.Extensions;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Repositories;

public class QueryRepository<T> : IQueryRepository<T> where T : BaseEntity
{
    protected readonly QueryContext _context;
    
    public QueryRepository(QueryContext context)
    {
        _context = context;
    }
    
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ApplyIsDeletedFilter().ToListAsync(cancellationToken);
    }
    
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ApplyIsDeletedFilter().FirstOrDefaultAsync(predicate, cancellationToken);
    }
    
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ApplyIsDeletedFilter().Where(predicate).ToListAsync(cancellationToken);
    }
    
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ApplyIsDeletedFilter().AnyAsync(predicate, cancellationToken);
    }
    
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ApplyIsDeletedFilter().CountAsync(predicate, cancellationToken);
    }
    
    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().ApplyIsDeletedFilter().AsQueryable();
    }
} 