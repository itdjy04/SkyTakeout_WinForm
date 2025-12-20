using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class FormMain : Form
    {
        private readonly Dictionary<string, Control> pages = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        private Button currentMenuButton;

        public FormMain()
        {
            InitializeComponent();
            MinimumSize = new Size(1100, 700);
            Load += FormMain_Load;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            NavigateTo("工作台");
        }

        private void NavigateTo(string pageName)
        {
            Control page = GetOrCreatePage(pageName);
            panelContent.SuspendLayout();
            panelContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
            panelContent.ResumeLayout();
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

            Panel container = new Panel();
            container.BackColor = Color.Transparent;

            Label title = new Label();
            title.AutoSize = true;
            title.Text = pageName;
            title.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(36, 36, 36);
            title.Location = new Point(6, 6);

            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Location = new Point(6, 56);
            card.Size = new Size(420, 140);

            Label hint = new Label();
            hint.AutoSize = true;
            hint.Text = "这里放模块页面内容（UserControl/Panel）";
            hint.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular);
            hint.ForeColor = Color.FromArgb(96, 98, 102);
            hint.Location = new Point(16, 18);

            card.Controls.Add(hint);
            container.Controls.Add(title);
            container.Controls.Add(card);

            pages[pageName] = container;
            return container;
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
    }
}
