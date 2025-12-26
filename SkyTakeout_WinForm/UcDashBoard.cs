using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcDashboard : System.Windows.Forms.UserControl
    {
        private bool hasLoaded;

        public UcDashboard()
        {
            InitializeComponent();
            Load += UcDashboard_Load;
            VisibleChanged += UcDashboard_VisibleChanged;
        }

        private void UcDashboard_Load(object sender, EventArgs e)
        {
            WireDashboardNavigation();
            RefreshDishAndSetmealOverview();
            hasLoaded = true;
        }

        private void UcDashboard_VisibleChanged(object sender, EventArgs e)
        {
            if (!hasLoaded || !Visible)
            {
                return;
            }

            RefreshDishAndSetmealOverview();
        }

        private void WireDashboardNavigation()
        {
            label菜品管理.Cursor = Cursors.Hand;
            pictureBox菜品管理.Cursor = Cursors.Hand;
            label套餐管理.Cursor = Cursors.Hand;
            pictureBox套餐管理.Cursor = Cursors.Hand;
            label新增菜品.Cursor = Cursors.Hand;
            pictureBox新增菜品.Cursor = Cursors.Hand;
            panel菜品总览_新增菜品.Cursor = Cursors.Hand;
            label新增套餐.Cursor = Cursors.Hand;
            pictureBox新增套餐.Cursor = Cursors.Hand;
            panel套餐总览_新增套餐.Cursor = Cursors.Hand;

            label菜品管理.Click += (s, e) => NavigateTo("菜品管理");
            pictureBox菜品管理.Click += (s, e) => NavigateTo("菜品管理");
            label套餐管理.Click += (s, e) => NavigateTo("套餐管理");
            pictureBox套餐管理.Click += (s, e) => NavigateTo("套餐管理");
            label新增菜品.Click += (s, e) => NavigateTo("新增菜品");
            pictureBox新增菜品.Click += (s, e) => NavigateTo("新增菜品");
            panel菜品总览_新增菜品.Click += (s, e) => NavigateTo("新增菜品");
            label新增套餐.Click += (s, e) => NavigateTo("新增套餐");
            pictureBox新增套餐.Click += (s, e) => NavigateTo("新增套餐");
            panel套餐总览_新增套餐.Click += (s, e) => NavigateTo("新增套餐");
        }

        private void NavigateTo(string pageName)
        {
            FormMain form = FindForm() as FormMain;
            if (form == null)
            {
                return;
            }

            form.NavigateToFromDashboard(pageName);
        }

        private void RefreshDishAndSetmealOverview()
        {
            try
            {
                (int dishOn, int dishOff) = QueryDishCounts();
                (int setmealOn, int setmealOff) = QuerySetmealCounts();

                label菜品总览_已起售Number.Text = dishOn.ToString();
                label菜品总览_已停售Number.Text = dishOff.ToString();
                label套餐总览_已启售Number.Text = setmealOn.ToString();
                label套餐总览_已停售Number.Text = setmealOff.ToString();
            }
            catch
            {
                label菜品总览_已起售Number.Text = "0";
                label菜品总览_已停售Number.Text = "0";
                label套餐总览_已启售Number.Text = "0";
                label套餐总览_已停售Number.Text = "0";
            }
        }

        private (int onSale, int offSale) QueryDishCounts()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[dish]', 'U') IS NULL
BEGIN
    SELECT CAST(0 AS int) AS [onSale], CAST(0 AS int) AS [offSale];
END
ELSE
BEGIN
    SELECT
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
                        return (0, 0);
                    }

                    int onSale = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0));
                    int offSale = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    return (onSale, offSale);
                }
            }
        }

        private (int onSale, int offSale) QuerySetmealCounts()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
IF OBJECT_ID('dbo.[setmeal]', 'U') IS NULL
BEGIN
    SELECT CAST(0 AS int) AS [onSale], CAST(0 AS int) AS [offSale];
END
ELSE
BEGIN
    SELECT
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
                        return (0, 0);
                    }

                    int onSale = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0));
                    int offSale = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                    return (onSale, offSale);
                }
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
