using System;
using System.Windows.Forms;
using WarehouseInventory.IoC;
using WarehouseInventory.Repositories.Interfaces;
using WarehouseInventory.Repositories.Stubs;

namespace WinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var container = new SimpleContainer();
            container.Register<IMaterialRepository, MaterialRepositoryStub>();
            container.Register<IProductRepository, ProductRepositoryStub>();
            container.Register<IMovementRepository, MovementRepositoryStub>();
            container.Register<IProductMaterialRepository, ProductMaterialRepositoryStub>();
            container.Register<IPurchaseRequestRepository, PurchaseRequestRepositoryStub>();
            Application.Run(container.Resolve<MainForm>());
        }
    }
}