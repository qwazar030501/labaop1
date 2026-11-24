using System.Collections.Generic;
using WarehouseInventory.Models.Entities;

namespace WarehouseInventory.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IReadOnlyList<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}