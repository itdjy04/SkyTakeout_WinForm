using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcSetmealEdit : UserControl
    {
        private readonly BindingSource dishBinding = new BindingSource();
        private List<SetmealDishItem> dishes = new List<SetmealDishItem>();
        private List<DishOption> dishOptions = new List<DishOption>();
        private bool allowSaveAndContinue;

        internal event Action CancelRequested;
        internal event Action<SetmealEditResult> SaveRequested;

        public UcSetmealEdit()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            InitGrid();
            WireEvents();
        }

        private void InitGrid()
        {
            dataGridViewDishes.AutoGenerateColumns = false;
            dataGridViewDishes.Columns.Clear();
            dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "菜品ID", DataPropertyName = "DishId", Width = 90 });
            dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "名称", DataPropertyName = "Name", Width = 260 });
            dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "价格", DataPropertyName = "PriceText", Width = 90 });
            dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "份数", DataPropertyName = "Copies", Width = 70 });
            dataGridViewDishes.DataSource = dishBinding;
            dishBinding.DataSource = new List<DishGridRow>();
        }

        private void WireEvents()
        {
            buttonCancel.Click += (s, e) => CancelRequested?.Invoke();
            buttonSave.Click += (s, e) => TryRaiseSave(false);
            buttonSaveContinue.Click += (s, e) => TryRaiseSave(true);
            buttonUpload.Click += buttonUpload_Click;
            buttonAddDish.Click += buttonAddDish_Click;
            buttonRemoveDish.Click += buttonRemoveDish_Click;
            textBoxImagePath.TextChanged += (s, e) => RefreshImagePreview();
        }

        internal void EnterAddMode(List<SetmealOptionItem> categoryOptions, List<DishOption> dishOptions, bool allowSaveAndContinue)
        {
            this.allowSaveAndContinue = allowSaveAndContinue;
            buttonSaveContinue.Visible = allowSaveAndContinue;
            this.dishOptions = dishOptions ?? new List<DishOption>();
            BindCategoryOptions(categoryOptions);
            textBoxSetmealName.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            textBoxDescription.Text = string.Empty;
            textBoxImagePath.Text = string.Empty;
            comboBoxStatus.SelectedIndex = 0;
            dishes = new List<SetmealDishItem>();
            RebindDishGrid();
            labelPageTitle.Text = "新增套餐";
        }

        internal void EnterEditMode(SetmealEditModel model, List<SetmealOptionItem> categoryOptions, List<DishOption> dishOptions)
        {
            allowSaveAndContinue = false;
            buttonSaveContinue.Visible = false;
            this.dishOptions = dishOptions ?? new List<DishOption>();
            BindCategoryOptions(categoryOptions);

            SetmealEditModel m = model ?? new SetmealEditModel(0, string.Empty, 0, 0m, 1, string.Empty, string.Empty, new List<SetmealDishItem>());
            labelPageTitle.Text = "修改套餐";
            textBoxSetmealName.Text = m.Name ?? string.Empty;
            textBoxPrice.Text = m.Price.ToString("0.##");
            textBoxDescription.Text = m.Description ?? string.Empty;
            textBoxImagePath.Text = m.ImagePath ?? string.Empty;
            comboBoxStatus.SelectedIndex = m.Status == 0 ? 1 : 0;

            if (m.CategoryId > 0)
            {
                for (int i = 0; i < comboBoxCategory.Items.Count; i++)
                {
                    if (comboBoxCategory.Items[i] is SetmealOptionItem it && it.Value == m.CategoryId)
                    {
                        comboBoxCategory.SelectedIndex = i;
                        break;
                    }
                }
            }

            dishes = m.Dishes?.ToList() ?? new List<SetmealDishItem>();
            RebindDishGrid();
        }

        internal void UpdateCategoryOptions(List<SetmealOptionItem> categoryOptions, bool preserveSelection)
        {
            long selected = 0;
            if (preserveSelection && comboBoxCategory.SelectedItem is SetmealOptionItem it)
            {
                selected = it.Value;
            }

            BindCategoryOptions(categoryOptions);

            if (!preserveSelection || selected <= 0)
            {
                return;
            }

            for (int i = 0; i < comboBoxCategory.Items.Count; i++)
            {
                if (comboBoxCategory.Items[i] is SetmealOptionItem opt && opt.Value == selected)
                {
                    comboBoxCategory.SelectedIndex = i;
                    break;
                }
            }
        }

        private void BindCategoryOptions(List<SetmealOptionItem> categoryOptions)
        {
            comboBoxCategory.Items.Clear();
            List<SetmealOptionItem> opts = categoryOptions ?? new List<SetmealOptionItem>();
            comboBoxCategory.Items.AddRange(opts.Cast<object>().ToArray());
            if (comboBoxCategory.Items.Count > 0)
            {
                comboBoxCategory.SelectedIndex = 0;
            }
        }

        private void TryRaiseSave(bool saveAndContinue)
        {
            if (saveAndContinue && !allowSaveAndContinue)
            {
                saveAndContinue = false;
            }

            string name = (textBoxSetmealName.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("请输入套餐名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxSetmealName.Focus();
                return;
            }

            if (!(comboBoxCategory.SelectedItem is SetmealOptionItem cat) || cat.Value <= 0)
            {
                MessageBox.Show("请选择套餐分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxCategory.Focus();
                return;
            }

            decimal price;
            if (!decimal.TryParse((textBoxPrice.Text ?? string.Empty).Trim(), out price) || price < 0)
            {
                MessageBox.Show("请输入正确的套餐价格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPrice.Focus();
                return;
            }

            int status = comboBoxStatus.SelectedIndex == 1 ? 0 : 1;
            string desc = (textBoxDescription.Text ?? string.Empty).Trim();
            string img = (textBoxImagePath.Text ?? string.Empty).Trim();

            SaveRequested?.Invoke(new SetmealEditResult(name, cat.Value, price, status, desc, img, dishes.ToList(), saveAndContinue));
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif|所有文件|*.*";
                dialog.Multiselect = false;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    string ext = (Path.GetExtension(filePath) ?? string.Empty).Trim().ToLowerInvariant();
                    HashSet<string> allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
                    if (!allowed.Contains(ext))
                    {
                        MessageBox.Show("仅支持 PNG/JPG/JPEG/BMP/GIF 格式图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    try
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Exists && fi.Length > 2 * 1024 * 1024)
                        {
                            MessageBox.Show("图片大小不能超过 2MB。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("读取图片文件失败，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    textBoxImagePath.Text = filePath;
                }
            }
        }

        private void buttonAddDish_Click(object sender, EventArgs e)
        {
            using (DishPickForm pick = new DishPickForm(dishOptions))
            {
                if (pick.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                DishOption selected = pick.SelectedDish;
                if (selected == null)
                {
                    return;
                }

                using (CopiesForm cf = new CopiesForm())
                {
                    if (cf.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    int copies = cf.Copies;
                    dishes.Add(new SetmealDishItem(selected.Id, selected.Name, selected.Price, copies));
                    RebindDishGrid();
                }
            }
        }

        private void buttonRemoveDish_Click(object sender, EventArgs e)
        {
            if (dataGridViewDishes.CurrentRow == null)
            {
                return;
            }

            int idx = dataGridViewDishes.CurrentRow.Index;
            if (idx < 0 || idx >= dishes.Count)
            {
                return;
            }

            dishes.RemoveAt(idx);
            RebindDishGrid();
        }

        private void RebindDishGrid()
        {
            List<DishGridRow> rows = dishes.Select(d => new DishGridRow(d.DishId, d.Name, d.Price.ToString("0.##") + "元", d.Copies)).ToList();
            dishBinding.DataSource = rows;
        }

        private void RefreshImagePreview()
        {
            string path = (textBoxImagePath.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(path))
            {
                pictureBoxPreview.Image = null;
                return;
            }

            try
            {
                using (var img = Image.FromFile(path))
                {
                    pictureBoxPreview.Image = (Image)img.Clone();
                }
            }
            catch
            {
                pictureBoxPreview.Image = null;
            }
        }

        private sealed class DishGridRow
        {
            public long DishId { get; }
            public string Name { get; }
            public string PriceText { get; }
            public int Copies { get; }

            public DishGridRow(long dishId, string name, string priceText, int copies)
            {
                DishId = dishId;
                Name = name ?? string.Empty;
                PriceText = priceText ?? string.Empty;
                Copies = copies;
            }
        }

        private sealed class DishPickForm : Form
        {
            private readonly List<DishOption> options;
            private readonly BindingSource binding = new BindingSource();
            private readonly DataGridView grid = new DataGridView();
            private readonly TextBox textBoxKeyword = new TextBox();
            private readonly Button buttonSearch = new Button();
            private readonly Button buttonOk = new Button();
            private readonly Button buttonCancel = new Button();

            public DishOption SelectedDish { get; private set; }

            public DishPickForm(List<DishOption> options)
            {
                this.options = options ?? new List<DishOption>();
                Text = "选择菜品";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(520, 420);

                Label label = new Label { Text = "菜品名称", AutoSize = true, Location = new Point(16, 18) };
                textBoxKeyword.Location = new Point(92, 14);
                textBoxKeyword.Size = new Size(200, 28);
                textBoxKeyword.KeyDown += textBoxKeyword_KeyDown;

                buttonSearch.Text = "查询";
                buttonSearch.Location = new Point(300, 12);
                buttonSearch.Size = new Size(70, 32);
                buttonSearch.Click += (s, e) => ApplyFilter();

                grid.Location = new Point(16, 54);
                grid.Size = new Size(488, 306);
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.ReadOnly = true;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.RowHeadersVisible = false;
                grid.AutoGenerateColumns = false;
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 80 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "名称", DataPropertyName = "Name", Width = 260 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "价格", DataPropertyName = "PriceText", Width = 100 });
                grid.DoubleClick += (s, e) => { if (TryPick()) { DialogResult = DialogResult.OK; Close(); } };

                buttonOk.Text = "确定";
                buttonOk.Location = new Point(344, 370);
                buttonOk.Size = new Size(76, 32);
                buttonOk.Click += (s, e) =>
                {
                    if (!TryPick())
                    {
                        MessageBox.Show("请选择一个菜品。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                };

                buttonCancel.Text = "取消";
                buttonCancel.Location = new Point(428, 370);
                buttonCancel.Size = new Size(76, 32);
                buttonCancel.DialogResult = DialogResult.Cancel;

                Controls.Add(label);
                Controls.Add(textBoxKeyword);
                Controls.Add(buttonSearch);
                Controls.Add(grid);
                Controls.Add(buttonOk);
                Controls.Add(buttonCancel);

                AcceptButton = buttonOk;
                CancelButton = buttonCancel;

                ApplyFilter();
            }

            private void textBoxKeyword_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
                ApplyFilter();
            }

            private void ApplyFilter()
            {
                string kw = (textBoxKeyword.Text ?? string.Empty).Trim();
                IEnumerable<DishOption> filtered = options;
                if (!string.IsNullOrWhiteSpace(kw))
                {
                    filtered = filtered.Where(d => (d.Name ?? string.Empty).IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                List<DishGridRow> rows = filtered.Select(d => new DishGridRow(d.Id, d.Name, d.Price.ToString("0.##") + "元")).ToList();
                binding.DataSource = rows;
                grid.DataSource = binding;
            }

            private bool TryPick()
            {
                if (grid.CurrentRow == null || !(grid.CurrentRow.DataBoundItem is DishGridRow row))
                {
                    return false;
                }

                DishOption opt = options.FirstOrDefault(o => o.Id == row.Id);
                if (opt == null)
                {
                    return false;
                }

                SelectedDish = opt;
                return true;
            }

            private sealed class DishGridRow
            {
                public long Id { get; }
                public string Name { get; }
                public string PriceText { get; }

                public DishGridRow(long id, string name, string priceText)
                {
                    Id = id;
                    Name = name ?? string.Empty;
                    PriceText = priceText ?? string.Empty;
                }
            }
        }

        private sealed class CopiesForm : Form
        {
            private readonly NumericUpDown numeric = new NumericUpDown();
            private readonly Button buttonOk = new Button();
            private readonly Button buttonCancel = new Button();

            public int Copies => (int)numeric.Value;

            public CopiesForm()
            {
                Text = "份数";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(260, 140);

                Label label = new Label { Text = "份数", AutoSize = true, Location = new Point(20, 24) };
                numeric.Location = new Point(70, 20);
                numeric.Size = new Size(160, 28);
                numeric.Minimum = 1;
                numeric.Maximum = 99;
                numeric.Value = 1;

                buttonOk.Text = "确定";
                buttonOk.Location = new Point(70, 76);
                buttonOk.Size = new Size(76, 32);
                buttonOk.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };

                buttonCancel.Text = "取消";
                buttonCancel.Location = new Point(154, 76);
                buttonCancel.Size = new Size(76, 32);
                buttonCancel.DialogResult = DialogResult.Cancel;

                Controls.Add(label);
                Controls.Add(numeric);
                Controls.Add(buttonOk);
                Controls.Add(buttonCancel);

                AcceptButton = buttonOk;
                CancelButton = buttonCancel;
            }
        }
    }

    internal sealed class SetmealOptionItem
    {
        public string Text { get; }
        public long Value { get; }

        public SetmealOptionItem(string text, long value)
        {
            Text = text ?? string.Empty;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    internal sealed class DishOption
    {
        public long Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public DishOption(long id, string name, decimal price)
        {
            Id = id;
            Name = (name ?? string.Empty).Trim();
            Price = price;
        }
    }

    internal sealed class SetmealDishItem
    {
        public long DishId { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Copies { get; }

        public SetmealDishItem(long dishId, string name, decimal price, int copies)
        {
            DishId = dishId;
            Name = (name ?? string.Empty).Trim();
            Price = price;
            Copies = copies < 1 ? 1 : copies;
        }
    }

    internal sealed class SetmealEditModel
    {
        public long Id { get; }
        public string Name { get; }
        public long CategoryId { get; }
        public decimal Price { get; }
        public int Status { get; }
        public string Description { get; }
        public string ImagePath { get; }
        public List<SetmealDishItem> Dishes { get; }

        public SetmealEditModel(long id, string name, long categoryId, decimal price, int status, string description, string imagePath, List<SetmealDishItem> dishes)
        {
            Id = id;
            Name = name ?? string.Empty;
            CategoryId = categoryId;
            Price = price;
            Status = status;
            Description = description ?? string.Empty;
            ImagePath = imagePath ?? string.Empty;
            Dishes = dishes ?? new List<SetmealDishItem>();
        }
    }

    internal sealed class SetmealEditResult
    {
        public string Name { get; }
        public long CategoryId { get; }
        public decimal Price { get; }
        public int Status { get; }
        public string Description { get; }
        public string ImagePath { get; }
        public List<SetmealDishItem> Dishes { get; }
        public bool SaveAndContinue { get; }

        public SetmealEditResult(string name, long categoryId, decimal price, int status, string description, string imagePath, List<SetmealDishItem> dishes, bool saveAndContinue)
        {
            Name = name ?? string.Empty;
            CategoryId = categoryId;
            Price = price;
            Status = status;
            Description = description ?? string.Empty;
            ImagePath = imagePath ?? string.Empty;
            Dishes = dishes ?? new List<SetmealDishItem>();
            SaveAndContinue = saveAndContinue;
        }
    }
}
