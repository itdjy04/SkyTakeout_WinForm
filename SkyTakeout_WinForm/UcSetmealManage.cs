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
    public partial class UcSetmealManage : UserControl
    {
        private readonly int currentUserId;
        private Dictionary<string, int> columnMaxLen = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalCount = 0;
        private bool suppressFilterReload = false;
        private bool isCategoryChangeSubscribed = false;
        private UcSetmealEdit editPage;
        private long? editingSetmealId;

        public UcSetmealManage(int userId)
        {
            currentUserId = userId;
            InitializeComponent();
            Load += UcSetmealManage_Load;
        }

        private void UcSetmealManage_Load(object sender, EventArgs e)
        {
            colToggle.UseColumnTextForLinkValue = false;
            comboBoxStatus.SelectedIndex = 0;
            comboBoxPageSize.SelectedIndex = 0;

            buttonPrev.Click += buttonPrev_Click;
            buttonNext.Click += buttonNext_Click;

            comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
            comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
            comboBoxPageSize.SelectedIndexChanged += comboBoxPageSize_SelectedIndexChanged;
            textBoxSetmealName.KeyDown += textBoxSetmealName_KeyDown;

            if (!isCategoryChangeSubscribed)
            {
                FormMain.CategoryChanged += FormMain_CategoryChanged;
                Disposed += UcSetmealManage_Disposed;
                isCategoryChangeSubscribed = true;
            }

            LoadCategoryOptions(false);
            columnMaxLen = QueryColumnMaxLen();
            ReloadFromDatabase(true);
        }

        private void EnsureEditPage()
        {
            if (editPage != null)
            {
                return;
            }

            editPage = new UcSetmealEdit();
            editPage.Visible = false;
            editPage.CancelRequested += () => ShowListPage(false);
            editPage.SaveRequested += EditPage_SaveRequested;
            Controls.Add(editPage);
            editPage.BringToFront();
        }

        private void ShowAddPage()
        {
            EnsureEditPage();
            editingSetmealId = null;
            List<SetmealOptionItem> categories = QuerySetmealCategoryOptionsForEdit();
            List<SkyTakeout_WinForm.DishOption> dishes = QueryDishOptionsForEdit();
            editPage.EnterAddMode(categories, dishes, true);
            panelRoot.Visible = false;
            editPage.Visible = true;
            editPage.BringToFront();
        }

        private void ShowEditPage(SetmealRow row)
        {
            if (row == null)
            {
                return;
            }

            EnsureEditPage();
            editingSetmealId = row.Id;

            List<SetmealOptionItem> categories = QuerySetmealCategoryOptionsForEdit();
            List<SkyTakeout_WinForm.DishOption> dishes = QueryDishOptionsForEdit();
            List<SkyTakeout_WinForm.SetmealDishItem> setmealDishes = QuerySetmealDishesForEdit(row.Id);
            SkyTakeout_WinForm.SetmealEditModel model = new SkyTakeout_WinForm.SetmealEditModel(row.Id, row.Name, row.CategoryId, row.PriceValue, row.Enabled ? 1 : 0, row.Description, row.ImagePath, setmealDishes);

            editPage.EnterEditMode(model, categories, dishes);
            panelRoot.Visible = false;
            editPage.Visible = true;
            editPage.BringToFront();
        }

        private void ShowListPage(bool reload)
        {
            editingSetmealId = null;
            if (editPage != null)
            {
                editPage.Visible = false;
            }

            panelRoot.Visible = true;
            panelRoot.BringToFront();

            if (reload)
            {
                ReloadFromDatabase(false);
            }
        }

        private void EditPage_SaveRequested(SkyTakeout_WinForm.SetmealEditResult result)
        {
            try
            {
                if (result == null)
                {
                    return;
                }

                List<SetmealDishItem> dishes = MapToLocalDishes(result.Dishes);

                if (editingSetmealId.HasValue)
                {
                    UpdateSetmeal(editingSetmealId.Value, result.Name, result.CategoryId, result.Price, result.Status, result.Description, result.ImagePath, dishes);
                    ShowListPage(true);
                    return;
                }

                InsertSetmeal(result.Name, result.CategoryId, result.Price, result.Status, result.Description, result.ImagePath, dishes);

                if (result.SaveAndContinue)
                {
                    List<SetmealOptionItem> categories = QuerySetmealCategoryOptionsForEdit();
                    List<SkyTakeout_WinForm.DishOption> dishOptions = QueryDishOptionsForEdit();
                    editPage.EnterAddMode(categories, dishOptions, true);
                    return;
                }

                ShowListPage(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshEditPageCategoryOptions()
        {
            if (editPage == null || !editPage.Visible)
            {
                return;
            }

            List<SetmealOptionItem> categories = QuerySetmealCategoryOptionsForEdit();
            editPage.UpdateCategoryOptions(categories, true);
        }

        private static List<SetmealDishItem> MapToLocalDishes(List<SkyTakeout_WinForm.SetmealDishItem> dishes)
        {
            List<SetmealDishItem> result = new List<SetmealDishItem>();
            if (dishes == null)
            {
                return result;
            }

            for (int i = 0; i < dishes.Count; i++)
            {
                SkyTakeout_WinForm.SetmealDishItem d = dishes[i];
                if (d == null)
                {
                    continue;
                }
                result.Add(new SetmealDishItem(d.DishId, d.Name, d.Price, d.Copies));
            }

            return result;
        }

        private List<SetmealOptionItem> QuerySetmealCategoryOptionsForEdit()
        {
            List<SetmealOptionItem> result = new List<SetmealOptionItem>();
            result.Add(new SetmealOptionItem("请选择", 0));
            List<ComboItem> options = QuerySetmealCategoryOptions();
            for (int i = 0; i < options.Count; i++)
            {
                ComboItem it = options[i];
                if (it != null && it.Value > 0)
                {
                    result.Add(new SetmealOptionItem(it.Text, it.Value));
                }
            }
            return result;
        }

        private List<SkyTakeout_WinForm.DishOption> QueryDishOptionsForEdit()
        {
            List<SkyTakeout_WinForm.DishOption> result = new List<SkyTakeout_WinForm.DishOption>();
            List<DishOption> local = QueryDishOptions();
            for (int i = 0; i < local.Count; i++)
            {
                DishOption d = local[i];
                result.Add(new SkyTakeout_WinForm.DishOption(d.Id, d.Name, d.Price));
            }
            return result;
        }

        private List<SkyTakeout_WinForm.SetmealDishItem> QuerySetmealDishesForEdit(long setmealId)
        {
            List<SkyTakeout_WinForm.SetmealDishItem> result = new List<SkyTakeout_WinForm.SetmealDishItem>();
            List<SetmealDishItem> local = QuerySetmealDishes(setmealId);
            for (int i = 0; i < local.Count; i++)
            {
                SetmealDishItem d = local[i];
                result.Add(new SkyTakeout_WinForm.SetmealDishItem(d.DishId, d.Name, d.Price, d.Copies));
            }
            return result;
        }

        private void UcSetmealManage_Disposed(object sender, EventArgs e)
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
                BeginInvoke(new Action(() =>
                {
                    LoadCategoryOptions(true);
                    RefreshEditPageCategoryOptions();
                }));
                return;
            }

            LoadCategoryOptions(true);
            RefreshEditPageCategoryOptions();
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

        private void textBoxSetmealName_KeyDown(object sender, KeyEventArgs e)
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
            ShowAddPage();
        }

        private void buttonBatchDelete_Click(object sender, EventArgs e)
        {
            List<long> ids = new List<long>();
            foreach (DataGridViewRow row in dataGridViewSetmeal.Rows)
            {
                bool isChecked = row.Cells[colSelect.Index].Value is bool b && b;
                if (isChecked && row.Tag is SetmealRow model)
                {
                    ids.Add(model.Id);
                }
            }

            ids = ids.Distinct().ToList();
            if (ids.Count == 0)
            {
                MessageBox.Show("请先勾选要删除的套餐。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show($"确定要批量删除 {ids.Count} 个套餐吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            DeleteSetmeals(ids);
            ReloadFromDatabase(true);
        }

        private void dataGridViewSetmeal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewSetmeal.Rows[e.RowIndex];
            SetmealRow model = row.Tag as SetmealRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colStatus.Index)
            {
                e.Value = model.Enabled ? "启售" : "停售";
                e.FormattingApplied = true;

                DataGridViewCell cell = row.Cells[e.ColumnIndex];
                cell.Style.ForeColor = model.Enabled ? Color.FromArgb(35, 170, 88) : Color.FromArgb(144, 147, 153);
                return;
            }
        }

        private void dataGridViewSetmeal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewSetmeal.Rows[e.RowIndex];
            SetmealRow model = row.Tag as SetmealRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colEdit.Index)
            {
                ShowEditPage(model);
                return;
            }

            if (e.ColumnIndex == colDelete.Index)
            {
                DialogResult confirm = MessageBox.Show($"确定删除套餐：{model.Name}？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                DeleteSetmeals(new List<long> { model.Id });
                ReloadFromDatabase(false);
                return;
            }

            if (e.ColumnIndex == colToggle.Index)
            {
                int next = model.Enabled ? 0 : 1;
                UpdateSetmealStatus(model.Id, next);
                ReloadFromDatabase(false);
            }
        }

        private void BindRows(List<SetmealRow> rows)
        {
            dataGridViewSetmeal.SuspendLayout();
            dataGridViewSetmeal.Rows.Clear();

            for (int i = 0; i < rows.Count; i++)
            {
                SetmealRow r = rows[i];
                int rowIndex = dataGridViewSetmeal.Rows.Add(false, r.Name, r.Image, r.CategoryName, r.PriceText, r.Enabled ? "启售" : "停售", r.UpdateTimeText, "修改", "删除", r.Enabled ? "停售" : "启售");
                DataGridViewRow gridRow = dataGridViewSetmeal.Rows[rowIndex];
                gridRow.Tag = r;
                gridRow.Height = 52;
            }

            labelTotal.Text = $"共 {rows.Count} 条";
            dataGridViewSetmeal.ResumeLayout();
        }

        private void ReloadFromDatabase(bool resetPage)
        {
            if (resetPage)
            {
                currentPage = 1;
            }

            string keyword = (textBoxSetmealName.Text ?? string.Empty).Trim();
            long? categoryId = GetSelectedCategoryId();
            int? status = ParseStatus(comboBoxStatus.SelectedItem as string);
            pageSize = ParsePageSize(comboBoxPageSize.SelectedItem as string);

            int count;
            List<SetmealRow> rows = QuerySetmeals(keyword, categoryId, status, currentPage, pageSize, out count);
            totalCount = count;
            BindRows(rows);
            UpdatePager();
        }

        private long? GetSelectedCategoryId()
        {
            ComboItem it = comboBoxCategory.SelectedItem as ComboItem;
            if (it == null || it.Value <= 0)
            {
                return null;
            }
            return it.Value;
        }

        private void LoadCategoryOptions(bool preserveSelection)
        {
            long prev = -1;
            if (preserveSelection && comboBoxCategory.SelectedItem is ComboItem prevItem)
            {
                prev = prevItem.Value;
            }

            suppressFilterReload = true;
            comboBoxCategory.Items.Clear();
            comboBoxCategory.Items.Add(new ComboItem("全部", 0));

            List<ComboItem> options = QuerySetmealCategoryOptions();
            for (int i = 0; i < options.Count; i++)
            {
                comboBoxCategory.Items.Add(options[i]);
            }

            int selectedIndex = 0;
            if (prev > 0)
            {
                for (int i = 0; i < comboBoxCategory.Items.Count; i++)
                {
                    if (comboBoxCategory.Items[i] is ComboItem it && it.Value == prev)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            comboBoxCategory.SelectedIndex = selectedIndex;
            suppressFilterReload = false;
        }

        private List<ComboItem> QuerySetmealCategoryOptions()
        {
            List<ComboItem> result = new List<ComboItem>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT [id], RTRIM([name]) AS [name]
FROM dbo.[category]
WHERE [type] = 2 AND ([status] IS NULL OR [status] = 1) AND [name] IS NOT NULL
ORDER BY TRY_CONVERT(int, RTRIM([sort])) ASC, [id] DESC
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = reader.IsDBNull(0) ? 0 : Convert.ToInt64(reader.GetValue(0));
                        string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                        if (id > 0 && !string.IsNullOrWhiteSpace(name))
                        {
                            result.Add(new ComboItem(name, id));
                        }
                    }
                }
            }
            return result;
        }

        private List<SetmealRow> QuerySetmeals(string keyword, long? categoryId, int? status, int page, int size, out int count)
        {
            string kw = keyword ?? string.Empty;
            long? catId = categoryId;
            int safePage = page < 1 ? 1 : page;
            int safeSize = size < 1 ? 10 : size;
            int offset = (safePage - 1) * safeSize;

            List<SetmealRow> result = new List<SetmealRow>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand countCmd = new SqlCommand(@"
SELECT COUNT(1)
FROM dbo.[setmeal] s
WHERE
    (@kw = '' OR s.[name] LIKE '%' + @kw + '%')
    AND (@catId IS NULL OR s.[category_id] = @catId)
    AND (@st IS NULL OR s.[status] = @st)
", conn))
                {
                    countCmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = kw;
                    countCmd.Parameters.Add("@catId", SqlDbType.BigInt).Value = (object)catId ?? DBNull.Value;
                    countCmd.Parameters.Add("@st", SqlDbType.Int).Value = (object)status ?? DBNull.Value;
                    object raw = countCmd.ExecuteScalar();
                    count = raw == null || raw == DBNull.Value ? 0 : Convert.ToInt32(raw);
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT
    s.[id],
    s.[name],
    s.[category_id],
    RTRIM(c.[name]) AS category_name,
    s.[price],
    s.[status],
    s.[update_time],
    s.[image],
    s.[description]
FROM dbo.[setmeal] s
LEFT JOIN dbo.[category] c ON s.[category_id] = c.[id]
WHERE
    (@kw = '' OR s.[name] LIKE '%' + @kw + '%')
    AND (@catId IS NULL OR s.[category_id] = @catId)
    AND (@st IS NULL OR s.[status] = @st)
ORDER BY s.[id] DESC
OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY
", conn))
                {
                    cmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = kw;
                    cmd.Parameters.Add("@catId", SqlDbType.BigInt).Value = (object)catId ?? DBNull.Value;
                    cmd.Parameters.Add("@st", SqlDbType.Int).Value = (object)status ?? DBNull.Value;
                    cmd.Parameters.Add("@offset", SqlDbType.Int).Value = offset;
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = safeSize;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.IsDBNull(0) ? 0 : Convert.ToInt64(reader.GetValue(0));
                            string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                            long categoryIdValue = reader.IsDBNull(2) ? 0 : Convert.ToInt64(reader.GetValue(2));
                            string categoryName = reader.IsDBNull(3) ? string.Empty : (reader.GetString(3) ?? string.Empty).Trim();
                            decimal price = reader.IsDBNull(4) ? 0m : Convert.ToDecimal(reader.GetValue(4));
                            int st = reader.IsDBNull(5) ? 0 : Convert.ToInt32(reader.GetValue(5));
                            DateTime? updateTime = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                            string image = reader.IsDBNull(7) ? string.Empty : (reader.GetString(7) ?? string.Empty).Trim();
                            string desc = reader.IsDBNull(8) ? string.Empty : (reader.GetString(8) ?? string.Empty).Trim();

                            result.Add(new SetmealRow(id, name, categoryIdValue, categoryName, price, st, updateTime, image, desc));
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

        private static int? ParseStatus(string value)
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
            if (v.Contains("停"))
            {
                return 0;
            }
            return null;
        }

        private void UpdateSetmealStatus(long id, int status)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[setmeal]
SET [status]=@status,
    [update_time]=GETDATE(),
    [update_user]=@userId
WHERE [id]=@id
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = currentUserId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteSetmeals(List<long> ids)
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
                            using (SqlCommand cmd1 = new SqlCommand("DELETE FROM dbo.[setmeal_dish] WHERE [setmeal_id]=@id", conn, tx))
                            {
                                cmd1.Parameters.Add("@id", SqlDbType.BigInt).Value = ids[i];
                                cmd1.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd2 = new SqlCommand("DELETE FROM dbo.[setmeal] WHERE [id]=@id", conn, tx))
                            {
                                cmd2.Parameters.Add("@id", SqlDbType.BigInt).Value = ids[i];
                                cmd2.ExecuteNonQuery();
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

        private void InsertSetmeal(string name, long categoryId, decimal price, int status, string description, string imagePath, List<SetmealDishItem> dishes)
        {
            string safeName = TruncateByColumnLen("name", (name ?? string.Empty).Trim());
            string safeDesc = TruncateByColumnLen("description", (description ?? string.Empty).Trim());
            string safeImage = TruncateByColumnLen("image", (imagePath ?? string.Empty).Trim());

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        long newId;
                        using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.[setmeal]([category_id],[name],[price],[status],[description],[image],[create_time],[update_time],[create_user],[update_user])
VALUES(@categoryId,@name,@price,@status,@desc,@image,GETDATE(),GETDATE(),@userId,@userId);
SELECT CAST(SCOPE_IDENTITY() AS bigint);
", conn, tx))
                        {
                            cmd.Parameters.Add("@categoryId", SqlDbType.BigInt).Value = categoryId;
                            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 32).Value = (object)safeName ?? DBNull.Value;
                            SqlParameter priceParam = cmd.Parameters.Add("@price", SqlDbType.Decimal);
                            priceParam.Precision = 10;
                            priceParam.Scale = 2;
                            priceParam.Value = price;
                            cmd.Parameters.Add("@status", SqlDbType.Int).Value = status;
                            cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 255).Value = (object)safeDesc ?? DBNull.Value;
                            cmd.Parameters.Add("@image", SqlDbType.NVarChar, 255).Value = (object)safeImage ?? DBNull.Value;
                            cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = currentUserId;
                            object raw = cmd.ExecuteScalar();
                            newId = raw == null || raw == DBNull.Value ? 0 : Convert.ToInt64(raw);
                        }

                        if (newId > 0 && dishes != null && dishes.Count > 0)
                        {
                            InsertSetmealDishes(conn, tx, newId, dishes);
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

        private void UpdateSetmeal(long id, string name, long categoryId, decimal price, int status, string description, string imagePath, List<SetmealDishItem> dishes)
        {
            string safeName = TruncateByColumnLen("name", (name ?? string.Empty).Trim());
            string safeDesc = TruncateByColumnLen("description", (description ?? string.Empty).Trim());
            string safeImage = TruncateByColumnLen("image", (imagePath ?? string.Empty).Trim());

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[setmeal]
SET [category_id]=@categoryId,
    [name]=@name,
    [price]=@price,
    [status]=@status,
    [description]=@desc,
    [image]=@image,
    [update_time]=GETDATE(),
    [update_user]=@userId
WHERE [id]=@id
", conn, tx))
                        {
                            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                            cmd.Parameters.Add("@categoryId", SqlDbType.BigInt).Value = categoryId;
                            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 32).Value = (object)safeName ?? DBNull.Value;
                            SqlParameter priceParam = cmd.Parameters.Add("@price", SqlDbType.Decimal);
                            priceParam.Precision = 10;
                            priceParam.Scale = 2;
                            priceParam.Value = price;
                            cmd.Parameters.Add("@status", SqlDbType.Int).Value = status;
                            cmd.Parameters.Add("@desc", SqlDbType.NVarChar, 255).Value = (object)safeDesc ?? DBNull.Value;
                            cmd.Parameters.Add("@image", SqlDbType.NVarChar, 255).Value = (object)safeImage ?? DBNull.Value;
                            cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = currentUserId;
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand del = new SqlCommand("DELETE FROM dbo.[setmeal_dish] WHERE [setmeal_id]=@id", conn, tx))
                        {
                            del.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                            del.ExecuteNonQuery();
                        }

                        if (dishes != null && dishes.Count > 0)
                        {
                            InsertSetmealDishes(conn, tx, id, dishes);
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

        private void InsertSetmealDishes(SqlConnection conn, SqlTransaction tx, long setmealId, List<SetmealDishItem> dishes)
        {
            for (int i = 0; i < dishes.Count; i++)
            {
                SetmealDishItem d = dishes[i];
                using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.[setmeal_dish]([setmeal_id],[dish_id],[name],[price],[copies])
VALUES(@setmealId,@dishId,@name,@price,@copies)
", conn, tx))
                {
                    cmd.Parameters.Add("@setmealId", SqlDbType.BigInt).Value = setmealId;
                    cmd.Parameters.Add("@dishId", SqlDbType.BigInt).Value = d.DishId;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 32).Value = (object)(d.Name ?? string.Empty) ?? DBNull.Value;
                    SqlParameter priceParam = cmd.Parameters.Add("@price", SqlDbType.Decimal);
                    priceParam.Precision = 10;
                    priceParam.Scale = 2;
                    priceParam.Value = d.Price;
                    cmd.Parameters.Add("@copies", SqlDbType.Int).Value = d.Copies;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private List<SetmealDishItem> QuerySetmealDishes(long setmealId)
        {
            List<SetmealDishItem> result = new List<SetmealDishItem>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT [dish_id], [name], [price], [copies]
FROM dbo.[setmeal_dish]
WHERE [setmeal_id]=@id
ORDER BY [id] ASC
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = setmealId;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long dishId = reader.IsDBNull(0) ? 0 : Convert.ToInt64(reader.GetValue(0));
                        string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                        decimal price = reader.IsDBNull(2) ? 0m : Convert.ToDecimal(reader.GetValue(2));
                        int copies = reader.IsDBNull(3) ? 1 : Convert.ToInt32(reader.GetValue(3));
                        result.Add(new SetmealDishItem(dishId, name, price, copies));
                    }
                }
            }
            return result;
        }

        private List<DishOption> QueryDishOptions()
        {
            List<DishOption> result = new List<DishOption>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT [id], RTRIM([name]) AS [name], RTRIM([price]) AS [price]
FROM dbo.[dish]
WHERE [id] IS NOT NULL
ORDER BY [id] DESC
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = reader.IsDBNull(0) ? 0 : Convert.ToInt64(reader.GetValue(0));
                        string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                        string priceText = reader.IsDBNull(2) ? string.Empty : (reader.GetString(2) ?? string.Empty).Trim();
                        decimal price;
                        if (id > 0 && !string.IsNullOrWhiteSpace(name) && TryParseMoney(priceText, out price))
                        {
                            result.Add(new DishOption(id, name, price));
                        }
                    }
                }
            }
            return result;
        }

        private static bool TryParseMoney(string value, out decimal result)
        {
            string t = (value ?? string.Empty).Trim();
            if (t.EndsWith("元", StringComparison.OrdinalIgnoreCase))
            {
                t = t.Substring(0, t.Length - 1).Trim();
            }
            return decimal.TryParse(t, out result);
        }

        private Dictionary<string, int> QueryColumnMaxLen()
        {
            Dictionary<string, int> result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME='setmeal'
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

        private string GetConnectionString()
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["SkyTakeout"];
            if (cs != null && !string.IsNullOrWhiteSpace(cs.ConnectionString))
            {
                return cs.ConnectionString;
            }

            return @"Data Source=.\SQLEXPRESS;Initial Catalog=SkyTakeout;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        }

        private sealed class ComboItem
        {
            public string Text { get; }
            public long Value { get; }

            public ComboItem(string text, long value)
            {
                Text = text ?? string.Empty;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private sealed class SetmealRow
        {
            public long Id { get; }
            public string Name { get; }
            public long CategoryId { get; }
            public string CategoryName { get; }
            public decimal PriceValue { get; }
            public string PriceText { get; }
            public bool Enabled { get; }
            public string UpdateTimeText { get; }
            public string ImagePath { get; }
            public Image Image { get; }
            public string Description { get; }

            public SetmealRow(long id, string name, long categoryId, string categoryName, decimal price, int status, DateTime? updateTime, string imagePath, string description)
            {
                Id = id;
                Name = (name ?? string.Empty).Trim();
                CategoryId = categoryId;
                CategoryName = (categoryName ?? string.Empty).Trim();
                PriceValue = price;
                PriceText = price.ToString("0.##") + "元";
                Enabled = status == 1;
                UpdateTimeText = updateTime.HasValue ? updateTime.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty;
                ImagePath = (imagePath ?? string.Empty).Trim();
                Image = LoadImageOrPlaceholder(ImagePath);
                Description = (description ?? string.Empty).Trim();
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

        private static Image LoadImageOrPlaceholder(string value)
        {
            string v = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
            {
                return SetmealRow.CreatePlaceholderImage();
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

            return SetmealRow.CreatePlaceholderImage();
        }

        private sealed class DishOption
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

        private sealed class SetmealDishItem
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

        private sealed class SetmealEditModel
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

        private sealed class SetmealEditForm : Form
        {
            private readonly List<ComboItem> categoryOptions;
            private readonly List<DishOption> dishOptions;
            private readonly TextBox textBoxName = new TextBox();
            private readonly ComboBox comboBoxCategory = new ComboBox();
            private readonly TextBox textBoxPrice = new TextBox();
            private readonly ComboBox comboBoxStatus = new ComboBox();
            private readonly TextBox textBoxDesc = new TextBox();
            private readonly TextBox textBoxImage = new TextBox();
            private readonly Button buttonBrowse = new Button();
            private readonly DataGridView dataGridViewDishes = new DataGridView();
            private readonly Button buttonAddDish = new Button();
            private readonly Button buttonRemoveDish = new Button();
            private readonly Button buttonOk = new Button();
            private readonly Button buttonCancel = new Button();

            private readonly BindingSource dishBinding = new BindingSource();
            private readonly List<SetmealDishItem> dishes = new List<SetmealDishItem>();

            public string SetmealName => (textBoxName.Text ?? string.Empty).Trim();
            public long CategoryId => comboBoxCategory.SelectedItem is ComboItem it ? it.Value : 0;
            public decimal Price => ParseDecimalSafe(textBoxPrice.Text);
            public int Status => comboBoxStatus.SelectedItem is ComboItem it ? (int)it.Value : 1;
            public string DescriptionText => (textBoxDesc.Text ?? string.Empty).Trim();
            public string ImagePath => (textBoxImage.Text ?? string.Empty).Trim();
            public List<SetmealDishItem> Dishes => dishes.ToList();

            public SetmealEditForm(string title, List<ComboItem> categoryOptions, List<DishOption> dishOptions, SetmealEditModel model)
            {
                Text = title;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(720, 520);

                this.categoryOptions = categoryOptions ?? new List<ComboItem>();
                this.dishOptions = dishOptions ?? new List<DishOption>();

                Label labelName = new Label { Text = "套餐名称", AutoSize = true, Location = new Point(24, 24) };
                textBoxName.Location = new Point(110, 20);
                textBoxName.Size = new Size(260, 28);
                textBoxName.MaxLength = 32;

                Label labelCategory = new Label { Text = "套餐分类", AutoSize = true, Location = new Point(24, 66) };
                comboBoxCategory.Location = new Point(110, 62);
                comboBoxCategory.Size = new Size(260, 26);
                comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxCategory.Items.AddRange(this.categoryOptions.Cast<object>().ToArray());
                if (comboBoxCategory.Items.Count > 0)
                {
                    comboBoxCategory.SelectedIndex = 0;
                }

                Label labelPrice = new Label { Text = "套餐价格", AutoSize = true, Location = new Point(400, 66) };
                textBoxPrice.Location = new Point(486, 62);
                textBoxPrice.Size = new Size(200, 28);

                Label labelStatus = new Label { Text = "售卖状态", AutoSize = true, Location = new Point(400, 24) };
                comboBoxStatus.Location = new Point(486, 20);
                comboBoxStatus.Size = new Size(200, 26);
                comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxStatus.Items.Add(new ComboItem("启售", 1));
                comboBoxStatus.Items.Add(new ComboItem("停售", 0));
                comboBoxStatus.SelectedIndex = 0;

                Label labelDesc = new Label { Text = "描述", AutoSize = true, Location = new Point(24, 108) };
                textBoxDesc.Location = new Point(110, 104);
                textBoxDesc.Size = new Size(576, 60);
                textBoxDesc.Multiline = true;
                textBoxDesc.MaxLength = 255;

                Label labelImage = new Label { Text = "图片路径", AutoSize = true, Location = new Point(24, 182) };
                textBoxImage.Location = new Point(110, 178);
                textBoxImage.Size = new Size(470, 28);
                buttonBrowse.Text = "浏览";
                buttonBrowse.Location = new Point(590, 177);
                buttonBrowse.Size = new Size(96, 30);
                buttonBrowse.Click += buttonBrowse_Click;

                Label labelDish = new Label { Text = "套餐菜品", AutoSize = true, Location = new Point(24, 228) };
                dataGridViewDishes.Location = new Point(110, 224);
                dataGridViewDishes.Size = new Size(576, 210);
                dataGridViewDishes.AllowUserToAddRows = false;
                dataGridViewDishes.AllowUserToDeleteRows = false;
                dataGridViewDishes.ReadOnly = true;
                dataGridViewDishes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewDishes.RowHeadersVisible = false;
                dataGridViewDishes.AutoGenerateColumns = false;
                dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "菜品ID", DataPropertyName = "DishId", Width = 90 });
                dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "名称", DataPropertyName = "Name", Width = 220 });
                dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "价格", DataPropertyName = "PriceText", Width = 90 });
                dataGridViewDishes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "份数", DataPropertyName = "Copies", Width = 70 });
                dishBinding.DataSource = new List<DishGridRow>();
                dataGridViewDishes.DataSource = dishBinding;

                buttonAddDish.Text = "添加菜品";
                buttonAddDish.Location = new Point(110, 442);
                buttonAddDish.Size = new Size(96, 32);
                buttonAddDish.Click += buttonAddDish_Click;

                buttonRemoveDish.Text = "移除菜品";
                buttonRemoveDish.Location = new Point(214, 442);
                buttonRemoveDish.Size = new Size(96, 32);
                buttonRemoveDish.Click += buttonRemoveDish_Click;

                buttonOk.Text = "保存";
                buttonOk.Location = new Point(510, 442);
                buttonOk.Size = new Size(80, 34);
                buttonOk.BackColor = Color.FromArgb(255, 184, 0);
                buttonOk.FlatStyle = FlatStyle.Flat;
                buttonOk.FlatAppearance.BorderSize = 0;
                buttonOk.ForeColor = Color.White;
                buttonOk.Click += buttonOk_Click;

                buttonCancel.Text = "取消";
                buttonCancel.Location = new Point(606, 442);
                buttonCancel.Size = new Size(80, 34);
                buttonCancel.DialogResult = DialogResult.Cancel;

                Controls.Add(labelName);
                Controls.Add(textBoxName);
                Controls.Add(labelCategory);
                Controls.Add(comboBoxCategory);
                Controls.Add(labelPrice);
                Controls.Add(textBoxPrice);
                Controls.Add(labelStatus);
                Controls.Add(comboBoxStatus);
                Controls.Add(labelDesc);
                Controls.Add(textBoxDesc);
                Controls.Add(labelImage);
                Controls.Add(textBoxImage);
                Controls.Add(buttonBrowse);
                Controls.Add(labelDish);
                Controls.Add(dataGridViewDishes);
                Controls.Add(buttonAddDish);
                Controls.Add(buttonRemoveDish);
                Controls.Add(buttonOk);
                Controls.Add(buttonCancel);

                AcceptButton = buttonOk;
                CancelButton = buttonCancel;

                if (model != null)
                {
                    textBoxName.Text = model.Name;
                    textBoxPrice.Text = model.Price.ToString("0.##");
                    textBoxDesc.Text = model.Description ?? string.Empty;
                    textBoxImage.Text = model.ImagePath ?? string.Empty;

                    for (int i = 0; i < comboBoxCategory.Items.Count; i++)
                    {
                        if (comboBoxCategory.Items[i] is ComboItem it && it.Value == model.CategoryId)
                        {
                            comboBoxCategory.SelectedIndex = i;
                            break;
                        }
                    }

                    for (int i = 0; i < comboBoxStatus.Items.Count; i++)
                    {
                        if (comboBoxStatus.Items[i] is ComboItem it && it.Value == model.Status)
                        {
                            comboBoxStatus.SelectedIndex = i;
                            break;
                        }
                    }

                    dishes.Clear();
                    if (model.Dishes != null)
                    {
                        dishes.AddRange(model.Dishes);
                    }
                    RebindDishGrid();
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

            private void buttonOk_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(SetmealName))
                {
                    MessageBox.Show("请输入套餐名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxName.Focus();
                    return;
                }

                if (CategoryId <= 0)
                {
                    MessageBox.Show("请选择套餐分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBoxCategory.Focus();
                    return;
                }

                decimal price = Price;
                if (price < 0)
                {
                    MessageBox.Show("请输入正确的套餐价格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxPrice.Focus();
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }

            private static decimal ParseDecimalSafe(string text)
            {
                decimal v;
                if (decimal.TryParse((text ?? string.Empty).Trim(), out v))
                {
                    return v;
                }
                return 0m;
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
                buttonSearch.Click += buttonSearch_Click;

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
                grid.DoubleClick += grid_DoubleClick;

                buttonOk.Text = "确定";
                buttonOk.Location = new Point(344, 370);
                buttonOk.Size = new Size(76, 32);
                buttonOk.Click += buttonOk_Click;

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

            private void buttonSearch_Click(object sender, EventArgs e)
            {
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

            private void grid_DoubleClick(object sender, EventArgs e)
            {
                if (TryPick())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }

            private void buttonOk_Click(object sender, EventArgs e)
            {
                if (!TryPick())
                {
                    MessageBox.Show("请选择一个菜品。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
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
}
