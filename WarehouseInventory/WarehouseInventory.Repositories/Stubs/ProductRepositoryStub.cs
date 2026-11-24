using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WarehouseInventory.Repositories.Stubs
{
    public class ProductRepositoryStub : IProductRepository
    {
        private readonly List<Product> _items = new()
        {
            Product.Create(1, "Кронштейн", 100),
            Product.Create(2, "Корпус насоса", 50)
        };
        private int _nextId = 3;

        public IReadOnlyList<Product> GetAll() => _items.ToList();
        public Product? GetById(int id) => _items.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            if (_items.Any(p => string.Equals(p.Name, product.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Продукция с таким названием уже существует.");

            var created = Product.Create(_nextId++, product.Name, product.OutputPerCycle);
            _items.Add(created);
        }

        public void Update(Product product)
        {
            if (_items.Any(p => p.Id != product.Id && string.Equals(p.Name, product.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Продукция с таким названием уже существует.");

            var index = _items.FindIndex(p => p.Id == product.Id);
            if (index >= 0) _items[index] = product;
        }

        public void Delete(int id) => _items.RemoveAll(p => p.Id == id);
    }
}