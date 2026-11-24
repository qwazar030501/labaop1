using System;
using System.Windows.Forms;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Repositories.Interfaces;

namespace WinFormsApp1.Products
{
    public class ProductEditForm : Form
    {
        private readonly IProductRepository _repository;
        private readonly Product? _existing;
        private readonly TextBox _name = new();
        private readonly NumericUpDown _outputPerCycle = new() { DecimalPlaces = 2, Maximum = 1000000 };

        public ProductEditForm(IProductRepository repository, Product? existing)
        {
            _repository = repository;
            _existing = existing;

            Text = existing == null ? "Новая продукция" : "Редактирование";
            Width = 400; Height = 220;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;

            var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(8) };
            table.RowCount = 2;
            table.Controls.Add(new Label { Text = "Название", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
            _name.Dock = DockStyle.Fill; table.Controls.Add(_name, 1, 0);
            table.Controls.Add(new Label { Text = "Выпуск за цикл", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
            _outputPerCycle.Dock = DockStyle.Fill; table.Controls.Add(_outputPerCycle, 1, 1);
            Controls.Add(table);

            var ok = new Button { Text = "Сохранить", Width = 100 };
            var cancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Width = 100 };
            var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40, FlowDirection = FlowDirection.RightToLeft };
            bottom.Controls.Add(cancel); bottom.Controls.Add(ok); Controls.Add(bottom);
            ok.Click += OnSave;

            if (_existing != null) { _name.Text = _existing.Name; _outputPerCycle.Value = _existing.OutputPerCycle; }
        }

        private void OnSave(object? sender, EventArgs e)
        {
            try
            {
                var id = _existing?.Id ?? 0;
                var product = Product.Create(id, _name.Text, _outputPerCycle.Value);
                if (_existing == null) _repository.Add(product); else _repository.Update(product);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}