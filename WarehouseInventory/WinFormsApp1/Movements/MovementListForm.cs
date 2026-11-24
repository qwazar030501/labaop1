using System.Windows.Forms;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WinFormsApp1.Movements
{
    public class MovementListForm : Form
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly DataGridView _grid;

        public MovementListForm(IMovementRepository movementRepository, IMaterialRepository materialRepository)
        {
            _movementRepository = movementRepository;
            _materialRepository = materialRepository;
            Text = "Движение материалов"; Width = 800; Height = 500;

            _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            Controls.Add(_grid);

            var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40 };
            var btnAdd = new Button { Text = "Добавить", Width = 120 };
            var btnRefresh = new Button { Text = "Обновить", Width = 120 };

            btnAdd.Click += (_, _) => { if (new MovementCreateForm(_movementRepository, _materialRepository).ShowDialog() == DialogResult.OK) LoadData(); };
            btnRefresh.Click += (_, _) => LoadData();

            buttons.Controls.AddRange(new Control[] { btnAdd, btnRefresh });
            Controls.Add(buttons); LoadData();
        }

        private void LoadData() { _grid.DataSource = null; _grid.DataSource = _movementRepository.GetAll(); }
    }
}