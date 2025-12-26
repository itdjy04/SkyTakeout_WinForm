namespace SkyTakeout_WinForm
{
    partial class UcEmployeeEdit
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
            this.textBoxIdNumber = new System.Windows.Forms.TextBox();
            this.labelIdNumber = new System.Windows.Forms.Label();
            this.radioFemale = new System.Windows.Forms.RadioButton();
            this.radioMale = new System.Windows.Forms.RadioButton();
            this.labelSex = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelPageTitle = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelForm.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(1191, 778);
            this.panelRoot.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelButtons);
            this.panelContent.Controls.Add(this.panelForm);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(16, 64);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1159, 698);
            this.panelContent.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonSaveContinue);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 646);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1159, 52);
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
            this.buttonSaveContinue.Location = new System.Drawing.Point(980, 10);
            this.buttonSaveContinue.Name = "buttonSaveContinue";
            this.buttonSaveContinue.Size = new System.Drawing.Size(156, 32);
            this.buttonSaveContinue.TabIndex = 2;
            this.buttonSaveContinue.Text = "保存并继续添加";
            this.buttonSaveContinue.UseVisualStyleBackColor = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(898, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(76, 32);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(816, 10);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 32);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelForm
            // 
            this.panelForm.Controls.Add(this.textBoxIdNumber);
            this.panelForm.Controls.Add(this.labelIdNumber);
            this.panelForm.Controls.Add(this.radioFemale);
            this.panelForm.Controls.Add(this.radioMale);
            this.panelForm.Controls.Add(this.labelSex);
            this.panelForm.Controls.Add(this.textBoxPhone);
            this.panelForm.Controls.Add(this.labelPhone);
            this.panelForm.Controls.Add(this.textBoxName);
            this.panelForm.Controls.Add(this.labelName);
            this.panelForm.Controls.Add(this.textBoxUsername);
            this.panelForm.Controls.Add(this.labelUsername);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(8);
            this.panelForm.Size = new System.Drawing.Size(1159, 698);
            this.panelForm.TabIndex = 0;
            // 
            // textBoxIdNumber
            // 
            this.textBoxIdNumber.Location = new System.Drawing.Point(110, 186);
            this.textBoxIdNumber.Name = "textBoxIdNumber";
            this.textBoxIdNumber.Size = new System.Drawing.Size(280, 28);
            this.textBoxIdNumber.TabIndex = 4;
            // 
            // labelIdNumber
            // 
            this.labelIdNumber.AutoSize = true;
            this.labelIdNumber.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelIdNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelIdNumber.Location = new System.Drawing.Point(12, 190);
            this.labelIdNumber.Name = "labelIdNumber";
            this.labelIdNumber.Size = new System.Drawing.Size(86, 24);
            this.labelIdNumber.TabIndex = 10;
            this.labelIdNumber.Text = "身份证号:";
            // 
            // radioFemale
            // 
            this.radioFemale.AutoSize = true;
            this.radioFemale.Location = new System.Drawing.Point(176, 148);
            this.radioFemale.Name = "radioFemale";
            this.radioFemale.Size = new System.Drawing.Size(51, 22);
            this.radioFemale.TabIndex = 3;
            this.radioFemale.TabStop = true;
            this.radioFemale.Text = "女";
            this.radioFemale.UseVisualStyleBackColor = true;
            // 
            // radioMale
            // 
            this.radioMale.AutoSize = true;
            this.radioMale.Location = new System.Drawing.Point(110, 148);
            this.radioMale.Name = "radioMale";
            this.radioMale.Size = new System.Drawing.Size(51, 22);
            this.radioMale.TabIndex = 2;
            this.radioMale.TabStop = true;
            this.radioMale.Text = "男";
            this.radioMale.UseVisualStyleBackColor = true;
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelSex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelSex.Location = new System.Drawing.Point(12, 146);
            this.labelSex.Name = "labelSex";
            this.labelSex.Size = new System.Drawing.Size(50, 24);
            this.labelSex.TabIndex = 7;
            this.labelSex.Text = "性别:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(110, 104);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(280, 28);
            this.textBoxPhone.TabIndex = 1;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelPhone.Location = new System.Drawing.Point(12, 108);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(50, 24);
            this.labelPhone.TabIndex = 5;
            this.labelPhone.Text = "手机:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(110, 62);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(280, 28);
            this.textBoxName.TabIndex = 0;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelName.Location = new System.Drawing.Point(12, 66);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(86, 24);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "员工姓名:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(110, 20);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(280, 28);
            this.textBoxUsername.TabIndex = 0;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelUsername.Location = new System.Drawing.Point(12, 24);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(50, 24);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "账号:";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.buttonBack);
            this.panelHeader.Controls.Add(this.labelPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(16, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1159, 48);
            this.panelHeader.TabIndex = 0;
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Location = new System.Drawing.Point(0, 8);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(60, 32);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "返回";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // labelPageTitle
            // 
            this.labelPageTitle.AutoSize = true;
            this.labelPageTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.labelPageTitle.Location = new System.Drawing.Point(70, 10);
            this.labelPageTitle.Name = "labelPageTitle";
            this.labelPageTitle.Size = new System.Drawing.Size(101, 30);
            this.labelPageTitle.TabIndex = 1;
            this.labelPageTitle.Text = "添加员工";
            // 
            // UcEmployeeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelRoot);
            this.Name = "UcEmployeeEdit";
            this.Size = new System.Drawing.Size(1191, 778);
            this.panelRoot.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelPageTitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonSaveContinue;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.TextBox textBoxIdNumber;
        private System.Windows.Forms.Label labelIdNumber;
        private System.Windows.Forms.RadioButton radioFemale;
        private System.Windows.Forms.RadioButton radioMale;
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
    }
}

