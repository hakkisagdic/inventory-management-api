using InventoryOrderManagement.Application.Common.Repositories;

namespace InventoryOrderManagement.Application.Common.CQS.Queries;

public interface IQueryContext : IEntityDbSet
{
    IQueryable<T> Set<T>() where T : class;
}