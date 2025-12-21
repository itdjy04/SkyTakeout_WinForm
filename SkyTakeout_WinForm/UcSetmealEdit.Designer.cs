namespace SkyTakeout_WinForm
{
    partial class UcSetmealEdit
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
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonSaveContinue = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelForm = new System.Windows.Forms.Panel();
            this.groupBoxImage = new System.Windows.Forms.GroupBox();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.textBoxImagePath = new System.Windows.Forms.TextBox();
            this.labelImage = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.panelDishes = new System.Windows.Forms.Panel();
            this.buttonRemoveDish = new System.Windows.Forms.Button();
            this.buttonAddDish = new System.Windows.Forms.Button();
            this.dataGridViewDishes = new System.Windows.Forms.DataGridView();
            this.labelDishes = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.textBoxSetmealName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelPageTitle = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.groupBoxImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panelDishes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDishes)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.White;
            this.panelRoot.Controls.Add(this.panelContent);
            this.panelRoot.Controls.Add(this.panelHeader);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Padding = new System.Windows.Forms.Padding(16);
            this.panelRoot.Size = new System.Drawing.Size(1203, 785);
            this.panelRoot.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelButtons);
            this.panelContent.Controls.Add(this.panelForm);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(16, 64);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1171, 705);
            this.panelContent.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonSaveContinue);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 653);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1171, 52);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonSaveContinue
            // 
            this.buttonSaveContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonSaveContinue.FlatAppearance.BorderSize = 0;
            this.buttonSaveContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveContinue.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSaveContinue.ForeColor = System.Drawing.Color.White;
            this.buttonSaveContinue.Location = new System.Drawing.Point(1022, 8);
            this.buttonSaveContinue.Name = "buttonSaveContinue";
            this.buttonSaveContinue.Size = new System.Drawing.Size(140, 36);
            this.buttonSaveContinue.TabIndex = 2;
            this.buttonSaveContinue.Text = "保存并继续添加";
            this.buttonSaveContinue.UseVisualStyleBackColor = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(936, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 36);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.buttonCancel.Location = new System.Drawing.Point(850, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 36);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // panelForm
            // 
            this.panelForm.AutoScroll = true;
            this.panelForm.Controls.Add(this.groupBoxImage);
            this.panelForm.Controls.Add(this.textBoxDescription);
            this.panelForm.Controls.Add(this.labelDescription);
            this.panelForm.Controls.Add(this.panelDishes);
            this.panelForm.Controls.Add(this.labelDishes);
            this.panelForm.Controls.Add(this.textBoxPrice);
            this.panelForm.Controls.Add(this.labelPrice);
            this.panelForm.Controls.Add(this.comboBoxStatus);
            this.panelForm.Controls.Add(this.labelStatus);
            this.panelForm.Controls.Add(this.comboBoxCategory);
            this.panelForm.Controls.Add(this.labelCategory);
            this.panelForm.Controls.Add(this.textBoxSetmealName);
            this.panelForm.Controls.Add(this.labelName);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(8);
            this.panelForm.Size = new System.Drawing.Size(1171, 705);
            this.panelForm.TabIndex = 0;
            // 
            // groupBoxImage
            // 
            this.groupBoxImage.Controls.Add(this.pictureBoxPreview);
            this.groupBoxImage.Controls.Add(this.buttonUpload);
            this.groupBoxImage.Controls.Add(this.textBoxImagePath);
            this.groupBoxImage.Controls.Add(this.labelImage);
            this.groupBoxImage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxImage.Location = new System.Drawing.Point(16, 341);
            this.groupBoxImage.Name = "groupBoxImage";
            this.groupBoxImage.Size = new System.Drawing.Size(1128, 130);
            this.groupBoxImage.TabIndex = 12;
            this.groupBoxImage.TabStop = false;
            this.groupBoxImage.Text = "套餐图片";
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.pictureBoxPreview.Location = new System.Drawing.Point(16, 30);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(120, 80);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 3;
            this.pictureBoxPreview.TabStop = false;
            // 
            // buttonUpload
            // 
            this.buttonUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonUpload.FlatAppearance.BorderSize = 0;
            this.buttonUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpload.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonUpload.ForeColor = System.Drawing.Color.White;
            this.buttonUpload.Location = new System.Drawing.Point(1016, 54);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(96, 34);
            this.buttonUpload.TabIndex = 2;
            this.buttonUpload.Text = "上传图片";
            this.buttonUpload.UseVisualStyleBackColor = false;
            // 
            // textBoxImagePath
            // 
            this.textBoxImagePath.Location = new System.Drawing.Point(222, 58);
            this.textBoxImagePath.Name = "textBoxImagePath";
            this.textBoxImagePath.Size = new System.Drawing.Size(780, 30);
            this.textBoxImagePath.TabIndex = 1;
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelImage.Location = new System.Drawing.Point(156, 62);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(64, 24);
            this.labelImage.TabIndex = 0;
            this.labelImage.Text = "路径：";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(104, 503);
            this.textBoxDescription.MaxLength = 255;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(1036, 70);
            this.textBoxDescription.TabIndex = 11;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelDescription.Location = new System.Drawing.Point(12, 507);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(64, 24);
            this.labelDescription.TabIndex = 10;
            this.labelDescription.Text = "描述：";
            // 
            // panelDishes
            // 
            this.panelDishes.Controls.Add(this.buttonRemoveDish);
            this.panelDishes.Controls.Add(this.buttonAddDish);
            this.panelDishes.Controls.Add(this.dataGridViewDishes);
            this.panelDishes.Location = new System.Drawing.Point(104, 114);
            this.panelDishes.Name = "panelDishes";
            this.panelDishes.Size = new System.Drawing.Size(1036, 221);
            this.panelDishes.TabIndex = 9;
            // 
            // buttonRemoveDish
            // 
            this.buttonRemoveDish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveDish.BackColor = System.Drawing.Color.White;
            this.buttonRemoveDish.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonRemoveDish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemoveDish.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.buttonRemoveDish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.buttonRemoveDish.Location = new System.Drawing.Point(124, 0);
            this.buttonRemoveDish.Name = "buttonRemoveDish";
            this.buttonRemoveDish.Size = new System.Drawing.Size(104, 37);
            this.buttonRemoveDish.TabIndex = 2;
            this.buttonRemoveDish.Text = "移除菜品";
            this.buttonRemoveDish.UseVisualStyleBackColor = false;
            // 
            // buttonAddDish
            // 
            this.buttonAddDish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonAddDish.FlatAppearance.BorderSize = 0;
            this.buttonAddDish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddDish.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAddDish.ForeColor = System.Drawing.Color.White;
            this.buttonAddDish.Location = new System.Drawing.Point(0, 0);
            this.buttonAddDish.Name = "buttonAddDish";
            this.buttonAddDish.Size = new System.Drawing.Size(118, 37);
            this.buttonAddDish.TabIndex = 1;
            this.buttonAddDish.Text = "+ 添加菜品";
            this.buttonAddDish.UseVisualStyleBackColor = false;
            // 
            // dataGridViewDishes
            // 
            this.dataGridViewDishes.AllowUserToAddRows = false;
            this.dataGridViewDishes.AllowUserToDeleteRows = false;
            this.dataGridViewDishes.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDishes.ColumnHeadersHeight = 32;
            this.dataGridViewDishes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewDishes.Location = new System.Drawing.Point(0, 43);
            this.dataGridViewDishes.MultiSelect = false;
            this.dataGridViewDishes.Name = "dataGridViewDishes";
            this.dataGridViewDishes.ReadOnly = true;
            this.dataGridViewDishes.RowHeadersVisible = false;
            this.dataGridViewDishes.RowHeadersWidth = 62;
            this.dataGridViewDishes.RowTemplate.Height = 28;
            this.dataGridViewDishes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDishes.Size = new System.Drawing.Size(1036, 160);
            this.dataGridViewDishes.TabIndex = 0;
            // 
            // labelDishes
            // 
            this.labelDishes.AutoSize = true;
            this.labelDishes.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelDishes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelDishes.Location = new System.Drawing.Point(12, 118);
            this.labelDishes.Name = "labelDishes";
            this.labelDishes.Size = new System.Drawing.Size(64, 24);
            this.labelDishes.TabIndex = 8;
            this.labelDishes.Text = "菜品：";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(486, 56);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(200, 28);
            this.textBoxPrice.TabIndex = 7;
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelPrice.Location = new System.Drawing.Point(400, 58);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(86, 24);
            this.labelPrice.TabIndex = 6;
            this.labelPrice.Text = "套餐价格:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "启售",
            "停售"});
            this.comboBoxStatus.Location = new System.Drawing.Point(486, 16);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(200, 26);
            this.comboBoxStatus.TabIndex = 5;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelStatus.Location = new System.Drawing.Point(400, 16);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(86, 24);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "售卖状态:";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(104, 56);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(260, 26);
            this.comboBoxCategory.TabIndex = 3;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelCategory.Location = new System.Drawing.Point(12, 58);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(86, 24);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "套餐分类:";
            // 
            // textBoxSetmealName
            // 
            this.textBoxSetmealName.Location = new System.Drawing.Point(104, 16);
            this.textBoxSetmealName.MaxLength = 32;
            this.textBoxSetmealName.Name = "textBoxSetmealName";
            this.textBoxSetmealName.Size = new System.Drawing.Size(260, 28);
            this.textBoxSetmealName.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelName.Location = new System.Drawing.Point(12, 18);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(86, 24);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "套餐名称:";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.labelPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(16, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1171, 48);
            this.panelHeader.TabIndex = 0;
            // 
            // labelPageTitle
            // 
            this.labelPageTitle.AutoSize = true;
            this.labelPageTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.labelPageTitle.Location = new System.Drawing.Point(0, 10);
            this.labelPageTitle.Name = "labelPageTitle";
            this.labelPageTitle.Size = new System.Drawing.Size(101, 30);
            this.labelPageTitle.TabIndex = 0;
            this.labelPageTitle.Text = "新增套餐";
            // 
            // UcSetmealEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelRoot);
            this.Name = "UcSetmealEdit";
            this.Size = new System.Drawing.Size(1203, 785);
            this.panelRoot.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.groupBoxImage.ResumeLayout(false);
            this.groupBoxImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panelDishes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDishes)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelPageTitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxSetmealName;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label labelDishes;
        private System.Windows.Forms.Panel panelDishes;
        private System.Windows.Forms.DataGridView dataGridViewDishes;
        private System.Windows.Forms.Button buttonAddDish;
        private System.Windows.Forms.Button buttonRemoveDish;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.GroupBox groupBoxImage;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.TextBox textBoxImagePath;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveContinue;
    }
}

