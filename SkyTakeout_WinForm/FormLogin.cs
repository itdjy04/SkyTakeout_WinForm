using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            AppIcon.Apply(this);
            Load += (s, e) => ApplyRoundedCorners();
            panelCard.SizeChanged += (s, e) => ApplyRoundedCorners();
            buttonLogin.SizeChanged += (s, e) => ApplyRoundedCorners();
            buttonLogin.Click += ButtonLogin_Click;
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxUsername.MaxLength = 10;
            textBoxPassword.MaxLength = 10;
            textBoxPassword.KeyDown += TextBoxPassword_KeyDown;
        }

        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
            ButtonLogin_Click(buttonLogin, EventArgs.Empty);
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string username = (textBoxUsername.Text ?? string.Empty).Trim();
            string password = (textBoxPassword.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("请输入用户名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("请输入密码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPassword.Focus();
                return;
            }

            int userId;
            string displayName;
            if (!TryLogin(username, password, out userId, out displayName))
            {
                MessageBox.Show("用户名或密码错误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.SelectAll();
                textBoxPassword.Focus();
                return;
            }

            FormMain main = new FormMain(userId, displayName);
            main.FormClosed += delegate
            {
                if (main.LogoutRequested)
                {
                    textBoxPassword.Text = string.Empty;
                    Show();
                    textBoxUsername.Focus();
                    return;
                }

                Close();
            };
            main.Show();
            Hide();
        }

        private bool TryLogin(string username, string password, out int userId, out string displayName)
        {
            userId = 0;
            displayName = string.Empty;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1 [id], RTRIM([username]) AS [username], RTRIM([password]) AS [password]
FROM dbo.[user]
WHERE RTRIM([username]) = @u
", conn))
            {
                cmd.Parameters.Add("@u", SqlDbType.NVarChar, 20).Value = username;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    int id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    string u = reader.IsDBNull(1) ? string.Empty : (reader.GetString(1) ?? string.Empty).Trim();
                    string p = reader.IsDBNull(2) ? string.Empty : (reader.GetString(2) ?? string.Empty).Trim();
                    if (!string.Equals(p, password, StringComparison.Ordinal))
                    {
                        return false;
                    }

                    userId = id;
                    displayName = u;
                    return userId > 0 && !string.IsNullOrWhiteSpace(displayName);
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

        private void ApplyRoundedCorners()
        {
            ApplyRoundedRegion(panelCard, 12);
            ApplyRoundedRegion(buttonLogin, Math.Max(1, buttonLogin.Height / 2));
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.FlatAppearance.BorderSize = 0;
        }

        private static void ApplyRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            int safeRadius = Math.Max(1, radius);
            int maxRadius = Math.Min(control.Width, control.Height) / 2;
            if (safeRadius > maxRadius)
            {
                safeRadius = maxRadius;
            }

            Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, safeRadius))
            {
                Region old = control.Region;
                control.Region = new Region(path);
                if (old != null)
                {
                    old.Dispose();
                }
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
