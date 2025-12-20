namespace SkyTakeout_WinForm
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.labelUser = new System.Windows.Forms.Label();
            this.buttonBusinessStatus = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelSide = new System.Windows.Forms.Panel();
            this.panelSideIndicator = new System.Windows.Forms.Panel();
            this.buttonMenuEmployee = new System.Windows.Forms.Button();
            this.buttonMenuCategory = new System.Windows.Forms.Button();
            this.buttonMenuDish = new System.Windows.Forms.Button();
            this.buttonMenuSetmeal = new System.Windows.Forms.Button();
            this.buttonMenuOrder = new System.Windows.Forms.Button();
            this.buttonMenuStatistics = new System.Windows.Forms.Button();
            this.buttonMenuDashboard = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelContentPlaceholderCard = new System.Windows.Forms.Panel();
            this.labelContentPlaceholder = new System.Windows.Forms.Label();
            this.labelContentTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelTopRight.SuspendLayout();
            this.panelSide.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelContentPlaceholderCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.panelTop.Controls.Add(this.panelTopRight);
            this.panelTop.Controls.Add(this.buttonBusinessStatus);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1392, 64);
            this.panelTop.TabIndex = 0;
            // 
            // panelTopRight
            // 
            this.panelTopRight.Controls.Add(this.labelUser);
            this.panelTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTopRight.Location = new System.Drawing.Point(1212, 0);
            this.panelTopRight.Name = "panelTopRight";
            this.panelTopRight.Padding = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.panelTopRight.Size = new System.Drawing.Size(180, 64);
            this.panelTopRight.TabIndex = 2;
            // 
            // labelUser
            // 
            this.labelUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.labelUser.Location = new System.Drawing.Point(110, 22);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(64, 24);
            this.labelUser.TabIndex = 0;
            this.labelUser.Text = "管理员";
            // 
            // buttonBusinessStatus
            // 
            this.buttonBusinessStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(206)))), ((int)(((byte)(77)))));
            this.buttonBusinessStatus.FlatAppearance.BorderSize = 0;
            this.buttonBusinessStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBusinessStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBusinessStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.buttonBusinessStatus.Location = new System.Drawing.Point(180, 21);
            this.buttonBusinessStatus.Name = "buttonBusinessStatus";
            this.buttonBusinessStatus.Size = new System.Drawing.Size(86, 37);
            this.buttonBusinessStatus.TabIndex = 1;
            this.buttonBusinessStatus.Text = "营业中";
            this.buttonBusinessStatus.UseVisualStyleBackColor = false;
            this.buttonBusinessStatus.Click += new System.EventHandler(this.buttonBusinessStatus_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(18, 16);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(129, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "苍穹外卖";
            // 
            // panelSide
            // 
            this.panelSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(55)))));
            this.panelSide.Controls.Add(this.panelSideIndicator);
            this.panelSide.Controls.Add(this.buttonMenuEmployee);
            this.panelSide.Controls.Add(this.buttonMenuCategory);
            this.panelSide.Controls.Add(this.buttonMenuDish);
            this.panelSide.Controls.Add(this.buttonMenuSetmeal);
            this.panelSide.Controls.Add(this.buttonMenuOrder);
            this.panelSide.Controls.Add(this.buttonMenuStatistics);
            this.panelSide.Controls.Add(this.buttonMenuDashboard);
            this.panelSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSide.Location = new System.Drawing.Point(0, 64);
            this.panelSide.Name = "panelSide";
            this.panelSide.Size = new System.Drawing.Size(180, 1012);
            this.panelSide.TabIndex = 1;
            // 
            // panelSideIndicator
            // 
            this.panelSideIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.panelSideIndicator.Location = new System.Drawing.Point(0, 16);
            this.panelSideIndicator.Name = "panelSideIndicator";
            this.panelSideIndicator.Size = new System.Drawing.Size(4, 44);
            this.panelSideIndicator.TabIndex = 7;
            this.panelSideIndicator.Visible = false;
            // 
            // buttonMenuEmployee
            // 
            this.buttonMenuEmployee.FlatAppearance.BorderSize = 0;
            this.buttonMenuEmployee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuEmployee.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuEmployee.Location = new System.Drawing.Point(12, 316);
            this.buttonMenuEmployee.Name = "buttonMenuEmployee";
            this.buttonMenuEmployee.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuEmployee.TabIndex = 6;
            this.buttonMenuEmployee.TabStop = false;
            this.buttonMenuEmployee.Text = "员工管理";
            this.buttonMenuEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuEmployee.UseVisualStyleBackColor = true;
            this.buttonMenuEmployee.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // buttonMenuCategory
            // 
            this.buttonMenuCategory.FlatAppearance.BorderSize = 0;
            this.buttonMenuCategory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuCategory.Location = new System.Drawing.Point(12, 266);
            this.buttonMenuCategory.Name = "buttonMenuCategory";
            this.buttonMenuCategory.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuCategory.TabIndex = 5;
            this.buttonMenuCategory.TabStop = false;
            this.buttonMenuCategory.Text = "分类管理";
            this.buttonMenuCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuCategory.UseVisualStyleBackColor = true;
            this.buttonMenuCategory.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // buttonMenuDish
            // 
            this.buttonMenuDish.FlatAppearance.BorderSize = 0;
            this.buttonMenuDish.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuDish.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuDish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuDish.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuDish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuDish.Location = new System.Drawing.Point(12, 216);
            this.buttonMenuDish.Name = "buttonMenuDish";
            this.buttonMenuDish.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuDish.TabIndex = 4;
            this.buttonMenuDish.TabStop = false;
            this.buttonMenuDish.Text = "菜品管理";
            this.buttonMenuDish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuDish.UseVisualStyleBackColor = true;
            this.buttonMenuDish.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // buttonMenuSetmeal
            // 
            this.buttonMenuSetmeal.FlatAppearance.BorderSize = 0;
            this.buttonMenuSetmeal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuSetmeal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuSetmeal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuSetmeal.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuSetmeal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuSetmeal.Location = new System.Drawing.Point(12, 166);
            this.buttonMenuSetmeal.Name = "buttonMenuSetmeal";
            this.buttonMenuSetmeal.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuSetmeal.TabIndex = 3;
            this.buttonMenuSetmeal.TabStop = false;
            this.buttonMenuSetmeal.Text = "套餐管理";
            this.buttonMenuSetmeal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuSetmeal.UseVisualStyleBackColor = true;
            this.buttonMenuSetmeal.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // buttonMenuOrder
            // 
            this.buttonMenuOrder.FlatAppearance.BorderSize = 0;
            this.buttonMenuOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuOrder.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuOrder.Location = new System.Drawing.Point(12, 116);
            this.buttonMenuOrder.Name = "buttonMenuOrder";
            this.buttonMenuOrder.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuOrder.TabIndex = 2;
            this.buttonMenuOrder.TabStop = false;
            this.buttonMenuOrder.Text = "订单管理";
            this.buttonMenuOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuOrder.UseVisualStyleBackColor = true;
            this.buttonMenuOrder.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // buttonMenuStatistics
            // 
            this.buttonMenuStatistics.FlatAppearance.BorderSize = 0;
            this.buttonMenuStatistics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuStatistics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuStatistics.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuStatistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuStatistics.Location = new System.Drawing.Point(12, 66);
            this.buttonMenuStatistics.Name = "buttonMenuStatistics";
            this.buttonMenuStatistics.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuStatistics.TabIndex = 1;
            this.buttonMenuStatistics.TabStop = false;
            this.buttonMenuStatistics.Text = "数据统计";
            this.buttonMenuStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuStatistics.UseVisualStyleBackColor = true;
            this.buttonMenuStatistics.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // buttonMenuDashboard
            // 
            this.buttonMenuDashboard.FlatAppearance.BorderSize = 0;
            this.buttonMenuDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
            this.buttonMenuDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMenuDashboard.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.buttonMenuDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.buttonMenuDashboard.Location = new System.Drawing.Point(12, 16);
            this.buttonMenuDashboard.Name = "buttonMenuDashboard";
            this.buttonMenuDashboard.Size = new System.Drawing.Size(156, 44);
            this.buttonMenuDashboard.TabIndex = 0;
            this.buttonMenuDashboard.TabStop = false;
            this.buttonMenuDashboard.Text = "工作台";
            this.buttonMenuDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMenuDashboard.UseVisualStyleBackColor = true;
            this.buttonMenuDashboard.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.panelContent.Controls.Add(this.panelContentPlaceholderCard);
            this.panelContent.Controls.Add(this.labelContentTitle);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(180, 64);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(16);
            this.panelContent.Size = new System.Drawing.Size(1212, 1012);
            this.panelContent.TabIndex = 2;
            // 
            // panelContentPlaceholderCard
            // 
            this.panelContentPlaceholderCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContentPlaceholderCard.BackColor = System.Drawing.Color.White;
            this.panelContentPlaceholderCard.Controls.Add(this.labelContentPlaceholder);
            this.panelContentPlaceholderCard.Location = new System.Drawing.Point(22, 116);
            this.panelContentPlaceholderCard.Name = "panelContentPlaceholderCard";
            this.panelContentPlaceholderCard.Size = new System.Drawing.Size(1168, 880);
            this.panelContentPlaceholderCard.TabIndex = 1;
            // 
            // labelContentPlaceholder
            // 
            this.labelContentPlaceholder.AutoSize = true;
            this.labelContentPlaceholder.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.labelContentPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelContentPlaceholder.Location = new System.Drawing.Point(16, 18);
            this.labelContentPlaceholder.Name = "labelContentPlaceholder";
            this.labelContentPlaceholder.Size = new System.Drawing.Size(406, 27);
            this.labelContentPlaceholder.TabIndex = 0;
            this.labelContentPlaceholder.Text = "这里放模块页面内容（UserControl/Panel）";
            // 
            // labelContentTitle
            // 
            this.labelContentTitle.AutoSize = true;
            this.labelContentTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelContentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.labelContentTitle.Location = new System.Drawing.Point(19, 36);
            this.labelContentTitle.Name = "labelContentTitle";
            this.labelContentTitle.Size = new System.Drawing.Size(128, 47);
            this.labelContentTitle.TabIndex = 0;
            this.labelContentTitle.Text = "工作台";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1392, 1076);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSide);
            this.Controls.Add(this.panelTop);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "苍穹外卖";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelTopRight.ResumeLayout(false);
            this.panelTopRight.PerformLayout();
            this.panelSide.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelContentPlaceholderCard.ResumeLayout(false);
            this.panelContentPlaceholderCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelTopRight;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Button buttonBusinessStatus;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelSide;
        private System.Windows.Forms.Panel panelSideIndicator;
        private System.Windows.Forms.Button buttonMenuEmployee;
        private System.Windows.Forms.Button buttonMenuCategory;
        private System.Windows.Forms.Button buttonMenuDish;
        private System.Windows.Forms.Button buttonMenuSetmeal;
        private System.Windows.Forms.Button buttonMenuOrder;
        private System.Windows.Forms.Button buttonMenuStatistics;
        private System.Windows.Forms.Button buttonMenuDashboard;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelContentPlaceholderCard;
        private System.Windows.Forms.Label labelContentPlaceholder;
        private System.Windows.Forms.Label labelContentTitle;
    }
}
