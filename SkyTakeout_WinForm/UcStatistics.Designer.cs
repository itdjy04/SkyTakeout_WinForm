using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    partial class UcStatistics
    {
        private IContainer components = null;
        private Panel panelTopBar;
        private Label labelTitle;
        private Button buttonRefresh;
        private TableLayoutPanel tableStats;
        private GroupBox groupDish;
        private GroupBox groupSetmeal;
        private GroupBox groupCategory;
        private GroupBox groupEmployee;
        private Label labelDishValue;
        private Label labelSetmealValue;
        private Label labelCategoryValue;
        private Label labelEmployeeValue;
        private TabControl tabControlMain;
        private TabPage tabPageSetmeal;
        private TabPage tabPageEmployee;
        private DataGridView gridSetmeal;
        private DataGridView gridEmployee;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.tableStats = new System.Windows.Forms.TableLayoutPanel();
            this.groupDish = new System.Windows.Forms.GroupBox();
            this.labelDishValue = new System.Windows.Forms.Label();
            this.groupSetmeal = new System.Windows.Forms.GroupBox();
            this.labelSetmealValue = new System.Windows.Forms.Label();
            this.groupCategory = new System.Windows.Forms.GroupBox();
            this.labelCategoryValue = new System.Windows.Forms.Label();
            this.groupEmployee = new System.Windows.Forms.GroupBox();
            this.labelEmployeeValue = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSetmeal = new System.Windows.Forms.TabPage();
            this.gridSetmeal = new System.Windows.Forms.DataGridView();
            this.tabPageEmployee = new System.Windows.Forms.TabPage();
            this.gridEmployee = new System.Windows.Forms.DataGridView();
            this.panelTopBar.SuspendLayout();
            this.tableStats.SuspendLayout();
            this.groupDish.SuspendLayout();
            this.groupSetmeal.SuspendLayout();
            this.groupCategory.SuspendLayout();
            this.groupEmployee.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageSetmeal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetmeal)).BeginInit();
            this.tabPageEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.White;
            this.panelTopBar.Controls.Add(this.labelTitle);
            this.panelTopBar.Controls.Add(this.buttonRefresh);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(16, 16);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(1419, 44);
            this.panelTopBar.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(110, 31);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "数据统计";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRefresh.ForeColor = System.Drawing.Color.White;
            this.buttonRefresh.Location = new System.Drawing.Point(1333, 6);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(86, 32);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // tableStats
            // 
            this.tableStats.BackColor = System.Drawing.Color.White;
            this.tableStats.ColumnCount = 2;
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.Controls.Add(this.groupDish, 0, 0);
            this.tableStats.Controls.Add(this.groupSetmeal, 1, 0);
            this.tableStats.Controls.Add(this.groupCategory, 0, 1);
            this.tableStats.Controls.Add(this.groupEmployee, 1, 1);
            this.tableStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableStats.Location = new System.Drawing.Point(16, 60);
            this.tableStats.Name = "tableStats";
            this.tableStats.RowCount = 2;
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.Size = new System.Drawing.Size(1419, 220);
            this.tableStats.TabIndex = 1;
            // 
            // groupDish
            // 
            this.groupDish.Controls.Add(this.labelDishValue);
            this.groupDish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDish.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupDish.Location = new System.Drawing.Point(3, 3);
            this.groupDish.Name = "groupDish";
            this.groupDish.Size = new System.Drawing.Size(703, 104);
            this.groupDish.TabIndex = 0;
            this.groupDish.TabStop = false;
            this.groupDish.Text = "菜品";
            // 
            // labelDishValue
            // 
            this.labelDishValue.AutoSize = true;
            this.labelDishValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDishValue.Location = new System.Drawing.Point(16, 34);
            this.labelDishValue.Name = "labelDishValue";
            this.labelDishValue.Size = new System.Drawing.Size(84, 27);
            this.labelDishValue.TabIndex = 0;
            this.labelDishValue.Text = "总数：0";
            // 
            // groupSetmeal
            // 
            this.groupSetmeal.Controls.Add(this.labelSetmealValue);
            this.groupSetmeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSetmeal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupSetmeal.Location = new System.Drawing.Point(712, 3);
            this.groupSetmeal.Name = "groupSetmeal";
            this.groupSetmeal.Size = new System.Drawing.Size(704, 104);
            this.groupSetmeal.TabIndex = 1;
            this.groupSetmeal.TabStop = false;
            this.groupSetmeal.Text = "套餐";
            // 
            // labelSetmealValue
            // 
            this.labelSetmealValue.AutoSize = true;
            this.labelSetmealValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSetmealValue.Location = new System.Drawing.Point(16, 34);
            this.labelSetmealValue.Name = "labelSetmealValue";
            this.labelSetmealValue.Size = new System.Drawing.Size(84, 27);
            this.labelSetmealValue.TabIndex = 0;
            this.labelSetmealValue.Text = "总数：0";
            // 
            // groupCategory
            // 
            this.groupCategory.Controls.Add(this.labelCategoryValue);
            this.groupCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupCategory.Location = new System.Drawing.Point(3, 113);
            this.groupCategory.Name = "groupCategory";
            this.groupCategory.Size = new System.Drawing.Size(703, 104);
            this.groupCategory.TabIndex = 2;
            this.groupCategory.TabStop = false;
            this.groupCategory.Text = "分类";
            // 
            // labelCategoryValue
            // 
            this.labelCategoryValue.AutoSize = true;
            this.labelCategoryValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCategoryValue.Location = new System.Drawing.Point(16, 34);
            this.labelCategoryValue.Name = "labelCategoryValue";
            this.labelCategoryValue.Size = new System.Drawing.Size(124, 27);
            this.labelCategoryValue.TabIndex = 0;
            this.labelCategoryValue.Text = "菜品分类：0";
            // 
            // groupEmployee
            // 
            this.groupEmployee.Controls.Add(this.labelEmployeeValue);
            this.groupEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEmployee.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupEmployee.Location = new System.Drawing.Point(712, 113);
            this.groupEmployee.Name = "groupEmployee";
            this.groupEmployee.Size = new System.Drawing.Size(704, 104);
            this.groupEmployee.TabIndex = 3;
            this.groupEmployee.TabStop = false;
            this.groupEmployee.Text = "员工";
            // 
            // labelEmployeeValue
            // 
            this.labelEmployeeValue.AutoSize = true;
            this.labelEmployeeValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEmployeeValue.Location = new System.Drawing.Point(16, 34);
            this.labelEmployeeValue.Name = "labelEmployeeValue";
            this.labelEmployeeValue.Size = new System.Drawing.Size(84, 27);
            this.labelEmployeeValue.TabIndex = 0;
            this.labelEmployeeValue.Text = "总数：0";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageSetmeal);
            this.tabControlMain.Controls.Add(this.tabPageEmployee);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(16, 280);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1419, 1038);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageSetmeal
            // 
            this.tabPageSetmeal.Controls.Add(this.gridSetmeal);
            this.tabPageSetmeal.Location = new System.Drawing.Point(4, 28);
            this.tabPageSetmeal.Name = "tabPageSetmeal";
            this.tabPageSetmeal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetmeal.Size = new System.Drawing.Size(1411, 1006);
            this.tabPageSetmeal.TabIndex = 0;
            this.tabPageSetmeal.Text = "最近更新套餐";
            this.tabPageSetmeal.UseVisualStyleBackColor = true;
            // 
            // gridSetmeal
            // 
            this.gridSetmeal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSetmeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSetmeal.Location = new System.Drawing.Point(3, 3);
            this.gridSetmeal.Name = "gridSetmeal";
            this.gridSetmeal.RowHeadersWidth = 62;
            this.gridSetmeal.RowTemplate.Height = 27;
            this.gridSetmeal.Size = new System.Drawing.Size(1405, 1000);
            this.gridSetmeal.TabIndex = 0;
            // 
            // tabPageEmployee
            // 
            this.tabPageEmployee.Controls.Add(this.gridEmployee);
            this.tabPageEmployee.Location = new System.Drawing.Point(4, 28);
            this.tabPageEmployee.Name = "tabPageEmployee";
            this.tabPageEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEmployee.Size = new System.Drawing.Size(1060, 372);
            this.tabPageEmployee.TabIndex = 1;
            this.tabPageEmployee.Text = "最近更新员工";
            this.tabPageEmployee.UseVisualStyleBackColor = true;
            // 
            // gridEmployee
            // 
            this.gridEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmployee.Location = new System.Drawing.Point(3, 3);
            this.gridEmployee.Name = "gridEmployee";
            this.gridEmployee.RowHeadersWidth = 62;
            this.gridEmployee.RowTemplate.Height = 27;
            this.gridEmployee.Size = new System.Drawing.Size(1054, 366);
            this.gridEmployee.TabIndex = 0;
            // 
            // UcStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.tableStats);
            this.Controls.Add(this.panelTopBar);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "UcStatistics";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(1451, 1334);
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.tableStats.ResumeLayout(false);
            this.groupDish.ResumeLayout(false);
            this.groupDish.PerformLayout();
            this.groupSetmeal.ResumeLayout(false);
            this.groupSetmeal.PerformLayout();
            this.groupCategory.ResumeLayout(false);
            this.groupCategory.PerformLayout();
            this.groupEmployee.ResumeLayout(false);
            this.groupEmployee.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSetmeal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSetmeal)).EndInit();
            this.tabPageEmployee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

