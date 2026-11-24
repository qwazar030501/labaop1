using System;
using WarehouseInventory.Models.Enums;

namespace WarehouseInventory.Models.Entities
{
    public class Movement
    {
        public int Id { get; private set; }
        public int MaterialId { get; private set; }
        public MovementType Type { get; private set; }
        public decimal Quantity { get; private set; }
        public DateTime Date { get; private set; }
        public string? DocumentNumber { get; private set; }

        private Movement() { }

        public static Movement Create(int id, int materialId, MovementType type,
                                      decimal quantity, DateTime date,
                                      string? documentNumber = null)
        {
            if (materialId <= 0) throw new ArgumentOutOfRangeException(nameof(materialId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (date == default) throw new ArgumentException("Дата обязательна.", nameof(date));

            return new Movement
            {
                Id = id,
                MaterialId = materialId,
                Type = type,
                Quantity = quantity,
                Date = date,
                DocumentNumber = string.IsNullOrWhiteSpace(documentNumber) ? null : documentNumber.Trim()
            };
        }
    }
}