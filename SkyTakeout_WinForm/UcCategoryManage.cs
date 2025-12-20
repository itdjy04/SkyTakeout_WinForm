using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcCategoryManage : System.Windows.Forms.UserControl
    {
        private readonly int currentUserId;
        private Dictionary<string, int> columnMaxLen = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalCount = 0;
        private bool suppressFilterReload = false;

        public UcCategoryManage(int userId)
        {
            currentUserId = userId;
            InitializeComponent();
            Load += UcCategoryManage_Load;
        }

        private void UcCategoryManage_Load(object sender, EventArgs e)
        {
            comboBoxType.SelectedIndex = 0;
            comboBoxStatus.SelectedIndex = 0;
            comboBoxPageSize.SelectedIndex = 0;

            colToggle.UseColumnTextForLinkValue = false;
            buttonPrev.Click += buttonPrev_Click;
            buttonNext.Click += buttonNext_Click;
            buttonSearch.Click += buttonSearch_Click;
            buttonBatchDelete.Click += buttonBatchDelete_Click;
            buttonAddDishCategory.Click += buttonAddDishCategory_Click;
            buttonAddSetmealCategory.Click += buttonAddSetmealCategory_Click;
            dataGridViewCategory.CellFormatting += dataGridViewCategory_CellFormatting;
            dataGridViewCategory.CellContentClick += dataGridViewCategory_CellContentClick;

            comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
            comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
            comboBoxPageSize.SelectedIndexChanged += comboBoxPageSize_SelectedIndexChanged;
            textBoxCategoryName.KeyDown += textBoxCategoryName_KeyDown;

            columnMaxLen = QueryColumnMaxLen();
            ReloadFromDatabase(true);
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBoxCategoryName_KeyDown(object sender, KeyEventArgs e)
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

        private void buttonAddDishCategory_Click(object sender, EventArgs e)
        {
            ShowEditDialog(1, null);
        }

        private void buttonAddSetmealCategory_Click(object sender, EventArgs e)
        {
            ShowEditDialog(2, null);
        }

        private void buttonBatchDelete_Click(object sender, EventArgs e)
        {
            List<int> ids = new List<int>();
            foreach (DataGridViewRow row in dataGridViewCategory.Rows)
            {
                bool isChecked = row.Cells[colSelect.Index].Value is bool b && b;
                if (isChecked && row.Tag is CategoryRow model)
                {
                    ids.Add(model.Id);
                }
            }

            ids = ids.Distinct().ToList();
            if (ids.Count == 0)
            {
                MessageBox.Show("请先勾选要删除的分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show($"确定要批量删除 {ids.Count} 个分类吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            DeleteCategories(ids);
            ReloadFromDatabase(true);
            FormMain.NotifyCategoryChanged();
        }

        private void dataGridViewCategory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewCategory.Rows[e.RowIndex];
            CategoryRow model = row.Tag as CategoryRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colStatus.Index)
            {
                e.Value = model.Enabled ? "启用" : "禁用";
                e.FormattingApplied = true;
                DataGridViewCell cell = row.Cells[e.ColumnIndex];
                cell.Style.ForeColor = model.Enabled ? Color.FromArgb(35, 170, 88) : Color.FromArgb(144, 147, 153);
                return;
            }

            if (e.ColumnIndex == colType.Index)
            {
                e.Value = model.Type == 1 ? "菜品分类" : "套餐分类";
                e.FormattingApplied = true;
                return;
            }
        }

        private void dataGridViewCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewCategory.Rows[e.RowIndex];
            CategoryRow model = row.Tag as CategoryRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colEdit.Index)
            {
                ShowEditDialog(0, model);
                return;
            }

            if (e.ColumnIndex == colDelete.Index)
            {
                DialogResult confirm = MessageBox.Show($"确定删除分类：{model.Name}？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                DeleteCategories(new List<int> { model.Id });
                ReloadFromDatabase(false);
                FormMain.NotifyCategoryChanged();
                return;
            }

            if (e.ColumnIndex == colToggle.Index)
            {
                UpdateCategoryStatus(model.Id, model.Enabled ? (byte)0 : (byte)1);
                ReloadFromDatabase(false);
                FormMain.NotifyCategoryChanged();
            }
        }

        private void ShowEditDialog(int fixedType, CategoryRow existing)
        {
            using (CategoryEditForm form = new CategoryEditForm(existing == null ? "新增分类" : "修改分类", fixedType, existing))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                if (existing == null)
                {
                    InsertCategory(form.CategoryName, form.CategoryType, form.SortText, form.IsEnabled ? (byte)1 : (byte)0);
                    ReloadFromDatabase(true);
                    FormMain.NotifyCategoryChanged();
                }
                else
                {
                    UpdateCategory(existing.Id, form.CategoryName, existing.Type, form.SortText, form.IsEnabled ? (byte)1 : (byte)0);
                    ReloadFromDatabase(false);
                    FormMain.NotifyCategoryChanged();
                }
            }
        }

        private void BindRows(List<CategoryRow> rows)
        {
            dataGridViewCategory.SuspendLayout();
            dataGridViewCategory.Rows.Clear();
            for (int i = 0; i < rows.Count; i++)
            {
                CategoryRow r = rows[i];
                int rowIndex = dataGridViewCategory.Rows.Add(false, r.Name, r.Type == 1 ? "菜品分类" : "套餐分类", r.SortText, r.Enabled ? "启用" : "禁用", r.UpdateTimeText, "修改", "删除", r.Enabled ? "禁用" : "启用");
                DataGridViewRow gridRow = dataGridViewCategory.Rows[rowIndex];
                gridRow.Tag = r;
                gridRow.Height = 52;
            }
            dataGridViewCategory.ResumeLayout();
        }

        private void ReloadFromDatabase(bool resetPage)
        {
            if (resetPage)
            {
                currentPage = 1;
            }

            string keyword = (textBoxCategoryName.Text ?? string.Empty).Trim();
            byte? type = ParseType(comboBoxType.SelectedItem as string);
            byte? status = ParseStatus(comboBoxStatus.SelectedItem as string);
            pageSize = ParsePageSize(comboBoxPageSize.SelectedItem as string);

            int count;
            List<CategoryRow> rows = QueryCategories(keyword, type, status, currentPage, pageSize, out count);
            totalCount = count;
            BindRows(rows);
            UpdatePager();
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

        private List<CategoryRow> QueryCategories(string keyword, byte? type, byte? status, int page, int size, out int count)
        {
            string kw = keyword ?? string.Empty;
            int safePage = page < 1 ? 1 : page;
            int safeSize = size < 1 ? 10 : size;
            int offset = (safePage - 1) * safeSize;

            List<CategoryRow> result = new List<CategoryRow>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand countCmd = new SqlCommand(@"
SELECT COUNT(1)
FROM dbo.[category]
WHERE
    (@kw = '' OR RTRIM([name]) LIKE '%' + @kw + '%')
    AND (@type IS NULL OR [type] = @type)
    AND (@st IS NULL OR [status] = @st)
", conn))
                {
                    countCmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = kw;
                    countCmd.Parameters.Add("@type", SqlDbType.TinyInt).Value = (object)type ?? DBNull.Value;
                    countCmd.Parameters.Add("@st", SqlDbType.TinyInt).Value = (object)status ?? DBNull.Value;
                    object raw = countCmd.ExecuteScalar();
                    count = raw == null || raw == DBNull.Value ? 0 : Convert.ToInt32(raw);
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT
    [id],
    RTRIM([name]) AS [name],
    [type],
    RTRIM([sort]) AS [sort],
    [status],
    [update_time]
FROM dbo.[category]
WHERE
    (@kw = '' OR RTRIM([name]) LIKE '%' + @kw + '%')
    AND (@type IS NULL OR [type] = @type)
    AND (@st IS NULL OR [status] = @st)
ORDER BY TRY_CONVERT(int, RTRIM([sort])) ASC, [id] DESC
OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY
", conn))
                {
                    cmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = kw;
                    cmd.Parameters.Add("@type", SqlDbType.TinyInt).Value = (object)type ?? DBNull.Value;
                    cmd.Parameters.Add("@st", SqlDbType.TinyInt).Value = (object)status ?? DBNull.Value;
                    cmd.Parameters.Add("@offset", SqlDbType.Int).Value = offset;
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = safeSize;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                            byte t = reader.IsDBNull(2) ? (byte)0 : Convert.ToByte(reader.GetValue(2));
                            string sortText = reader.IsDBNull(3) ? string.Empty : (reader.GetString(3) ?? string.Empty).Trim();
                            byte st = reader.IsDBNull(4) ? (byte)0 : Convert.ToByte(reader.GetValue(4));
                            DateTime? updateTime = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                            result.Add(new CategoryRow(id, name, t, sortText, st, updateTime));
                        }
                    }
                }
            }

            return result;
        }

        private void InsertCategory(string name, byte type, string sortText, byte status)
        {
            string safeName = TruncateByColumnLen("name", (name ?? string.Empty).Trim());
            string safeSort = TruncateByColumnLen("sort", NormalizeSortForDb(sortText));

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.[category]([name],[type],[sort],[status],[create_time],[update_time],[create_user],[update_user])
VALUES(@name,@type,@sort,@status,GETDATE(),GETDATE(),@userId,@userId)
", conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.NChar, 10).Value = (object)safeName ?? DBNull.Value;
                cmd.Parameters.Add("@type", SqlDbType.TinyInt).Value = type;
                cmd.Parameters.Add("@sort", SqlDbType.NChar, 10).Value = (object)safeSort ?? DBNull.Value;
                cmd.Parameters.Add("@status", SqlDbType.TinyInt).Value = status;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = currentUserId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateCategory(int id, string name, byte type, string sortText, byte status)
        {
            string safeName = TruncateByColumnLen("name", (name ?? string.Empty).Trim());
            string safeSort = TruncateByColumnLen("sort", NormalizeSortForDb(sortText));

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[category]
SET [name]=@name,
    [type]=@type,
    [sort]=@sort,
    [status]=@status,
    [update_time]=GETDATE(),
    [update_user]=@userId
WHERE [id]=@id
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@name", SqlDbType.NChar, 10).Value = (object)safeName ?? DBNull.Value;
                cmd.Parameters.Add("@type", SqlDbType.TinyInt).Value = type;
                cmd.Parameters.Add("@sort", SqlDbType.NChar, 10).Value = (object)safeSort ?? DBNull.Value;
                cmd.Parameters.Add("@status", SqlDbType.TinyInt).Value = status;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = currentUserId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateCategoryStatus(int id, byte status)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[category]
SET [status]=@status,
    [update_time]=GETDATE(),
    [update_user]=@userId
WHERE [id]=@id
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@status", SqlDbType.TinyInt).Value = status;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = currentUserId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteCategories(List<int> ids)
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
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[category] WHERE [id]=@id", conn, tx))
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

        private Dictionary<string, int> QueryColumnMaxLen()
        {
            Dictionary<string, int> result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME='category'
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
            if (!columnMaxLen.TryGetValue(columnName, out len) || len <= 0)
            {
                return value;
            }

            if (value.Length <= len)
            {
                return value;
            }

            return value.Substring(0, len);
        }

        private static string NormalizeSortForDb(string value)
        {
            string t = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                return string.Empty;
            }

            int parsed;
            if (!int.TryParse(t, out parsed) || parsed < 0)
            {
                return string.Empty;
            }

            return parsed.ToString();
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

        private static byte? ParseType(string value)
        {
            string v = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v) || string.Equals(v, "全部", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            if (v.Contains("菜品"))
            {
                return 1;
            }
            if (v.Contains("套餐"))
            {
                return 2;
            }
            return null;
        }

        private static byte? ParseStatus(string value)
        {
            string v = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v) || string.Equals(v, "全部", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            if (v.Contains("启"))
            {
                return 1;
            }
            if (v.Contains("禁"))
            {
                return 0;
            }
            return null;
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

        private sealed class CategoryRow
        {
            public int Id { get; }
            public string Name { get; }
            public byte Type { get; }
            public string SortText { get; }
            public bool Enabled { get; }
            public string UpdateTimeText { get; }

            public CategoryRow(int id, string name, byte type, string sortText, byte status, DateTime? updateTime)
            {
                Id = id;
                Name = (name ?? string.Empty).Trim();
                Type = type;
                SortText = (sortText ?? string.Empty).Trim();
                Enabled = status == 1;
                UpdateTimeText = updateTime.HasValue ? updateTime.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty;
            }
        }

        private sealed class CategoryEditForm : Form
        {
            private readonly TextBox textBoxName = new TextBox();
            private readonly TextBox textBoxSort = new TextBox();
            private readonly ComboBox comboBoxType = new ComboBox();
            private readonly ComboBox comboBoxStatus = new ComboBox();
            private readonly Button buttonOk = new Button();
            private readonly Button buttonCancel = new Button();

            public string CategoryName => (textBoxName.Text ?? string.Empty).Trim();
            public string SortText => (textBoxSort.Text ?? string.Empty).Trim();
            public byte CategoryType => comboBoxType.SelectedItem is ComboItem it ? it.Value : (byte)0;
            public bool IsEnabled => comboBoxStatus.SelectedItem is ComboItem it && it.Value == 1;

            public CategoryEditForm(string title, int fixedType, CategoryRow model)
            {
                Text = title;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(480, 240);

                Label labelName = new Label { Text = "分类名称", AutoSize = true, Location = new Point(24, 28) };
                textBoxName.Location = new Point(110, 24);
                textBoxName.Size = new Size(330, 28);
                textBoxName.MaxLength = 10;

                Label labelType = new Label { Text = "分类类型", AutoSize = true, Location = new Point(24, 74) };
                comboBoxType.Location = new Point(110, 70);
                comboBoxType.Size = new Size(160, 26);
                comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxType.Items.Add(new ComboItem("菜品分类", 1));
                comboBoxType.Items.Add(new ComboItem("套餐分类", 2));

                Label labelSort = new Label { Text = "排序", AutoSize = true, Location = new Point(290, 74) };
                textBoxSort.Location = new Point(340, 70);
                textBoxSort.Size = new Size(100, 28);
                textBoxSort.MaxLength = 10;

                Label labelStatus = new Label { Text = "状态", AutoSize = true, Location = new Point(24, 120) };
                comboBoxStatus.Location = new Point(110, 116);
                comboBoxStatus.Size = new Size(160, 26);
                comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxStatus.Items.Add(new ComboItem("启用", 1));
                comboBoxStatus.Items.Add(new ComboItem("禁用", 0));

                buttonOk.Text = "保存";
                buttonOk.Location = new Point(280, 176);
                buttonOk.Size = new Size(76, 32);
                buttonOk.BackColor = Color.FromArgb(255, 184, 0);
                buttonOk.FlatStyle = FlatStyle.Flat;
                buttonOk.FlatAppearance.BorderSize = 0;
                buttonOk.ForeColor = Color.White;
                buttonOk.Click += buttonOk_Click;

                buttonCancel.Text = "取消";
                buttonCancel.Location = new Point(364, 176);
                buttonCancel.Size = new Size(76, 32);
                buttonCancel.DialogResult = DialogResult.Cancel;

                Controls.Add(labelName);
                Controls.Add(textBoxName);
                Controls.Add(labelType);
                Controls.Add(comboBoxType);
                Controls.Add(labelSort);
                Controls.Add(textBoxSort);
                Controls.Add(labelStatus);
                Controls.Add(comboBoxStatus);
                Controls.Add(buttonOk);
                Controls.Add(buttonCancel);

                AcceptButton = buttonOk;
                CancelButton = buttonCancel;

                if (model != null)
                {
                    textBoxName.Text = model.Name;
                    textBoxSort.Text = model.SortText;
                    SelectComboValue(comboBoxType, model.Type);
                    SelectComboValue(comboBoxStatus, model.Enabled ? (byte)1 : (byte)0);
                    comboBoxType.Enabled = false;
                }
                else
                {
                    if (fixedType == 1 || fixedType == 2)
                    {
                        SelectComboValue(comboBoxType, (byte)fixedType);
                        comboBoxType.Enabled = false;
                    }
                    else
                    {
                        comboBoxType.SelectedIndex = 0;
                    }
                    comboBoxStatus.SelectedIndex = 0;
                }
            }

            private void buttonOk_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(CategoryName))
                {
                    MessageBox.Show("请输入分类名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxName.Focus();
                    return;
                }

                int s;
                if (!int.TryParse((SortText ?? string.Empty).Trim(), out s) || s < 0)
                {
                    MessageBox.Show("请输入正确的排序（非负整数）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxSort.Focus();
                    return;
                }

                if (CategoryType != 1 && CategoryType != 2)
                {
                    MessageBox.Show("请选择分类类型。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBoxType.Focus();
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }

            private static void SelectComboValue(ComboBox comboBox, byte value)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i] is ComboItem it && it.Value == value)
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }

            private sealed class ComboItem
            {
                public string Text { get; }
                public byte Value { get; }

                public ComboItem(string text, byte value)
                {
                    Text = text;
                    Value = value;
                }

                public override string ToString()
                {
                    return Text;
                }
            }
        }
    }
}
