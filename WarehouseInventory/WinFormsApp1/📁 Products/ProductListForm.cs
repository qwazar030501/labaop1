using System.Windows.Forms;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WinFormsApp1.Products
{
    public class ProductListForm : Form
    {
        private readonly IProductRepository _repository;
        private readonly DataGridView _grid;

        public ProductListForm(IProductRepository repository)
        {
            _repository = repository;
            Text = "Продукция"; Width = 800; Height = 500;

            _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            Controls.Add(_grid);

            var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40 };
            var btnAdd = new Button { Text = "Добавить", Width = 120 };
            var btnEdit = new Button { Text = "Редактировать", Width = 120 };
            var btnDelete = new Button { Text = "Удалить", Width = 120 };
            var btnRefresh = new Button { Text = "Обновить", Width = 120 };

            btnAdd.Click += (_, _) => { if (new ProductEditForm(_repository, null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click += (_, _) => { if (_grid.CurrentRow?.DataBoundItem is Product p && new ProductEditForm(_repository, p).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click += (_, _) =>
            {
                if (_grid.CurrentRow?.DataBoundItem is Product p && MessageBox.Show($"Удалить '{p.Name}'?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { _repository.Delete(p.Id); LoadData(); }
            };
            btnRefresh.Click += (_, _) => LoadData();

            buttons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });
            Controls.Add(buttons); LoadData();
        }

        private void LoadData() { _grid.DataSource = null; _grid.DataSource = _repository.GetAll(); }
    }
}