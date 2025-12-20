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
    }
}
