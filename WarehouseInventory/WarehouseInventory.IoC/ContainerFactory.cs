using WarehouseInventory.Repositories.Interfaces;
using WarehouseInventory.Repositories.Stubs;

namespace WarehouseInventory.IoC
{
    public static class ContainerFactory
    {
        public static SimpleContainer Build()
        {
            var container = new SimpleContainer();

            container.Register<IMaterialRepository, MaterialRepositoryStub>();
            container.Register<IProductRepository, ProductRepositoryStub>();
            container.Register<IMovementRepository, MovementRepositoryStub>();
            container.Register<IProductMaterialRepository, ProductMaterialRepositoryStub>();
            container.Register<IPurchaseRequestRepository, PurchaseRequestRepositoryStub>();

            return container;
        }
    }
}