using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WarehouseInventory.Repositories.Stubs
{
    public class ProductMaterialRepositoryStub : IProductMaterialRepository
    {
        private readonly List<ProductMaterial> _items = new()
        {
            ProductMaterial.Create(1, 1, 1.2m),
            ProductMaterial.Create(1, 3, 4),
            ProductMaterial.Create(2, 2, 0.8m)
        };

        public IReadOnlyList<ProductMaterial> GetAll() => _items.ToList();
        public IReadOnlyList<ProductMaterial> GetByProduct(int productId) => _items.Where(pm => pm.ProductId == productId).ToList();

        public void Add(ProductMaterial link)
        {
            if (_items.Any(pm => pm.ProductId == link.ProductId && pm.MaterialId == link.MaterialId))
                throw new InvalidOperationException("Такая связка уже существует.");
            _items.Add(link);
        }

        public void Delete(int productId, int materialId) =>
            _items.RemoveAll(pm => pm.ProductId == productId && pm.MaterialId == materialId);
    }
}