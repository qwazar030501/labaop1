using System.Collections.Generic;
using WarehouseInventory.Models.Entities;

namespace WarehouseInventory.Repositories.Interfaces
{
    public interface IProductMaterialRepository
    {
        IReadOnlyList<ProductMaterial> GetAll();
        IReadOnlyList<ProductMaterial> GetByProduct(int productId);
        void Add(ProductMaterial link);
        void Delete(int productId, int materialId);
    }
}