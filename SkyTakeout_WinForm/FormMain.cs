using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class FormMain : Form
    {
        public static event Action CategoryChanged;

        private readonly Dictionary<string, Control> pages = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        private Button currentMenuButton;
        private readonly int currentUserId;
        private readonly string currentUsername;
        private readonly ContextMenuStrip userMenu = new ContextMenuStrip();

        public bool LogoutRequested { get; private set; }

        public FormMain(int userId, string username)
        {
            InitializeComponent();
            MinimumSize = new Size(1100, 700);
            Load += FormMain_Load;
            currentUserId = userId;
            currentUsername = (username ?? string.Empty).Trim();
            InitUserMenu();
        }

        public static void NotifyCategoryChanged()
        {
            CategoryChanged?.Invoke();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            labelUser.Text = string.IsNullOrWhiteSpace(currentUsername) ? "管理员 ▾" : currentUsername + " ▾";
            labelUser.Cursor = Cursors.Hand;
            labelUser.Click += labelUser_Click;
            NavigateTo("工作台");
        }

        private void labelUser_Click(object sender, EventArgs e)
        {
            Point p = labelUser.PointToScreen(new Point(0, labelUser.Height));
            userMenu.Show(p);
        }

        private void InitUserMenu()
        {
            userMenu.ShowImageMargin = true;
            userMenu.RenderMode = ToolStripRenderMode.System;

            ToolStripMenuItem itemChangePwd = new ToolStripMenuItem("修改密码");
            itemChangePwd.Image = Properties.Resources.密码;
            itemChangePwd.Click += (s, e) => ShowChangePasswordDialog();

            ToolStripMenuItem itemLogout = new ToolStripMenuItem("退出登录");
            itemLogout.Image = Properties.Resources.错;
            itemLogout.Click += (s, e) => DoLogout();

            userMenu.Items.Clear();
            userMenu.Items.Add(itemChangePwd);
            userMenu.Items.Add(new ToolStripSeparator());
            userMenu.Items.Add(itemLogout);
        }

        private void DoLogout()
        {
            DialogResult confirm = MessageBox.Show("确定要退出登录吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            LogoutRequested = true;
            Close();
        }

        private void ShowChangePasswordDialog()
        {
            while (true)
            {
                using (ChangePasswordForm form = new ChangePasswordForm())
                {
                    if (form.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    bool ok = TryChangePassword(currentUserId, form.OldPassword, form.NewPassword);
                    if (!ok)
                    {
                        MessageBox.Show("原密码不正确，修改失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    MessageBox.Show("密码修改成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private bool TryChangePassword(int userId, string oldPwd, string newPwd)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.[user]
SET [password] = @newPwd,
    [update_time] = GETDATE()
WHERE [id] = @id AND RTRIM([password]) = @oldPwd
", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@oldPwd", SqlDbType.NVarChar, 20).Value = oldPwd ?? string.Empty;
                cmd.Parameters.Add("@newPwd", SqlDbType.NVarChar, 20).Value = newPwd ?? string.Empty;
                conn.Open();
                int affected = cmd.ExecuteNonQuery();
                return affected > 0;
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

        private void NavigateTo(string pageName)
        {
            labelContentTitle.Text = pageName;

            panelContentPlaceholderCard.SuspendLayout();
            panelContentPlaceholderCard.Controls.Clear();

            Control page = GetOrCreatePage(pageName);
            if (page != null)
            {
                page.Dock = DockStyle.Fill;
                panelContentPlaceholderCard.Controls.Add(page);
            }
            else
            {
                labelContentPlaceholder.Text = "此模块待实现：" + pageName;
                labelContentPlaceholder.Location = new Point(16, 18);
                panelContentPlaceholderCard.Controls.Add(labelContentPlaceholder);
            }

            panelContentPlaceholderCard.ResumeLayout();
            HighlightMenuButton(pageName);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            NavigateTo(btn.Text);
        }

        private void buttonBusinessStatus_Click(object sender, EventArgs e)
        {
            buttonBusinessStatus.Text = buttonBusinessStatus.Text == "营业中" ? "已打烊" : "营业中";
        }

        private Control GetOrCreatePage(string pageName)
        {
            Control existing;
            if (pages.TryGetValue(pageName, out existing))
            {
                return existing;
            }

            Control created = null;

            if (string.Equals(pageName, "工作台", StringComparison.OrdinalIgnoreCase))
            {
                created = new UcDashboard();
            }
            else if (string.Equals(pageName, "菜品管理", StringComparison.OrdinalIgnoreCase))
            {
                created = new UcDishManage();
            }
            else if (string.Equals(pageName, "分类管理", StringComparison.OrdinalIgnoreCase))
            {
                created = new UcCategoryManage(currentUserId);
            }
            else if (string.Equals(pageName, "套餐管理", StringComparison.OrdinalIgnoreCase))
            {
                created = new UcSetmealManage(currentUserId);
            }
            else if (string.Equals(pageName, "员工管理", StringComparison.OrdinalIgnoreCase))
            {
                created = new UcEmployeeManage(currentUserId);
            }
            
            pages[pageName] = created;
            return created;
        }

        private void HighlightMenuButton(string pageName)
        {
            Button target = null;
            for (int i = 0; i < panelSide.Controls.Count; i++)
            {
                Button b = panelSide.Controls[i] as Button;
                if (b != null && b.Text == pageName)
                {
                    target = b;
                    break;
                }
            }

            if (target == null)
            {
                return;
            }

            if (currentMenuButton != null)
            {
                currentMenuButton.BackColor = Color.Transparent;
                currentMenuButton.ForeColor = Color.FromArgb(220, 223, 230);
            }

            currentMenuButton = target;
            currentMenuButton.BackColor = Color.FromArgb(55, 61, 70);
            currentMenuButton.ForeColor = Color.White;

            panelSideIndicator.Visible = true;
            panelSideIndicator.Location = new Point(0, currentMenuButton.Top);
            panelSideIndicator.Height = currentMenuButton.Height;
            panelSideIndicator.BringToFront();
        }

        private sealed class ChangePasswordForm : Form
        {
            private readonly TextBox textBoxOld = new TextBox();
            private readonly TextBox textBoxNew = new TextBox();
            private readonly TextBox textBoxConfirm = new TextBox();
            private readonly Button buttonOk = new Button();
            private readonly Button buttonCancel = new Button();

            public string OldPassword => (textBoxOld.Text ?? string.Empty).Trim();
            public string NewPassword => (textBoxNew.Text ?? string.Empty).Trim();
            public string ConfirmPassword => (textBoxConfirm.Text ?? string.Empty).Trim();

            public ChangePasswordForm()
            {
                Text = "修改密码";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                ClientSize = new Size(420, 220);

                Label labelOld = new Label { Text = "原密码", AutoSize = true, Location = new Point(24, 28) };
                Label labelNew = new Label { Text = "新密码", AutoSize = true, Location = new Point(24, 74) };
                Label labelConfirm = new Label { Text = "确认新密码", AutoSize = true, Location = new Point(24, 120) };

                textBoxOld.Location = new Point(140, 24);
                textBoxOld.Size = new Size(250, 28);
                textBoxOld.UseSystemPasswordChar = true;
                textBoxOld.MaxLength = 10;

                textBoxNew.Location = new Point(140, 70);
                textBoxNew.Size = new Size(250, 28);
                textBoxNew.UseSystemPasswordChar = true;
                textBoxNew.MaxLength = 10;

                textBoxConfirm.Location = new Point(140, 116);
                textBoxConfirm.Size = new Size(250, 28);
                textBoxConfirm.UseSystemPasswordChar = true;
                textBoxConfirm.MaxLength = 10;

                buttonOk.Text = "保存";
                buttonOk.Location = new Point(230, 168);
                buttonOk.Size = new Size(76, 32);
                buttonOk.BackColor = Color.FromArgb(255, 184, 0);
                buttonOk.FlatStyle = FlatStyle.Flat;
                buttonOk.FlatAppearance.BorderSize = 0;
                buttonOk.ForeColor = Color.White;
                buttonOk.Click += buttonOk_Click;

                buttonCancel.Text = "取消";
                buttonCancel.Location = new Point(314, 168);
                buttonCancel.Size = new Size(76, 32);
                buttonCancel.DialogResult = DialogResult.Cancel;

                Controls.Add(labelOld);
                Controls.Add(textBoxOld);
                Controls.Add(labelNew);
                Controls.Add(textBoxNew);
                Controls.Add(labelConfirm);
                Controls.Add(textBoxConfirm);
                Controls.Add(buttonOk);
                Controls.Add(buttonCancel);

                AcceptButton = buttonOk;
                CancelButton = buttonCancel;
            }

            private void buttonOk_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(OldPassword))
                {
                    MessageBox.Show("请输入原密码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxOld.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(NewPassword))
                {
                    MessageBox.Show("请输入新密码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxNew.Focus();
                    return;
                }

                if (NewPassword.Length > 10)
                {
                    MessageBox.Show("新密码长度不能超过 10 位。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxNew.Focus();
                    return;
                }

                if (!string.Equals(NewPassword, ConfirmPassword, StringComparison.Ordinal))
                {
                    MessageBox.Show("两次输入的新密码不一致。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxConfirm.Focus();
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
