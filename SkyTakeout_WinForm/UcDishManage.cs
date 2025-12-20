using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcDishManage : System.Windows.Forms.UserControl
    {
        private List<DishRow> allRows = new List<DishRow>();
        private Dictionary<string, int> dishColumnMaxLen = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        private int currentPage = 1;
        private int pageSize = 10;
        private int totalCount = 0;
        private bool suppressFilterReload = false;
        private bool isCategoryChangeSubscribed = false;

        public UcDishManage()
        {
            InitializeComponent();
            Load += UcDishManage_Load;
        }

        private void UcDishManage_Load(object sender, EventArgs e)
        {
            comboBoxStatus.SelectedIndex = 0;
            comboBoxPageSize.SelectedIndex = 0;
            buttonPrev.Click += buttonPrev_Click;
            buttonNext.Click += buttonNext_Click;
            colToggle.UseColumnTextForLinkValue = false;
            LoadCategoryOptions();
            if (!isCategoryChangeSubscribed)
            {
                FormMain.CategoryChanged += FormMain_CategoryChanged;
                Disposed += UcDishManage_Disposed;
                isCategoryChangeSubscribed = true;
            }
            comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
            comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
            comboBoxPageSize.SelectedIndexChanged += comboBoxPageSize_SelectedIndexChanged;
            textBoxDishName.KeyDown += textBoxDishName_KeyDown;
            dishColumnMaxLen = QueryDishColumnMaxLen();
            ReloadFromDatabase(true);
        }

        private void UcDishManage_Disposed(object sender, EventArgs e)
        {
            FormMain.CategoryChanged -= FormMain_CategoryChanged;
            isCategoryChangeSubscribed = false;
        }

        private void FormMain_CategoryChanged()
        {
            if (IsDisposed)
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => LoadCategoryOptions(true)));
                return;
            }

            LoadCategoryOptions(true);
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressFilterReload)
            {
                return;
            }

            ReloadFromDatabase(true);
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressFilterReload)
            {
                return;
            }

            ReloadFromDatabase(true);
        }

        private void comboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressFilterReload)
            {
                return;
            }

            ReloadFromDatabase(true);
        }

        private void textBoxDishName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
            ReloadFromDatabase(true);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ReloadFromDatabase(true);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DishEditForm form = new DishEditForm("新增菜品", GetCategoryOptionsForEdit(), null);
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            InsertDish(form.DishName, form.Category, form.PriceText, form.ImagePath, form.StatusText);
            LoadCategoryOptions();
            ReloadFromDatabase(true);
        }

        private void buttonBatchDelete_Click(object sender, EventArgs e)
        {
            List<DishRow> selected = new List<DishRow>();
            foreach (DataGridViewRow row in dataGridViewDish.Rows)
            {
                bool isChecked = row.Cells[colSelect.Index].Value is bool b && b;
                if (isChecked && row.Tag is DishRow model)
                {
                    selected.Add(model);
                }
            }

            if (selected.Count == 0)
            {
                MessageBox.Show("请先勾选要删除的菜品。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show($"确定要批量删除 {selected.Count} 个菜品吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            List<int> ids = selected.Select(s => s.Id).Distinct().ToList();
            DeleteDishes(ids);
            ReloadFromDatabase(true);
        }

        private void dataGridViewDish_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == colStatus.Index)
            {
                DataGridViewRow row = dataGridViewDish.Rows[e.RowIndex];
                DishRow model = row.Tag as DishRow;
                if (model == null)
                {
                    return;
                }

                e.Value = model.Enabled ? "启售" : "停售";
                e.FormattingApplied = true;

                DataGridViewCell cell = row.Cells[e.ColumnIndex];
                cell.Style.ForeColor = model.Enabled ? Color.FromArgb(35, 170, 88) : Color.FromArgb(144, 147, 153);
            }
        }

        private void dataGridViewDish_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewDish.Rows[e.RowIndex];
            DishRow model = row.Tag as DishRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colEdit.Index)
            {
                DishEditForm form = new DishEditForm("修改菜品", GetCategoryOptionsForEdit(), model);
                if (form.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                UpdateDish(model.Id, form.DishName, form.Category, form.PriceText, form.ImagePath, form.StatusText);
                LoadCategoryOptions();
                ReloadFromDatabase(false);
                return;
            }

            if (e.ColumnIndex == colDelete.Index)
            {
                DialogResult confirm = MessageBox.Show($"确定删除菜品：{model.Name}？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                DeleteDishes(new List<int> { model.Id });
                ReloadFromDatabase(false);
                return;
            }

            if (e.ColumnIndex == colToggle.Index)
            {
                UpdateDishStatus(model.Id, model.Enabled ? "停售" : "启售");
                ReloadFromDatabase(false);
            }
        }

        private void BindRows(List<DishRow> rows)
        {
            dataGridViewDish.SuspendLayout();
            dataGridViewDish.Rows.Clear();

            for (int i = 0; i < rows.Count; i++)
            {
                DishRow r = rows[i];
                int rowIndex = dataGridViewDish.Rows.Add(false, r.Name, r.Image, r.Category, r.PriceText, r.Enabled ? "启售" : "停售", r.UpdateTimeText, "修改", "删除", r.Enabled ? "停售" : "启售");
                DataGridViewRow gridRow = dataGridViewDish.Rows[rowIndex];
                gridRow.Tag = r;
                gridRow.Height = 52;
            }

            labelTotal.Text = $"共 {rows.Count} 条";
            dataGridViewDish.ResumeLayout();
        }

        private void ReloadFromDatabase(bool resetPage)
        {
            if (resetPage)
            {
                currentPage = 1;
            }

            string keyword = (textBoxDishName.Text ?? string.Empty).Trim();
            string category = comboBoxCategory.SelectedItem as string;
            string status = comboBoxStatus.SelectedItem as string;

            pageSize = ParsePageSize(comboBoxPageSize.SelectedItem as string);

            int count;
            List<DishRow> rows = QueryDishes(keyword, category, status, currentPage, pageSize, out count);
            totalCount = count;
            allRows = rows;
            BindRows(allRows);
            UpdatePager();
        }

        private void LoadCategoryOptions(bool preserveSelection = false)
        {
            string prevSelected = preserveSelection ? (comboBoxCategory.SelectedItem as string ?? string.Empty) : string.Empty;

            suppressFilterReload = true;
            comboBoxCategory.Items.Clear();
            comboBoxCategory.Items.Add("全部");

            List<string> categories = QueryCategories();
            for (int i = 0; i < categories.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(categories[i]))
                {
                    comboBoxCategory.Items.Add(categories[i]);
                }
            }

            if (!string.IsNullOrWhiteSpace(prevSelected))
            {
                int idx = comboBoxCategory.FindStringExact(prevSelected);
                comboBoxCategory.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                comboBoxCategory.SelectedIndex = 0;
            }
            suppressFilterReload = false;
        }

        private List<string> QueryCategories()
        {
            List<string> result = new List<string>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT
    RTRIM([name]) AS c
FROM dbo.[category]
WHERE
    [type] = 1
    AND ([status] IS NULL OR [status] = 1)
    AND [name] IS NOT NULL
ORDER BY TRY_CONVERT(int, RTRIM([sort])) ASC, [id] DESC
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string c = reader.IsDBNull(0) ? string.Empty : (reader.GetString(0) ?? string.Empty).Trim();
                        if (!string.IsNullOrWhiteSpace(c))
                        {
                            result.Add(c);
                        }
                    }
                }
            }
            return result;
        }

        private List<DishRow> QueryDishes(string keyword, string category, string status, int page, int size, out int count)
        {
            string kw = keyword ?? string.Empty;
            string cat = category ?? string.Empty;
            string st = status ?? string.Empty;

            if (string.Equals(cat, "全部", StringComparison.OrdinalIgnoreCase))
            {
                cat = string.Empty;
            }

            if (string.Equals(st, "全部", StringComparison.OrdinalIgnoreCase))
            {
                st = string.Empty;
            }

            int safePage = page < 1 ? 1 : page;
            int safeSize = size < 1 ? 10 : size;
            int offset = (safePage - 1) * safeSize;

            List<DishRow> result = new List<DishRow>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand countCmd = new SqlCommand(@"
SELECT COUNT(1)
FROM dbo.[dish]
WHERE
    (@kw = '' OR RTRIM([name]) LIKE '%' + @kw + '%')
    AND (@cat = '' OR RTRIM([category]) = @cat)
    AND (@st = '' OR RTRIM([status]) = @st)
", conn))
                {
                    countCmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = kw;
                    countCmd.Parameters.Add("@cat", SqlDbType.NVarChar, 200).Value = cat;
                    countCmd.Parameters.Add("@st", SqlDbType.NVarChar, 50).Value = st;
                    object raw = countCmd.ExecuteScalar();
                    count = raw == null || raw == DBNull.Value ? 0 : Convert.ToInt32(raw);
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT
    [id],
    RTRIM([name]) AS [name],
    RTRIM([image]) AS [image],
    RTRIM([price]) AS [price],
    RTRIM([category]) AS [category],
    RTRIM([status]) AS [status],
    [update_time]
FROM dbo.[dish]
WHERE
    (@kw = '' OR RTRIM([name]) LIKE '%' + @kw + '%')
    AND (@cat = '' OR RTRIM([category]) = @cat)
    AND (@st = '' OR RTRIM([status]) = @st)
ORDER BY [id] DESC
OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY
", conn))
                {
                    cmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = kw;
                    cmd.Parameters.Add("@cat", SqlDbType.NVarChar, 200).Value = cat;
                    cmd.Parameters.Add("@st", SqlDbType.NVarChar, 50).Value = st;
                    cmd.Parameters.Add("@offset", SqlDbType.Int).Value = offset;
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = safeSize;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                            string imageText = reader.IsDBNull(2) ? string.Empty : (reader.GetString(2) ?? string.Empty).Trim();
                            string priceText = reader.IsDBNull(3) ? string.Empty : (reader.GetString(3) ?? string.Empty).Trim();
                            string catText = reader.IsDBNull(4) ? string.Empty : (reader.GetString(4) ?? string.Empty).Trim();
                            string statusText = reader.IsDBNull(5) ? string.Empty : (reader.GetString(5) ?? string.Empty).Trim();
                            TimeSpan? updateTime = reader.IsDBNull(6) ? (TimeSpan?)null : (TimeSpan)reader.GetValue(6);

                            result.Add(new DishRow(id, name, catText, priceText, statusText, updateTime, imageText));
                        }
                    }
                }
            }

            return result;
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (currentPage <= 1)
            {
                return;
            }

            currentPage--;
            ReloadFromDatabase(false);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            int pageCount = GetPageCount();
            if (currentPage >= pageCount)
            {
                return;
            }

            currentPage++;
            ReloadFromDatabase(false);
        }

        private int GetPageCount()
        {
            if (pageSize <= 0)
            {
                return 1;
            }

            int pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            return pageCount < 1 ? 1 : pageCount;
        }

        private void UpdatePager()
        {
            int pageCount = GetPageCount();
            if (currentPage > pageCount)
            {
                currentPage = pageCount;
            }

            labelTotal.Text = $"共 {totalCount} 条";
            labelPage.Text = $"{currentPage}/{pageCount}";
            buttonPrev.Enabled = currentPage > 1;
            buttonNext.Enabled = currentPage < pageCount;
        }

        private static int ParsePageSize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 10;
            }

            string digits = new string(value.TakeWhile(char.IsDigit).ToArray());
            int parsed;
            if (int.TryParse(digits, out parsed) && parsed > 0)
            {
                return parsed;
            }

            return 10;
        }

        private void UpdateDishStatus(int id, string status)
        {
            string safeStatus = TruncateByColumnLen("status", (status ?? string.Empty).Trim());
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.[dish] SET [status]=@status, [update_time]=@updateTime WHERE [id]=@id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar, 20).Value = safeStatus;
                cmd.Parameters.Add("@updateTime", SqlDbType.Time).Value = DateTime.Now.TimeOfDay;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteDishes(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[dish] WHERE [id]=@id", conn, tx))
                            {
                                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ids[i];
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        private void InsertDish(string name, string category, string priceText, string imagePath, string statusText)
        {
            string safeName = TruncateByColumnLen("name", (name ?? string.Empty).Trim());
            string safeCategory = TruncateByColumnLen("category", (category ?? string.Empty).Trim());
            string safePrice = TruncateByColumnLen("price", NormalizePriceForDb(priceText));
            string safeImage = TruncateByColumnLen("image", (imagePath ?? string.Empty).Trim());
            string safeStatus = TruncateByColumnLen("status", (statusText ?? string.Empty).Trim());

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.[dish]([name],[image],[price],[ceate_time],[update_time],[category],[status])
VALUES(@name,@image,@price,@createTime,@updateTime,@category,@status)
", conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = safeName;
                cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = safeImage;
                cmd.Parameters.Add("@price", SqlDbType.NVarChar).Value = safePrice;
                cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = safeCategory;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = safeStatus;
                cmd.Parameters.Add("@createTime", SqlDbType.Time).Value = DateTime.Now.TimeOfDay;
                cmd.Parameters.Add("@updateTime", SqlDbType.Time).Value = DateTime.Now.TimeOfDay;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateDish(int id, string name, string category, string priceText, string imagePath, string statusText)
        {
            string safeName = TruncateByColumnLen("name", (name ?? string.Empty).Trim());
            string safeCategory = TruncateByColumnLen("category", (category ?? string.Empty).Trim());
            string safePrice = TruncateByColumnLen("price", NormalizePriceForDb(priceText));
            string safeImage = TruncateByColumnLen("image", (imagePath ?? string.Empty).Trim());
            string safeStatus = TruncateByColumnLen("status", (statusText ?? string.Empty).Trim());

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[dish]
SET [name]=@name,
    [image]=@image,
    [price]=@price,
    [category]=@category,
    [status]=@status,
    [update_time]=@updateTime
WHERE [id]=@id
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = safeName;
                cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = safeImage;
                cmd.Parameters.Add("@price", SqlDbType.NVarChar).Value = safePrice;
                cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = safeCategory;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = safeStatus;
                cmd.Parameters.Add("@updateTime", SqlDbType.Time).Value = DateTime.Now.TimeOfDay;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string NormalizePriceForDb(string priceText)
        {
            string t = (priceText ?? string.Empty).Trim();
            if (t.EndsWith("元", StringComparison.OrdinalIgnoreCase))
            {
                t = t.Substring(0, t.Length - 1).Trim();
            }

            decimal parsed;
            if (!decimal.TryParse(t, out parsed) || parsed < 0)
            {
                return string.Empty;
            }

            return parsed.ToString("0.##");
        }

        private Dictionary<string, int> QueryDishColumnMaxLen()
        {
            Dictionary<string, int> result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME='dish'
ORDER BY ORDINAL_POSITION
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string col = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        if (string.IsNullOrWhiteSpace(col))
                        {
                            continue;
                        }

                        if (reader.IsDBNull(1))
                        {
                            continue;
                        }

                        int len = Convert.ToInt32(reader.GetValue(1));
                        if (len > 0)
                        {
                            result[col.Trim()] = len;
                        }
                    }
                }
            }

            return result;
        }

        private string TruncateByColumnLen(string columnName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            int len;
            if (!dishColumnMaxLen.TryGetValue(columnName, out len) || len <= 0)
            {
                return value;
            }

            if (value.Length <= len)
            {
                return value;
            }

            return value.Substring(0, len);
        }

        private static string NormalizeMoneyText(string value)
        {
            string t = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                return string.Empty;
            }

            if (t.Contains("元"))
            {
                return t;
            }

            return t + "元";
        }

        private static string FormatUpdateTime(TimeSpan? time)
        {
            if (!time.HasValue)
            {
                return string.Empty;
            }

            DateTime dt = DateTime.Today.Add(time.Value);
            return dt.ToString("yyyy-MM-dd HH:mm");
        }

        private string GetConnectionString()
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["SkyTakeout"];
            if (cs != null && !string.IsNullOrWhiteSpace(cs.ConnectionString))
            {
                return cs.ConnectionString;
            }

            return @"Data Source=.\SQLEXPRESS;Initial Catalog=SkyTakeout;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        }

        private static Image LoadImageOrPlaceholder(string value)
        {
            string v = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
            {
                return DishRow.CreatePlaceholderImage();
            }

            try
            {
                if (File.Exists(v))
                {
                    using (FileStream fs = new FileStream(v, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (Image img = Image.FromStream(fs))
                    {
                        return (Image)img.Clone();
                    }
                }
            }
            catch
            {
            }

            return DishRow.CreatePlaceholderImage();
        }

        private List<string> GetCategoryOptionsForEdit()
        {
            List<string> options = new List<string>();
            for (int i = 0; i < comboBoxCategory.Items.Count; i++)
            {
                string item = comboBoxCategory.Items[i] as string;
                if (!string.IsNullOrWhiteSpace(item) && !string.Equals(item, "全部", StringComparison.OrdinalIgnoreCase))
                {
                    options.Add(item);
                }
            }

            if (options.Count == 0)
            {
                options = QueryCategories();
            }

            return options;
        }

        private sealed class DishRow
        {
            public int Id { get; }
            public string Name { get; }
            public string Category { get; }
            public string PriceText { get; }
            public bool Enabled { get; }
            public string StatusText { get; }
            public string UpdateTimeText { get; }
            public string ImagePath { get; }
            public Image Image { get; }

            public DishRow(int id, string name, string category, string priceText, string statusText, TimeSpan? updateTime, string imageText)
            {
                Id = id;
                Name = (name ?? string.Empty).Trim();
                Category = (category ?? string.Empty).Trim();
                StatusText = (statusText ?? string.Empty).Trim();
                Enabled = string.Equals(StatusText, "启售", StringComparison.OrdinalIgnoreCase);
                PriceText = NormalizeMoneyText(priceText);
                UpdateTimeText = FormatUpdateTime(updateTime);
                ImagePath = (imageText ?? string.Empty).Trim();
                Image = LoadImageOrPlaceholder(ImagePath);
            }

            public static Image CreatePlaceholderImage()
            {
                Bitmap bmp = new Bitmap(40, 40);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.FromArgb(245, 246, 247));
                    using (Pen pen = new Pen(Color.FromArgb(220, 223, 230)))
                    {
                        g.DrawRectangle(pen, 0, 0, 39, 39);
                        g.DrawLine(pen, 8, 26, 18, 16);
                        g.DrawLine(pen, 18, 16, 26, 24);
                        g.DrawLine(pen, 26, 24, 32, 18);
                    }
                }

                return bmp;
            }
        }

        private sealed class DishEditForm : Form
        {
            private readonly TextBox textBoxName = new TextBox();
            private readonly ComboBox comboBoxCategory = new ComboBox();
            private readonly TextBox textBoxPrice = new TextBox();
            private readonly TextBox textBoxImage = new TextBox();
            private readonly ComboBox comboBoxStatus = new ComboBox();
            private readonly Button buttonBrowse = new Button();
            private readonly Button buttonOk = new Button();
            private readonly Button buttonCancel = new Button();

            public string DishName => (textBoxName.Text ?? string.Empty).Trim();
            public string Category => (comboBoxCategory.Text ?? string.Empty).Trim();
            public string PriceText => (textBoxPrice.Text ?? string.Empty).Trim();
            public string ImagePath => (textBoxImage.Text ?? string.Empty).Trim();
            public string StatusText => comboBoxStatus.SelectedItem as string ?? string.Empty;

            public DishEditForm(string title, List<string> categoryOptions, DishRow model)
            {
                Text = title;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(520, 260);

                Label labelName = new Label { Text = "菜品名称", AutoSize = true, Location = new Point(24, 24) };
                textBoxName.Location = new Point(110, 20);
                textBoxName.Size = new Size(370, 28);

                Label labelCategory = new Label { Text = "菜品分类", AutoSize = true, Location = new Point(24, 66) };
                comboBoxCategory.Location = new Point(110, 62);
                comboBoxCategory.Size = new Size(180, 28);
                comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDown;
                if (categoryOptions != null)
                {
                    comboBoxCategory.Items.AddRange(categoryOptions.Cast<object>().ToArray());
                }

                Label labelPrice = new Label { Text = "价格", AutoSize = true, Location = new Point(320, 66) };
                textBoxPrice.Location = new Point(370, 62);
                textBoxPrice.Size = new Size(110, 28);

                Label labelImage = new Label { Text = "图片路径", AutoSize = true, Location = new Point(24, 108) };
                textBoxImage.Location = new Point(110, 104);
                textBoxImage.Size = new Size(290, 28);
                buttonBrowse.Text = "浏览";
                buttonBrowse.Location = new Point(410, 103);
                buttonBrowse.Size = new Size(70, 30);
                buttonBrowse.Click += buttonBrowse_Click;

                Label labelStatus = new Label { Text = "售卖状态", AutoSize = true, Location = new Point(24, 150) };
                comboBoxStatus.Location = new Point(110, 146);
                comboBoxStatus.Size = new Size(180, 28);
                comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxStatus.Items.AddRange(new object[] { "启售", "停售" });
                comboBoxStatus.SelectedIndex = 0;

                buttonOk.Text = "保存";
                buttonOk.Location = new Point(300, 200);
                buttonOk.Size = new Size(80, 34);
                buttonOk.BackColor = Color.FromArgb(255, 184, 0);
                buttonOk.FlatStyle = FlatStyle.Flat;
                buttonOk.FlatAppearance.BorderSize = 0;
                buttonOk.ForeColor = Color.White;
                buttonOk.Click += buttonOk_Click;

                buttonCancel.Text = "取消";
                buttonCancel.Location = new Point(400, 200);
                buttonCancel.Size = new Size(80, 34);
                buttonCancel.DialogResult = DialogResult.Cancel;

                Controls.Add(labelName);
                Controls.Add(textBoxName);
                Controls.Add(labelCategory);
                Controls.Add(comboBoxCategory);
                Controls.Add(labelPrice);
                Controls.Add(textBoxPrice);
                Controls.Add(labelImage);
                Controls.Add(textBoxImage);
                Controls.Add(buttonBrowse);
                Controls.Add(labelStatus);
                Controls.Add(comboBoxStatus);
                Controls.Add(buttonOk);
                Controls.Add(buttonCancel);

                AcceptButton = buttonOk;
                CancelButton = buttonCancel;

                if (model != null)
                {
                    textBoxName.Text = model.Name;
                    comboBoxCategory.Text = model.Category;
                    textBoxPrice.Text = (model.PriceText ?? string.Empty).Replace("元", string.Empty).Trim();
                    textBoxImage.Text = model.ImagePath;
                    comboBoxStatus.SelectedItem = model.Enabled ? "启售" : "停售";
                }
            }

            private void buttonBrowse_Click(object sender, EventArgs e)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif|所有文件|*.*";
                    dialog.Multiselect = false;
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxImage.Text = dialog.FileName;
                    }
                }
            }

            private void buttonOk_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(DishName))
                {
                    MessageBox.Show("请输入菜品名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxName.Focus();
                    return;
                }

                decimal p;
                string price = PriceText ?? string.Empty;
                if (!decimal.TryParse(price, out p) || p < 0)
                {
                    MessageBox.Show("请输入正确的价格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxPrice.Focus();
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}

