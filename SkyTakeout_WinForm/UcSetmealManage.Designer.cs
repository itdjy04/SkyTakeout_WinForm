namespace SkyTakeout_WinForm
{
    partial class UcSetmealManage
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
            this.dataGridViewSetmeal = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewLinkColumn();
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
            this.buttonBatchDelete = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.textBoxSetmealName = new System.Windows.Forms.TextBox();
            this.labelSetmealName = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSetmeal)).BeginInit();
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
            this.panelTable.Controls.Add(this.dataGridViewSetmeal);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(16, 76);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(1159, 610);
            this.panelTable.TabIndex = 2;
            // 
            // dataGridViewSetmeal
            // 
            this.dataGridViewSetmeal.AllowUserToAddRows = false;
            this.dataGridViewSetmeal.AllowUserToDeleteRows = false;
            this.dataGridViewSetmeal.AllowUserToResizeRows = false;
            this.dataGridViewSetmeal.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSetmeal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSetmeal.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewSetmeal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSetmeal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSetmeal.ColumnHeadersHeight = 44;
            this.dataGridViewSetmeal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewSetmeal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colName,
            this.colImage,
            this.colCategory,
            this.colPrice,
            this.colStatus,
            this.colUpdateTime,
            this.colEdit,
            this.colDelete,
            this.colToggle});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSetmeal.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSetmeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSetmeal.EnableHeadersVisualStyles = false;
            this.dataGridViewSetmeal.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.dataGridViewSetmeal.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSetmeal.MultiSelect = false;
            this.dataGridViewSetmeal.Name = "dataGridViewSetmeal";
            this.dataGridViewSetmeal.RowHeadersVisible = false;
            this.dataGridViewSetmeal.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dataGridViewSetmeal.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewSetmeal.RowTemplate.Height = 52;
            this.dataGridViewSetmeal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSetmeal.Size = new System.Drawing.Size(1159, 610);
            this.dataGridViewSetmeal.TabIndex = 0;
            this.dataGridViewSetmeal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSetmeal_CellContentClick);
            this.dataGridViewSetmeal.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewSetmeal_CellFormatting);
            // 
            // colSelect
            // 
            this.colSelect.FillWeight = 36F;
            this.colSelect.HeaderText = "";
            this.colSelect.MinimumWidth = 36;
            this.colSelect.Name = "colSelect";
            this.colSelect.Width = 36;
            // 
            // colName
            // 
            this.colName.FillWeight = 140F;
            this.colName.HeaderText = "套餐名称";
            this.colName.MinimumWidth = 120;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 140;
            // 
            // colImage
            // 
            this.colImage.FillWeight = 70F;
            this.colImage.HeaderText = "图片";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.MinimumWidth = 60;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            this.colImage.Width = 70;
            // 
            // colCategory
            // 
            this.colCategory.FillWeight = 120F;
            this.colCategory.HeaderText = "套餐分类";
            this.colCategory.MinimumWidth = 100;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 120;
            // 
            // colPrice
            // 
            this.colPrice.FillWeight = 80F;
            this.colPrice.HeaderText = "套餐价格";
            this.colPrice.MinimumWidth = 70;
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 80F;
            this.colStatus.HeaderText = "售卖状态";
            this.colStatus.MinimumWidth = 80;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 150;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.FillWeight = 140F;
            this.colUpdateTime.HeaderText = "最后操作时间";
            this.colUpdateTime.MinimumWidth = 140;
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.ReadOnly = true;
            this.colUpdateTime.Width = 160;
            // 
            // colEdit
            // 
            this.colEdit.FillWeight = 55F;
            this.colEdit.HeaderText = "操作";
            this.colEdit.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.colEdit.MinimumWidth = 50;
            this.colEdit.Name = "colEdit";
            this.colEdit.Text = "修改";
            this.colEdit.UseColumnTextForLinkValue = true;
            this.colEdit.Width = 55;
            // 
            // colDelete
            // 
            this.colDelete.FillWeight = 55F;
            this.colDelete.HeaderText = "";
            this.colDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.colDelete.MinimumWidth = 50;
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "删除";
            this.colDelete.UseColumnTextForLinkValue = true;
            this.colDelete.Width = 55;
            // 
            // colToggle
            // 
            this.colToggle.FillWeight = 70F;
            this.colToggle.HeaderText = "";
            this.colToggle.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.colToggle.MinimumWidth = 60;
            this.colToggle.Name = "colToggle";
            this.colToggle.Text = "停售";
            this.colToggle.UseColumnTextForLinkValue = true;
            this.colToggle.Width = 70;
            // 
            // panelPager
            // 
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
            this.comboBoxPageSize.Location = new System.Drawing.Point(94, 7);
            this.comboBoxPageSize.Name = "comboBoxPageSize";
            this.comboBoxPageSize.Size = new System.Drawing.Size(96, 26);
            this.comboBoxPageSize.TabIndex = 1;
            // 
            // labelPage
            // 
            this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage.AutoSize = true;
            this.labelPage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelPage.Location = new System.Drawing.Point(1065, 9);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(40, 24);
            this.labelPage.TabIndex = 4;
            this.labelPage.Text = "1/1";
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.FlatAppearance.BorderSize = 0;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.buttonNext.Location = new System.Drawing.Point(1119, 4);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(36, 30);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonPrev
            // 
            this.buttonPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrev.FlatAppearance.BorderSize = 0;
            this.buttonPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrev.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.buttonPrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.buttonPrev.Location = new System.Drawing.Point(1023, 4);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(36, 30);
            this.buttonPrev.TabIndex = 2;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelTotal.Location = new System.Drawing.Point(0, 9);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(67, 24);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "共 0 条";
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.panelActions);
            this.panelFilter.Controls.Add(this.buttonSearch);
            this.panelFilter.Controls.Add(this.comboBoxStatus);
            this.panelFilter.Controls.Add(this.labelStatus);
            this.panelFilter.Controls.Add(this.comboBoxCategory);
            this.panelFilter.Controls.Add(this.labelCategory);
            this.panelFilter.Controls.Add(this.textBoxSetmealName);
            this.panelFilter.Controls.Add(this.labelSetmealName);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(16, 16);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1159, 60);
            this.panelFilter.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.buttonAdd);
            this.panelActions.Controls.Add(this.buttonBatchDelete);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelActions.Location = new System.Drawing.Point(883, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(276, 60);
            this.panelActions.TabIndex = 7;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(152, 8);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 40);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "新增套餐";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonBatchDelete
            // 
            this.buttonBatchDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBatchDelete.BackColor = System.Drawing.Color.White;
            this.buttonBatchDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonBatchDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBatchDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.buttonBatchDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.buttonBatchDelete.Location = new System.Drawing.Point(46, 8);
            this.buttonBatchDelete.Name = "buttonBatchDelete";
            this.buttonBatchDelete.Size = new System.Drawing.Size(100, 40);
            this.buttonBatchDelete.TabIndex = 0;
            this.buttonBatchDelete.Text = "批量删除";
            this.buttonBatchDelete.UseVisualStyleBackColor = false;
            this.buttonBatchDelete.Click += new System.EventHandler(this.buttonBatchDelete_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Location = new System.Drawing.Point(606, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(80, 36);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "查询";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "全部",
            "启售",
            "停售"});
            this.comboBoxStatus.Location = new System.Drawing.Point(477, 19);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(108, 26);
            this.comboBoxStatus.TabIndex = 5;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelStatus.Location = new System.Drawing.Point(387, 19);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(86, 24);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "售卖状态:";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] {
            "全部"});
            this.comboBoxCategory.Location = new System.Drawing.Point(283, 17);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(108, 26);
            this.comboBoxCategory.TabIndex = 3;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelCategory.Location = new System.Drawing.Point(201, 19);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(86, 24);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "套餐分类:";
            // 
            // textBoxSetmealName
            // 
            this.textBoxSetmealName.Location = new System.Drawing.Point(87, 17);
            this.textBoxSetmealName.Name = "textBoxSetmealName";
            this.textBoxSetmealName.Size = new System.Drawing.Size(108, 28);
            this.textBoxSetmealName.TabIndex = 1;
            // 
            // labelSetmealName
            // 
            this.labelSetmealName.AutoSize = true;
            this.labelSetmealName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelSetmealName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelSetmealName.Location = new System.Drawing.Point(0, 19);
            this.labelSetmealName.Name = "labelSetmealName";
            this.labelSetmealName.Size = new System.Drawing.Size(86, 24);
            this.labelSetmealName.TabIndex = 0;
            this.labelSetmealName.Text = "套餐名称:";
            // 
            // UcSetmealManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelRoot);
            this.Name = "UcSetmealManage";
            this.Size = new System.Drawing.Size(1191, 742);
            this.panelRoot.ResumeLayout(false);
            this.panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSetmeal)).EndInit();
            this.panelPager.ResumeLayout(false);
            this.panelPager.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label labelSetmealName;
        private System.Windows.Forms.TextBox textBoxSetmealName;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button buttonBatchDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.ComboBox comboBoxPageSize;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.DataGridView dataGridViewSetmeal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdateTime;
        private System.Windows.Forms.DataGridViewLinkColumn colEdit;
        private System.Windows.Forms.DataGridViewLinkColumn colDelete;
        private System.Windows.Forms.DataGridViewLinkColumn colToggle;
    }
}
