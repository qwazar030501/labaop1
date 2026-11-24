using System.Windows.Forms;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WinFormsApp1.Materials
{
    public class MaterialListForm : Form
    {
        private readonly IMaterialRepository _repository;
        private readonly DataGridView _grid;

        public MaterialListForm(IMaterialRepository repository)
        {
            _repository = repository;
            Text = "Материалы";
            Width = 800;
            Height = 500;

            _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            Controls.Add(_grid);

            var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40 };
            var btnAdd = new Button { Text = "Добавить", Width = 120 };
            var btnEdit = new Button { Text = "Редактировать", Width = 120 };
            var btnDelete = new Button { Text = "Удалить", Width = 120 };
            var btnRefresh = new Button { Text = "Обновить", Width = 120 };

            btnAdd.Click += (_, _) => { if (new MaterialEditForm(_repository, null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click += (_, _) => { if (_grid.CurrentRow?.DataBoundItem is Material m && new MaterialEditForm(_repository, m).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click += (_, _) =>
            {
                if (_grid.CurrentRow?.DataBoundItem is Material m && MessageBox.Show($"Удалить '{m.Name}'?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { _repository.Delete(m.Id); LoadData(); }
            };
            btnRefresh.Click += (_, _) => LoadData();

            buttons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });
            Controls.Add(buttons);
            LoadData();
        }

        private void LoadData() { _grid.DataSource = null; _grid.DataSource = _repository.GetAll(); }
    }
}