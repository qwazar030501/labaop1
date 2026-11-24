using System;

namespace WarehouseInventory.Models.Entities
{
    public class PurchaseRequest
    {
        public int Id { get; private set; }
        public int MaterialId { get; private set; }
        public decimal Quantity { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsProcessed { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public string? Comment { get; private set; }

        private PurchaseRequest() { }

        public static PurchaseRequest Create(int id, int materialId, decimal quantity,
                                             DateTime createdAt, bool isProcessed,
                                             DateTime? processedAt = null, string? comment = null)
        {
            if (materialId <= 0) throw new ArgumentOutOfRangeException(nameof(materialId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (isProcessed && processedAt == null)
                throw new ArgumentException("Для обработанной заявки требуется дата обработки.");

            return new PurchaseRequest
            {
                Id = id,
                MaterialId = materialId,
                Quantity = quantity,
                CreatedAt = createdAt,
                IsProcessed = isProcessed,
                ProcessedAt = processedAt,
                Comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim()
            };
        }
    }
}