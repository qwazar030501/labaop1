using System;
using System.Linq;
using System.Windows.Forms;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Models.Enums;
using WarehouseInventory.Repositories.Interfaces;

namespace WinFormsApp1.Materials
{
    public class MaterialEditForm : Form
    {
        private readonly IMaterialRepository _repository;
        private readonly Material? _existing;
        private readonly TextBox _name = new();
		private readonly CheckedListBox _category = new();
		private readonly TextBox _unit = new();
        private readonly NumericUpDown _minStock = new() { DecimalPlaces = 2, Maximum = 1000000 };
        private readonly NumericUpDown _scrapPercent = new() { DecimalPlaces = 2, Maximum = 100 };
        private readonly NumericUpDown _currentStock = new() { DecimalPlaces = 2, Maximum = 1000000 };
        private readonly TextBox _article = new();

        public MaterialEditForm(IMaterialRepository repository, Material? existing)
        {
            _repository = repository;
            _existing = existing;

            Text = existing == null ? "Новый материал" : "Редактирование";
            Width = 420; Height = 400;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;

			_category.CheckOnClick = true; 
			_category.Items.AddRange(Enum.GetValues(typeof(MaterialCategory)).Cast<object>()
				.Where(x => (MaterialCategory)x != MaterialCategory.None).ToArray());

			var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(8) };
            AddRow(table, "Название", _name); AddRow(table, "Артикул", _article);
            AddRow(table, "Категория", _category); AddRow(table, "Ед. изм.", _unit);
            AddRow(table, "Мин. остаток", _minStock); AddRow(table, "Брак, %", _scrapPercent);
            AddRow(table, "Остаток", _currentStock);
            Controls.Add(table);

            var ok = new Button { Text = "Сохранить", Width = 100 };
            var cancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Width = 100 };
            var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40, FlowDirection = FlowDirection.RightToLeft };
            bottom.Controls.Add(cancel); bottom.Controls.Add(ok); Controls.Add(bottom);
            ok.Click += OnSave;

            if (_existing != null)
            {
                _name.Text = _existing.Name; _article.Text = _existing.Article ?? "";
				for (int i = 0; i < _category.Items.Count; i++)
				{
					var cat = (MaterialCategory)_category.Items[i];
					if ((_existing.Category & cat) == cat) 
					{
						_category.SetItemChecked(i, true);
					}
				}
				_unit.Text = _existing.Unit;
                _minStock.Value = _existing.MinStock; _scrapPercent.Value = _existing.ScrapPercent; _currentStock.Value = _existing.CurrentStock;
            }
            else _category.SelectedIndex = 0;
        }

        private static void AddRow(TableLayoutPanel panel, string label, Control control)
        {
            panel.RowCount++;
            panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, panel.RowCount - 1);
            control.Dock = DockStyle.Fill; panel.Controls.Add(control, 1, panel.RowCount - 1);
        }

        private void OnSave(object? sender, EventArgs e)
        {
            try
            {
                var id = _existing?.Id ?? 0;
				var category = MaterialCategory.None;
				foreach (var item in _category.CheckedItems)
				{
					category |= (MaterialCategory)item; 
				}
				var material = Material.Create(id, _name.Text, category, _unit.Text, _minStock.Value, _scrapPercent.Value, _currentStock.Value, _article.Text);

                if (_existing == null) _repository.Add(material); else _repository.Update(material);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}