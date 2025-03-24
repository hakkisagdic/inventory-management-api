using InventoryOrderManagement.Application.Common.CQS.Queries;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;

public class QueryContext : DataContext, IQueryContext
{
    public QueryContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    public IQueryable<T> Set<T>() where T : class
    {
        return base.Set<T>();
    }
}