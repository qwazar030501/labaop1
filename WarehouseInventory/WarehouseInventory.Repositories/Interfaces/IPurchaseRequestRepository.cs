using System.Collections.Generic;
using WarehouseInventory.Models.Entities;

namespace WarehouseInventory.Repositories.Interfaces
{
    public interface IPurchaseRequestRepository
    {
        IReadOnlyList<PurchaseRequest> GetAll();
        void Add(PurchaseRequest request);
        void MarkProcessed(int id);
    }
}