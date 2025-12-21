namespace SkyTakeout_WinForm
{
    partial class UcEmployeeManage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelTable = new System.Windows.Forms.Panel();
            this.dataGridViewEmployee = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colToggle = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panelPager = new System.Windows.Forms.Panel();
            this.comboBoxPageSize = new System.Windows.Forms.ComboBox();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.panelActions = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxEmployeeName = new System.Windows.Forms.TextBox();
            this.labelEmployeeName = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployee)).BeginInit();
            this.panelPager.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.White;
            this.panelRoot.Controls.Add(this.panelTable);
            this.panelRoot.Controls.Add(this.panelPager);
            this.panelRoot.Controls.Add(this.panelFilter);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Padding = new System.Windows.Forms.Padding(16);
            this.panelRoot.Size = new System.Drawing.Size(1191, 742);
            this.panelRoot.TabIndex = 0;
            // 
            // panelTable
            // 
            this.panelTable.Controls.Add(this.dataGridViewEmployee);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(16, 76);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(1159, 610);
            this.panelTable.TabIndex = 2;
            // 
            // dataGridViewEmployee
            // 
            this.dataGridViewEmployee.AllowUserToAddRows = false;
            this.dataGridViewEmployee.AllowUserToDeleteRows = false;
            this.dataGridViewEmployee.AllowUserToResizeRows = false;
            this.dataGridViewEmployee.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewEmployee.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewEmployee.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEmployee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEmployee.ColumnHeadersHeight = 44;
            this.dataGridViewEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colUsername,
            this.colPhone,
            this.colStatus,
            this.colUpdateTime,
            this.colEdit,
            this.colToggle});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmployee.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEmployee.EnableHeadersVisualStyles = false;
            this.dataGridViewEmployee.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.dataGridViewEmployee.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEmployee.MultiSelect = false;
            this.dataGridViewEmployee.Name = "dataGridViewEmployee";
            this.dataGridViewEmployee.ReadOnly = true;
            this.dataGridViewEmployee.RowHeadersVisible = false;
            this.dataGridViewEmployee.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dataGridViewEmployee.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewEmployee.RowTemplate.Height = 52;
            this.dataGridViewEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmployee.Size = new System.Drawing.Size(1159, 610);
            this.dataGridViewEmployee.TabIndex = 0;
            this.dataGridViewEmployee.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmployee_CellContentClick);
            this.dataGridViewEmployee.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewEmployee_CellFormatting);
            // 
            // colName
            // 
            this.colName.HeaderText = "员工姓名";
            this.colName.MinimumWidth = 120;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 160;
            // 
            // colUsername
            // 
            this.colUsername.HeaderText = "账号";
            this.colUsername.MinimumWidth = 120;
            this.colUsername.Name = "colUsername";
            this.colUsername.ReadOnly = true;
            this.colUsername.Width = 160;
            // 
            // colPhone
            // 
            this.colPhone.HeaderText = "手机号";
            this.colPhone.MinimumWidth = 120;
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            this.colPhone.Width = 160;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "账号状态";
            this.colStatus.MinimumWidth = 90;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 110;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.HeaderText = "最后操作时间";
            this.colUpdateTime.MinimumWidth = 160;
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.ReadOnly = true;
            this.colUpdateTime.Width = 180;
            // 
            // colEdit
            // 
            this.colEdit.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.colEdit.HeaderText = "操作";
            this.colEdit.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.colEdit.MinimumWidth = 70;
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colEdit.Text = "修改";
            this.colEdit.UseColumnTextForLinkValue = true;
            this.colEdit.Width = 70;
            // 
            // colToggle
            // 
            this.colToggle.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.colToggle.HeaderText = "";
            this.colToggle.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.colToggle.MinimumWidth = 70;
            this.colToggle.Name = "colToggle";
            this.colToggle.ReadOnly = true;
            this.colToggle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colToggle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colToggle.Width = 70;
            // 
            // panelPager
            // 
            this.panelPager.BackColor = System.Drawing.Color.White;
            this.panelPager.Controls.Add(this.comboBoxPageSize);
            this.panelPager.Controls.Add(this.labelPage);
            this.panelPager.Controls.Add(this.buttonNext);
            this.panelPager.Controls.Add(this.buttonPrev);
            this.panelPager.Controls.Add(this.labelTotal);
            this.panelPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPager.Location = new System.Drawing.Point(16, 686);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(1159, 40);
            this.panelPager.TabIndex = 1;
            // 
            // comboBoxPageSize
            // 
            this.comboBoxPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPageSize.FormattingEnabled = true;
            this.comboBoxPageSize.Items.AddRange(new object[] {
            "10条/页",
            "20条/页",
            "50条/页"});
            this.comboBoxPageSize.Location = new System.Drawing.Point(92, 8);
            this.comboBoxPageSize.Name = "comboBoxPageSize";
            this.comboBoxPageSize.Size = new System.Drawing.Size(92, 26);
            this.comboBoxPageSize.TabIndex = 1;
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(540, 12);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(35, 18);
            this.labelPage.TabIndex = 4;
            this.labelPage.Text = "1/1";
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(622, 7);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(54, 28);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonPrev
            // 
            this.buttonPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrev.Location = new System.Drawing.Point(564, 7);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(54, 28);
            this.buttonPrev.TabIndex = 2;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(0, 12);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(71, 18);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "共 0 条";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.panelActions);
            this.panelFilter.Controls.Add(this.buttonSearch);
            this.panelFilter.Controls.Add(this.textBoxEmployeeName);
            this.panelFilter.Controls.Add(this.labelEmployeeName);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(16, 16);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1159, 60);
            this.panelFilter.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.buttonAdd);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelActions.Location = new System.Drawing.Point(979, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(180, 60);
            this.panelActions.TabIndex = 8;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(28, 9);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(115, 40);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "+ 添加员工";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(340, 14);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(70, 32);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "查询";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxEmployeeName
            // 
            this.textBoxEmployeeName.Location = new System.Drawing.Point(92, 16);
            this.textBoxEmployeeName.Name = "textBoxEmployeeName";
            this.textBoxEmployeeName.Size = new System.Drawing.Size(240, 28);
            this.textBoxEmployeeName.TabIndex = 0;
            // 
            // labelEmployeeName
            // 
            this.labelEmployeeName.AutoSize = true;
            this.labelEmployeeName.Location = new System.Drawing.Point(0, 20);
            this.labelEmployeeName.Name = "labelEmployeeName";
            this.labelEmployeeName.Size = new System.Drawing.Size(89, 18);
            this.labelEmployeeName.TabIndex = 0;
            this.labelEmployeeName.Text = "员工姓名:";
            // 
            // UcEmployeeManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelRoot);
            this.Name = "UcEmployeeManage";
            this.Size = new System.Drawing.Size(1191, 742);
            this.panelRoot.ResumeLayout(false);
            this.panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployee)).EndInit();
            this.panelPager.ResumeLayout(false);
            this.panelPager.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.DataGridView dataGridViewEmployee;
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.ComboBox comboBoxPageSize;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxEmployeeName;
        private System.Windows.Forms.Label labelEmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdateTime;
        private System.Windows.Forms.DataGridViewLinkColumn colEdit;
        private System.Windows.Forms.DataGridViewLinkColumn colToggle;
    }
}

