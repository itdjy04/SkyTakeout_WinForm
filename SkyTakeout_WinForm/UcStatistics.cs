using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcStatistics : UserControl
    {
        private bool hasLoaded;

        public UcStatistics()
        {
            InitializeComponent();
            InitTable(gridSetmeal);
            InitTable(gridEmployee);
            WireEvents();
        }

        private static void InitTable(DataGridView grid)
        {
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = Color.White;
        }

        private void WireEvents()
        {
            Load += (s, e) =>
            {
                if (hasLoaded)
                {
                    return;
                }

                hasLoaded = true;
                RefreshStats();
            };

            VisibleChanged += (s, e) =>
            {
                if (Visible)
                {
                    RefreshStats();
                }
            };

            buttonRefresh.Click += (s, e) => RefreshStats();
        }

        private void RefreshStats()
        {
            try
            {
                (int total, int onSale, int offSale) dish = QueryDishStats();
                labelDishValue.Text = $"总数：{dish.total}    启售：{dish.onSale}    停售：{dish.offSale}";
            }
            catch
            {
                labelDishValue.Text = "无法读取数据（dish 表不存在或字段不匹配）";
            }

            try
            {
                (int total, int onSale, int offSale) setmeal = QuerySetmealStats();
                labelSetmealValue.Text = $"总数：{setmeal.total}    启售：{setmeal.onSale}    停售：{setmeal.offSale}";
            }
            catch
            {
                labelSetmealValue.Text = "无法读取数据（setmeal 表不存在或字段不匹配）";
            }

            try
            {
                CategoryStats category = QueryCategoryStats();
                labelCategoryValue.Text = $"菜品分类：{category.DishTotal}（启用 {category.DishEnabled} / 禁用 {category.DishDisabled}）    套餐分类：{category.SetmealTotal}（启用 {category.SetmealEnabled} / 禁用 {category.SetmealDisabled}）";
            }
            catch
            {
                labelCategoryValue.Text = "无法读取数据（category 表不存在或字段不匹配）";
            }

            try
            {
                (int total, int enabled, int disabled) employee = QueryEmployeeStats();
                labelEmployeeValue.Text = $"总数：{employee.total}    启用：{employee.enabled}    禁用：{employee.disabled}";
            }
            catch
            {
                labelEmployeeValue.Text = "无法读取数据（employee 表不存在或字段不匹配）";
            }

            try
            {
                gridSetmeal.DataSource = QueryRecentSetmeals();
            }
            catch
            {
                gridSetmeal.DataSource = null;
            }

            try
            {
                gridEmployee.DataSource = QueryRecentEmployees();
            }
            catch
            {
                gridEmployee.DataSource = null;
            }
        }

        private struct CategoryStats
        {
            public int DishTotal;
            public int DishEnabled;
            public int DishDisabled;
            public int SetmealTotal;
            public int SetmealEnabled;
            public int SetmealDisabled;
        }

        private (int total, int onSale, int offSale) QueryDishStats()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[dish]', 'U') IS NULL
BEGIN
    SELECT CAST(0 AS int) AS [total], CAST(0 AS int) AS [onSale], CAST(0 AS int) AS [offSale];
END
ELSE
BEGIN
    SELECT
        COUNT(1) AS [total],
        SUM(CASE WHEN RTRIM([status]) = N'启售' THEN 1 ELSE 0 END) AS [onSale],
        SUM(CASE WHEN RTRIM([status]) = N'停售' THEN 1 ELSE 0 END) AS [offSale]
    FROM dbo.[dish];
END
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return (0, 0, 0);
                    }

                    int total = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0));
                    int onSale = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    int offSale = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2));
                    return (total, onSale, offSale);
                }
            }
        }

        private (int total, int onSale, int offSale) QuerySetmealStats()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[setmeal]', 'U') IS NULL
BEGIN
    SELECT CAST(0 AS int) AS [total], CAST(0 AS int) AS [onSale], CAST(0 AS int) AS [offSale];
END
ELSE
BEGIN
    SELECT
        COUNT(1) AS [total],
        SUM(CASE WHEN [status] = 1 THEN 1 ELSE 0 END) AS [onSale],
        SUM(CASE WHEN [status] = 0 THEN 1 ELSE 0 END) AS [offSale]
    FROM dbo.[setmeal];
