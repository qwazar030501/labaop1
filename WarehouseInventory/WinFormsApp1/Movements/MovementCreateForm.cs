using System;
using System.Windows.Forms;
using WarehouseInventory.Models.Entities;
using WarehouseInventory.Models.Enums;
using WarehouseInventory.Repositories.Interfaces;

namespace WinFormsApp1.Movements
{
    public class MovementCreateForm : Form
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMaterialRepository _materialRepository;

        private readonly ComboBox _material = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _type = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly NumericUpDown _quantity = new() { DecimalPlaces = 2, Maximum = 1000000 };
        private readonly DateTimePicker _date = new() { Format = DateTimePickerFormat.Short };
        private readonly TextBox _docNumber = new();

        public MovementCreateForm(IMovementRepository movementRepository, IMaterialRepository materialRepository)
        {
            _movementRepository = movementRepository;
            _materialRepository = materialRepository;

            Text = "Новое движение"; Width = 400; Height = 300;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;

            _material.DataSource = _materialRepository.GetAll();
            _material.DisplayMember = "Name"; _material.ValueMember = "Id";
            _type.DataSource = Enum.GetValues(typeof(MovementType));

            var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(8) };
            table.RowCount = 5;
            AddRow(table, 0, "Материал", _material);
            AddRow(table, 1, "Тип движения", _type);
            AddRow(table, 2, "Количество", _quantity);
            AddRow(table, 3, "Дата", _date);
            AddRow(table, 4, "Номер док-та", _docNumber);
            Controls.Add(table);

            var ok = new Button { Text = "Сохранить", Width = 100 };
            var cancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Width = 100 };
            var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40, FlowDirection = FlowDirection.RightToLeft };
            bottom.Controls.Add(cancel); bottom.Controls.Add(ok); Controls.Add(bottom);
            ok.Click += OnSave;
        }

        private static void AddRow(TableLayoutPanel panel, int row, string label, Control control)
        {
            panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
            control.Dock = DockStyle.Fill; panel.Controls.Add(control, 1, row);
        }

        private void OnSave(object? sender, EventArgs e)
        {
            try
            {
                var materialId = (int)_material.SelectedValue!;
                var type = (MovementType)_type.SelectedItem!;
                var movement = Movement.Create(0, materialId, type, _quantity.Value, _date.Value, _docNumber.Text);

                _movementRepository.Add(movement);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}