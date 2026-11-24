using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WarehouseInventory.Repositories.Stubs
{
    public class PurchaseRequestRepositoryStub : IPurchaseRequestRepository
    {
        private readonly List<PurchaseRequest> _items = new()
        {
            PurchaseRequest.Create(1, 2, 60, DateTime.Today.AddDays(-3), false)
        };
        private int _nextId = 2;

        public IReadOnlyList<PurchaseRequest> GetAll() => _items.ToList();

        public void Add(PurchaseRequest request)
        {
            if (_items.Any(r => r.MaterialId == request.MaterialId && !r.IsProcessed))
                throw new InvalidOperationException("По материалу уже есть активная заявка.");

            var created = PurchaseRequest.Create(_nextId++, request.MaterialId, request.Quantity,
                request.CreatedAt, request.IsProcessed, request.ProcessedAt, request.Comment);
            _items.Add(created);
        }

        public void MarkProcessed(int id)
        {
            var index = _items.FindIndex(r => r.Id == id);
            if (index < 0) return;
            var r = _items[index];
            if (r.IsProcessed) return;
            _items[index] = PurchaseRequest.Create(r.Id, r.MaterialId, r.Quantity, r.CreatedAt, true, DateTime.Now, r.Comment);
        }
    }
}