END
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return (0, 0, 0);
                    }

                    int total = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0));
                    int onSale = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    int offSale = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2));
                    return (total, onSale, offSale);
                }
            }
        }

        private CategoryStats QueryCategoryStats()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[category]', 'U') IS NULL
BEGIN
    SELECT
        CAST(0 AS int) AS [dishTotal],
        CAST(0 AS int) AS [dishEnabled],
        CAST(0 AS int) AS [dishDisabled],
        CAST(0 AS int) AS [setmealTotal],
        CAST(0 AS int) AS [setmealEnabled],
        CAST(0 AS int) AS [setmealDisabled];
END
ELSE
BEGIN
    SELECT
        SUM(CASE WHEN [type] = 1 THEN 1 ELSE 0 END) AS [dishTotal],
        SUM(CASE WHEN [type] = 1 AND [status] = 1 THEN 1 ELSE 0 END) AS [dishEnabled],
        SUM(CASE WHEN [type] = 1 AND [status] = 0 THEN 1 ELSE 0 END) AS [dishDisabled],
        SUM(CASE WHEN [type] = 2 THEN 1 ELSE 0 END) AS [setmealTotal],
        SUM(CASE WHEN [type] = 2 AND [status] = 1 THEN 1 ELSE 0 END) AS [setmealEnabled],
        SUM(CASE WHEN [type] = 2 AND [status] = 0 THEN 1 ELSE 0 END) AS [setmealDisabled]
    FROM dbo.[category];
END
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return new CategoryStats();
                    }

                    CategoryStats s = new CategoryStats();
                    s.DishTotal = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0));
                    s.DishEnabled = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    s.DishDisabled = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2));
                    s.SetmealTotal = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3));
                    s.SetmealEnabled = reader.IsDBNull(4) ? 0 : Convert.ToInt32(reader.GetValue(4));
                    s.SetmealDisabled = reader.IsDBNull(5) ? 0 : Convert.ToInt32(reader.GetValue(5));
                    return s;
                }
            }
        }

        private (int total, int enabled, int disabled) QueryEmployeeStats()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[employee]', 'U') IS NULL
BEGIN
    SELECT CAST(0 AS int) AS [total], CAST(0 AS int) AS [enabled], CAST(0 AS int) AS [disabled];
END
ELSE
BEGIN
    SELECT
        COUNT(1) AS [total],
        SUM(CASE WHEN [status] = 1 THEN 1 ELSE 0 END) AS [enabled],
        SUM(CASE WHEN [status] = 0 THEN 1 ELSE 0 END) AS [disabled]
    FROM dbo.[employee];
END
", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return (0, 0, 0);
                    }

                    int total = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0));
                    int enabled = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    int disabled = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2));
                    return (total, enabled, disabled);
                }
            }
        }

        private DataTable QueryRecentSetmeals()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[setmeal]', 'U') IS NULL
BEGIN
    SELECT CAST(NULL AS bigint) AS [ID], CAST(NULL AS nvarchar(64)) AS [名称], CAST(NULL AS decimal(10,2)) AS [价格], CAST(NULL AS int) AS [状态], CAST(NULL AS datetime) AS [更新时间] WHERE 1=0;
END
ELSE
BEGIN
    SELECT TOP 10
        [id] AS [ID],
        RTRIM([name]) AS [名称],
        [price] AS [价格],
        [status] AS [状态],
        [update_time] AS [更新时间]
    FROM dbo.[setmeal]
    ORDER BY ISNULL([update_time], '1900-01-01') DESC, [id] DESC;
END
", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        private DataTable QueryRecentEmployees()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[employee]', 'U') IS NULL
BEGIN
    SELECT CAST(NULL AS bigint) AS [ID], CAST(NULL AS nvarchar(32)) AS [姓名], CAST(NULL AS nvarchar(32)) AS [账号], CAST(NULL AS int) AS [状态], CAST(NULL AS datetime) AS [更新时间] WHERE 1=0;
END
ELSE
BEGIN
    SELECT TOP 10
        [id] AS [ID],
        RTRIM([name]) AS [姓名],
        RTRIM([username]) AS [账号],
        [status] AS [状态],
        [update_time] AS [更新时间]
    FROM dbo.[employee]
    ORDER BY ISNULL([update_time], '1900-01-01') DESC, [id] DESC;
END
", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
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
    }
}
