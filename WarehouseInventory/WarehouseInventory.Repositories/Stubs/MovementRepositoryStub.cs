using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Models.Enums;
using WarehouseInventory.Repositories.Interfaces;

namespace WarehouseInventory.Repositories.Stubs
{
    public class MovementRepositoryStub : IMovementRepository
    {
        private readonly List<Movement> _items = new()
        {
            Movement.Create(1, 1, MovementType.Income, 200, DateTime.Today.AddDays(-5), "НАКЛ-001"),
            Movement.Create(2, 2, MovementType.Expense, 30, DateTime.Today.AddDays(-2)),
            Movement.Create(3, 1, MovementType.ScrapReturn, 5, DateTime.Today.AddDays(-1))
        };
        private int _nextId = 4;

        public IReadOnlyList<Movement> GetAll() => _items.ToList();
        public IReadOnlyList<Movement> GetByMaterial(int materialId) => _items.Where(m => m.MaterialId == materialId).ToList();

        public void Add(Movement movement)
        {
            var created = Movement.Create(_nextId++, movement.MaterialId, movement.Type,
                movement.Quantity, movement.Date, movement.DocumentNumber);
            _items.Add(created);
        }
    }
}