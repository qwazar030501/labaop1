using System.Collections.Generic;
using WarehouseInventory.Models.Entities;

namespace WarehouseInventory.Repositories.Interfaces
{
    public interface IMaterialRepository
    {
        IReadOnlyList<Material> GetAll();
        Material? GetById(int id);
        void Add(Material material);
        void Update(Material material);
        void Delete(int id);
    }
}