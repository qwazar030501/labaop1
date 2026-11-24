using System;
using WarehouseInventory.Models.Enums;

namespace WarehouseInventory.Models.Entities
{
    public class Material
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public MaterialCategory Category { get; private set; }
        public string Unit { get; private set; } = string.Empty;
        public decimal MinStock { get; private set; }
        public decimal ScrapPercent { get; private set; }
        public decimal CurrentStock { get; private set; }
        public string? Article { get; private set; } 

        private Material() { }

        public static Material Create(
            int id, string name, MaterialCategory category, string unit,
            decimal minStock, decimal scrapPercent, decimal currentStock,
            string? article = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название материала обязательно.", nameof(name));
            if (string.IsNullOrWhiteSpace(unit))
                throw new ArgumentException("Единица измерения обязательна.", nameof(unit));
            if (minStock < 0)
                throw new ArgumentOutOfRangeException(nameof(minStock));
            if (scrapPercent < 0 || scrapPercent > 100)
                throw new ArgumentOutOfRangeException(nameof(scrapPercent));
            if (currentStock < 0)
                throw new ArgumentOutOfRangeException(nameof(currentStock));

            return new Material
            {
                Id = id,
                Name = name.Trim(),
                Category = category,
                Unit = unit.Trim(),
                MinStock = minStock,
                ScrapPercent = scrapPercent,
                CurrentStock = currentStock,
                Article = string.IsNullOrWhiteSpace(article) ? null : article.Trim()
            };
        }
    }
}