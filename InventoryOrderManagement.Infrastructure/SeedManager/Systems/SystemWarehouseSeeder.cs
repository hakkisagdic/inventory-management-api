using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core;
using System.Linq;

namespace Infrastructure.SeedManager.Systems
{
    public class SystemWarehouseSeeder
    {
        private readonly ICommandRepository<Warehouse> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueryRepository<Warehouse> _queryRepository;

        public SystemWarehouseSeeder(
            ICommandRepository<Warehouse> repository,
            IUnitOfWork unitOfWork,
            IQueryRepository<Warehouse> queryRepository
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _queryRepository = queryRepository;
        }

        public async Task GenerateDataAsync()
        {
            // Aynı isimde depo kayıtları var mı diye kontrol et
            var existingWarehouses = await _queryRepository.GetAllAsync();
            var existingWarehouseNames = existingWarehouses.Select(w => w.Name).ToHashSet();
            
            var warehousesToCreate = new List<Warehouse>();
            
            var systemWarehouses = new List<Warehouse>
            {
                new Warehouse { Name = "Customer", SystemWarehouse = true },
                new Warehouse { Name = "Vendor", SystemWarehouse = true },
                new Warehouse { Name = "Transfer", SystemWarehouse = true },
                new Warehouse { Name = "Adjustment", SystemWarehouse = true },
                new Warehouse { Name = "StockCount", SystemWarehouse = true },
                new Warehouse { Name = "Scrapping", SystemWarehouse = true }
            };

            // Sadece mevcut olmayan depoları ekle
            foreach (var warehouse in systemWarehouses)
            {
                if (!existingWarehouseNames.Contains(warehouse.Name))
                {
                    warehousesToCreate.Add(warehouse);
                }
            }
            
            // Eklenecek depolar varsa ekle
            if (warehousesToCreate.Any())
            {
                foreach (var warehouse in warehousesToCreate)
                {
                    await _repository.CreateAsync(warehouse);
                }
                
                await _unitOfWork.SaveAsync();
            }
        }
    }
}