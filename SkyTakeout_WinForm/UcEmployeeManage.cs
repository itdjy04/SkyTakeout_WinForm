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
    public partial class UcEmployeeManage : UserControl
    {
        private readonly int currentUserId;
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalCount = 0;
        private UcEmployeeEdit editPage;
        private long? editingEmployeeId;
        private bool hasLoaded;

        public UcEmployeeManage(int userId)
        {
            currentUserId = userId;
            InitializeComponent();
            Load += UcEmployeeManage_Load;
            VisibleChanged += UcEmployeeManage_VisibleChanged;
        }

        private void UcEmployeeManage_Load(object sender, EventArgs e)
        {
            comboBoxPageSize.SelectedIndex = 0;
            buttonPrev.Click += buttonPrev_Click;
            buttonNext.Click += buttonNext_Click;
            comboBoxPageSize.SelectedIndexChanged += comboBoxPageSize_SelectedIndexChanged;
            textBoxEmployeeName.KeyDown += textBoxEmployeeName_KeyDown;

            EnsureEmployeeTableExists();
            ReloadFromDatabase(true);
            hasLoaded = true;
        }

        private void UcEmployeeManage_VisibleChanged(object sender, EventArgs e)
        {
            if (!hasLoaded || !Visible)
            {
                return;
            }

            if (panelRoot != null && panelRoot.Visible)
            {
                ReloadFromDatabase(false);
            }
        }

        private void EnsureEditPage()
        {
            if (editPage != null)
            {
                return;
            }

            editPage = new UcEmployeeEdit();
            editPage.Visible = false;
            editPage.CancelRequested += () => ShowListPage(true, false);
            editPage.SaveRequested += EditPage_SaveRequested;
            Controls.Add(editPage);
            editPage.BringToFront();
        }

        private void ShowAddPage()
        {
            EnsureEditPage();
            editingEmployeeId = null;
            editPage.EnterAddMode(true);
            panelRoot.Visible = false;
            editPage.Visible = true;
            editPage.BringToFront();
        }

        private void ShowEditPage(EmployeeRow row)
        {
            if (row == null)
            {
                return;
            }

            EnsureEditPage();
            EmployeeEditModel model = QueryEmployeeById(row.Id);
            if (model == null)
            {
                MessageBox.Show("员工不存在或已被删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadFromDatabase(false);
                return;
            }

            editingEmployeeId = row.Id;
            editPage.EnterEditMode(model);
            panelRoot.Visible = false;
            editPage.Visible = true;
            editPage.BringToFront();
        }

        private void ShowListPage(bool reload, bool resetPage)
        {
            editingEmployeeId = null;
            if (editPage != null)
            {
                editPage.Visible = false;
            }
            panelRoot.Visible = true;
            panelRoot.BringToFront();
            if (reload)
            {
                ReloadFromDatabase(resetPage);
            }
        }

        private void EditPage_SaveRequested(EmployeeEditResult result)
        {
            try
            {
                if (result == null)
                {
                    return;
                }

                if (editingEmployeeId.HasValue)
                {
                    UpdateEmployee(editingEmployeeId.Value, result.Name, result.Phone, result.Sex, result.IdNumber);
                    ShowListPage(true, false);
                    return;
                }

                InsertEmployee(result.Username, result.Name, result.Phone, result.Sex, result.IdNumber);

                if (result.SaveAndContinue)
                {
                    editPage.EnterAddMode(true);
                    return;
                }

                ShowListPage(true, true);
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
                if (msg != null && msg.IndexOf("UX_employee_username", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show("账号已存在，请更换账号。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxEmployeeName_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadFromDatabase(true);
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

        private void ReloadFromDatabase(bool resetPage)
        {
            if (resetPage)
            {
                currentPage = 1;
            }

            string keyword = (textBoxEmployeeName.Text ?? string.Empty).Trim();
            pageSize = ParsePageSize(comboBoxPageSize.SelectedItem as string);

            int count;
            List<EmployeeRow> rows = QueryEmployees(keyword, currentPage, pageSize, out count);
            totalCount = count;
            BindRows(rows);
            UpdatePager();
        }

        private void BindRows(List<EmployeeRow> rows)
        {
            dataGridViewEmployee.SuspendLayout();
            dataGridViewEmployee.Rows.Clear();

            for (int i = 0; i < rows.Count; i++)
            {
                EmployeeRow r = rows[i];
                int rowIndex = dataGridViewEmployee.Rows.Add(r.Name, r.Username, r.Phone, r.Enabled ? "● 启用" : "● 禁用", r.UpdateTimeText, "修改", r.Enabled ? "禁用" : "启用");
                DataGridViewRow gridRow = dataGridViewEmployee.Rows[rowIndex];
                gridRow.Tag = r;
                gridRow.Height = 52;
            }

            dataGridViewEmployee.ResumeLayout();
        }

        private void dataGridViewEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewEmployee.Rows[e.RowIndex];
            EmployeeRow model = row.Tag as EmployeeRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colStatus.Index)
            {
                e.Value = model.Enabled ? "● 启用" : "● 禁用";
                e.FormattingApplied = true;
                DataGridViewCell cell = row.Cells[e.ColumnIndex];
                cell.Style.ForeColor = model.Enabled ? Color.FromArgb(35, 170, 88) : Color.FromArgb(144, 147, 153);
                return;
            }

            if (e.ColumnIndex == colToggle.Index)
            {
                DataGridViewLinkCell link = row.Cells[e.ColumnIndex] as DataGridViewLinkCell;
                if (link != null)
                {
                    link.LinkColor = model.Enabled ? Color.FromArgb(245, 108, 108) : Color.FromArgb(64, 158, 255);
                    link.ActiveLinkColor = link.LinkColor;
                }
            }
        }

        private void dataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridViewEmployee.Rows[e.RowIndex];
            EmployeeRow model = row.Tag as EmployeeRow;
            if (model == null)
            {
                return;
            }

            if (e.ColumnIndex == colEdit.Index)
            {
                ShowEditPage(model);
                return;
            }

            if (e.ColumnIndex == colToggle.Index)
            {
                int next = model.Enabled ? 0 : 1;
                string actionText = model.Enabled ? "禁用" : "启用";
                DialogResult confirm = MessageBox.Show($"确定要{actionText}员工：{model.Name}？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                UpdateEmployeeStatus(model.Id, next);
                ReloadFromDatabase(false);
            }
        }

        private void EnsureEmployeeTableExists()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[employee]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.[employee](
        [id] bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [name] varchar(32) NOT NULL,
        [username] varchar(32) NOT NULL,
        [password] varchar(64) NOT NULL,
        [phone] varchar(11) NULL,
        [sex] varchar(2) NULL,
        [id_number] varchar(18) NULL,
        [status] int NOT NULL CONSTRAINT [DF_employee_status] DEFAULT(1),
        [create_time] datetime NULL,
        [update_time] datetime NULL,
        [create_user] bigint NULL,
        [update_user] bigint NULL
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name]='UX_employee_username' AND [object_id]=OBJECT_ID('dbo.[employee]'))
BEGIN
    CREATE UNIQUE INDEX [UX_employee_username] ON dbo.[employee]([username]);
END
", conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private List<EmployeeRow> QueryEmployees(string keyword, int page, int size, out int count)
        {
            string kw = keyword ?? string.Empty;
            int safePage = page < 1 ? 1 : page;
            int safeSize = size < 1 ? 10 : size;
            int offset = (safePage - 1) * safeSize;

            List<EmployeeRow> result = new List<EmployeeRow>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand countCmd = new SqlCommand(@"
SELECT COUNT(1)
FROM dbo.[employee] e
WHERE (@kw = '' OR e.[name] LIKE '%' + @kw + '%')
", conn))
                {
                    countCmd.Parameters.Add("@kw", SqlDbType.VarChar, 32).Value = kw;
                    object raw = countCmd.ExecuteScalar();
                    count = raw == null || raw == DBNull.Value ? 0 : Convert.ToInt32(raw);
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT [id], RTRIM([name]) AS [name], RTRIM([username]) AS [username], RTRIM([phone]) AS [phone], [status], [update_time]
FROM dbo.[employee]
WHERE (@kw = '' OR [name] LIKE '%' + @kw + '%')
ORDER BY [id] DESC
OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY
", conn))
                {
                    cmd.Parameters.Add("@kw", SqlDbType.VarChar, 32).Value = kw;
                    cmd.Parameters.Add("@offset", SqlDbType.Int).Value = offset;
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = safeSize;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.IsDBNull(0) ? 0 : Convert.ToInt64(reader.GetValue(0));
                            string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                            string username = reader.IsDBNull(2) ? string.Empty : (reader.GetString(2) ?? string.Empty).Trim();
                            string phone = reader.IsDBNull(3) ? string.Empty : (reader.GetString(3) ?? string.Empty).Trim();
                            int st = reader.IsDBNull(4) ? 1 : Convert.ToInt32(reader.GetValue(4));
                            DateTime? updateTime = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                            result.Add(new EmployeeRow(id, name, username, phone, st, updateTime));
                        }
                    }
                }
            }

            return result;
        }

        private EmployeeEditModel QueryEmployeeById(long id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT [id], RTRIM([name]) AS [name], RTRIM([username]) AS [username], RTRIM([phone]) AS [phone], RTRIM([sex]) AS [sex], RTRIM([id_number]) AS [id_number], [status]
FROM dbo.[employee]
WHERE [id]=@id
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    long empId = reader.IsDBNull(0) ? 0 : Convert.ToInt64(reader.GetValue(0));
                    string name = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                    string username = reader.IsDBNull(2) ? string.Empty : (reader.GetString(2) ?? string.Empty).Trim();
                    string phone = reader.IsDBNull(3) ? string.Empty : (reader.GetString(3) ?? string.Empty).Trim();
                    string sex = reader.IsDBNull(4) ? string.Empty : (reader.GetString(4) ?? string.Empty).Trim();
                    string idNumber = reader.IsDBNull(5) ? string.Empty : (reader.GetString(5) ?? string.Empty).Trim();
                    int st = reader.IsDBNull(6) ? 1 : Convert.ToInt32(reader.GetValue(6));
                    return new EmployeeEditModel(empId, name, username, phone, sex, idNumber, st);
                }
            }
        }

        private void InsertEmployee(string username, string name, string phone, string sex, string idNumber)
        {
            string u = Truncate(username, 32);
            string n = Truncate(name, 32);
            string p = Truncate(phone, 11);
            string s = Truncate(sex, 2);
            string idn = Truncate(idNumber, 18);

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.[employee]([name],[username],[password],[phone],[sex],[id_number],[status],[create_time],[update_time],[create_user],[update_user])
VALUES(@name,@username,@password,@phone,@sex,@idNumber,1,GETDATE(),GETDATE(),@userId,@userId)
", conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 32).Value = n ?? string.Empty;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 32).Value = u ?? string.Empty;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 64).Value = "123456";
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 11).Value = (object)(p ?? string.Empty) ?? DBNull.Value;
                cmd.Parameters.Add("@sex", SqlDbType.VarChar, 2).Value = (object)(s ?? string.Empty) ?? DBNull.Value;
                cmd.Parameters.Add("@idNumber", SqlDbType.VarChar, 18).Value = (object)(idn ?? string.Empty) ?? DBNull.Value;
                cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = currentUserId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployee(long id, string name, string phone, string sex, string idNumber)
        {
            string n = Truncate(name, 32);
            string p = Truncate(phone, 11);
            string s = Truncate(sex, 2);
            string idn = Truncate(idNumber, 18);

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[employee]
SET [name]=@name,
    [phone]=@phone,
    [sex]=@sex,
    [id_number]=@idNumber,
    [update_time]=GETDATE(),
    [update_user]=@userId
WHERE [id]=@id
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 32).Value = n ?? string.Empty;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 11).Value = (object)(p ?? string.Empty) ?? DBNull.Value;
                cmd.Parameters.Add("@sex", SqlDbType.VarChar, 2).Value = (object)(s ?? string.Empty) ?? DBNull.Value;
                cmd.Parameters.Add("@idNumber", SqlDbType.VarChar, 18).Value = (object)(idn ?? string.Empty) ?? DBNull.Value;
                cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = currentUserId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployeeStatus(long id, int status)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[employee]
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

        private static string Truncate(string value, int max)
        {
            string v = (value ?? string.Empty).Trim();
            if (max <= 0 || v.Length <= max)
            {
                return v;
            }
            return v.Substring(0, max);
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

        private sealed class EmployeeRow
        {
            public long Id { get; }
            public string Name { get; }
            public string Username { get; }
            public string Phone { get; }
            public bool Enabled { get; }
            public string UpdateTimeText { get; }

            public EmployeeRow(long id, string name, string username, string phone, int status, DateTime? updateTime)
            {
                Id = id;
                Name = (name ?? string.Empty).Trim();
                Username = (username ?? string.Empty).Trim();
                Phone = (phone ?? string.Empty).Trim();
                Enabled = status == 1;
                UpdateTimeText = updateTime.HasValue ? updateTime.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty;
            }
        }
    }

    internal sealed class EmployeeEditModel
    {
        public long Id { get; }
        public string Name { get; }
        public string Username { get; }
        public string Phone { get; }
        public string Sex { get; }
        public string IdNumber { get; }
        public int Status { get; }

        public EmployeeEditModel(long id, string name, string username, string phone, string sex, string idNumber, int status)
        {
            Id = id;
            Name = name ?? string.Empty;
            Username = username ?? string.Empty;
            Phone = phone ?? string.Empty;
            Sex = sex ?? string.Empty;
            IdNumber = idNumber ?? string.Empty;
            Status = status;
        }
    }

    internal sealed class EmployeeEditResult
    {
        public string Username { get; }
        public string Name { get; }
        public string Phone { get; }
        public string Sex { get; }
        public string IdNumber { get; }
        public bool SaveAndContinue { get; }

        public EmployeeEditResult(string username, string name, string phone, string sex, string idNumber, bool saveAndContinue)
        {
            Username = username ?? string.Empty;
            Name = name ?? string.Empty;
            Phone = phone ?? string.Empty;
            Sex = sex ?? string.Empty;
            IdNumber = idNumber ?? string.Empty;
            SaveAndContinue = saveAndContinue;
        }
    }
}
