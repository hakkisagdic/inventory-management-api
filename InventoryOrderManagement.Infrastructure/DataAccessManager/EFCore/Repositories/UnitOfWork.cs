using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CommandContext _context;

    public UnitOfWork(CommandContext context)
    {
        _context = context;
    }
    
    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}