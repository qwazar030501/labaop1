using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Models.Enums;
using WarehouseInventory.Repositories.Interfaces;

namespace WarehouseInventory.Repositories.Stubs
{
    public class MaterialRepositoryStub : IMaterialRepository
    {
        private readonly List<Material> _items = new()
        {
            Material.Create(1, "Сталь листовая", MaterialCategory.Raw, "кг", 100, 2.5m, 320),
            Material.Create(2, "Пластик ABS", MaterialCategory.Raw, "кг", 50, 4.0m, 40),
            Material.Create(3, "Болт М8", MaterialCategory.Component, "шт", 500, 1.0m, 800)
        };
        private int _nextId = 4;

        public IReadOnlyList<Material> GetAll() => _items.ToList();
        public Material? GetById(int id) => _items.FirstOrDefault(m => m.Id == id);

        public void Add(Material material)
        {
            if (_items.Any(m => string.Equals(m.Name, material.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Материал с таким названием уже существует.");

            var created = Material.Create(_nextId++, material.Name, material.Category, material.Unit,
                material.MinStock, material.ScrapPercent, material.CurrentStock, material.Article);
            _items.Add(created);
        }

        public void Update(Material material)
        {
            if (_items.Any(m => m.Id != material.Id && string.Equals(m.Name, material.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Материал с таким названием уже существует.");

            var index = _items.FindIndex(m => m.Id == material.Id);
            if (index >= 0) _items[index] = material;
        }

        public void Delete(int id) => _items.RemoveAll(m => m.Id == id);
    }
}