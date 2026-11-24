using System;

namespace WarehouseInventory.Models.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal OutputPerCycle { get; private set; }

        private Product() { }

        public static Product Create(int id, string name, decimal outputPerCycle)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название продукции обязательно.", nameof(name));
            if (outputPerCycle <= 0)
                throw new ArgumentOutOfRangeException(nameof(outputPerCycle));

            return new Product
            {
                Id = id,
                Name = name.Trim(),
                OutputPerCycle = outputPerCycle
            };
        }
    }
}