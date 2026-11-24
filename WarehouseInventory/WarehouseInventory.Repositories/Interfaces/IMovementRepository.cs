using System.Collections.Generic;
using WarehouseInventory.Models.Entities;

namespace WarehouseInventory.Repositories.Interfaces
{
    public interface IMovementRepository
    {
        IReadOnlyList<Movement> GetAll();
        IReadOnlyList<Movement> GetByMaterial(int materialId);
        void Add(Movement movement);
    }
}