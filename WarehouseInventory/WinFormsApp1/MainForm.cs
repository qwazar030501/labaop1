using System;
using System.Linq;
using System.Windows.Forms;
using WarehouseInventory.IoC;
using WarehouseInventory.Repositories.Interfaces;
using WarehouseInventory.Repositories.Stubs;
using WinFormsApp1.Materials;
using WinFormsApp1.Movements;
using WinFormsApp1.Products;

namespace WinFormsApp1
{
    public class MainForm : Form
    {
        private readonly SimpleContainer _container;

        public MainForm(SimpleContainer container)
        {
            _container = container;
            _container.Register<IMaterialRepository, MaterialRepositoryStub>();
            _container.Register<IProductRepository, ProductRepositoryStub>();
            _container.Register<IMovementRepository, MovementRepositoryStub>();
            _container.Register<IProductMaterialRepository, ProductMaterialRepositoryStub>();
            _container.Register<IPurchaseRequestRepository, PurchaseRequestRepositoryStub>();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Учёт материальных запасов";
            Width = 900;
            Height = 600;
            IsMdiContainer = true;

            var menu = new MenuStrip();

            var dirMenu = new ToolStripMenuItem("Справочники");
            dirMenu.DropDownItems.Add("Материалы", null, (_, _) => OpenMdi<MaterialListForm>());
            dirMenu.DropDownItems.Add("Продукция", null, (_, _) => OpenMdi<ProductListForm>());
            dirMenu.DropDownItems.Add("Заявки на пополнение", null, (_, _) => OpenRequests());

            var opMenu = new ToolStripMenuItem("Операции");
            opMenu.DropDownItems.Add("Движение материалов", null, (_, _) => OpenMdi<MovementListForm>());

            var repMenu = new ToolStripMenuItem("Отчёты");

            menu.Items.Add(dirMenu);
            menu.Items.Add(opMenu);
            menu.Items.Add(repMenu);

            MainMenuStrip = menu;
            Controls.Add(menu);
        }

        private void OpenMdi<T>() where T : Form
        {
            try
            {
                var form = _container.Resolve<T>();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenRequests()
        {
            var repo = _container.Resolve<IPurchaseRequestRepository>();
            var matRepo = _container.Resolve<IMaterialRepository>();
            var requests = repo.GetAll();

            var message = requests.Count == 0
                ? "Заявок нет."
                : string.Join(Environment.NewLine, requests.Select(r =>
                {
                    var mat = matRepo.GetById(r.MaterialId);
                    return $"№{r.Id}: {mat?.Name ?? "?"} — {r.Quantity} ({r.CreatedAt:dd.MM.yyyy}) — " +
                           (r.IsProcessed ? $"обработана {r.ProcessedAt:dd.MM.yyyy}" : "ожидает");
                }));

            MessageBox.Show(message, "Заявки на пополнение");
        }
    }
}