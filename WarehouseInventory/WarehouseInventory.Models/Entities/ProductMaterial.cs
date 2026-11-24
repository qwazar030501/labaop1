using System;

namespace WarehouseInventory.Models.Entities
{
    public class ProductMaterial
    {
        public int ProductId { get; private set; }
        public int MaterialId { get; private set; }
        public decimal QuantityPerUnit { get; private set; }

        private ProductMaterial() { }

        public static ProductMaterial Create(int productId, int materialId, decimal quantityPerUnit)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (materialId <= 0) throw new ArgumentOutOfRangeException(nameof(materialId));
            if (quantityPerUnit <= 0) throw new ArgumentOutOfRangeException(nameof(quantityPerUnit));

            return new ProductMaterial
            {
                ProductId = productId,
                MaterialId = materialId,
                QuantityPerUnit = quantityPerUnit
            };
        }
    }
}