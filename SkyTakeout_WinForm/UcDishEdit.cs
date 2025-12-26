using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcDishEdit : UserControl
    {
        private bool isEditMode;
        private string selectedImagePath = string.Empty;
        private readonly List<string> tastes = new List<string>();

        private Panel panelRoot;
        private Panel panelHeader;
        private Label labelPageTitle;
        private Panel panelContent;
        private Panel panelButtons;
        private Button buttonSave;
        private Button buttonCancel;
        private Panel panelForm;
        private TableLayoutPanel tableForm;
        private Label labelDishName;
        private TextBox textBoxDishName;
        private Label labelCategory;
        private ComboBox comboBoxCategory;
        private Label labelPrice;
        private TextBox textBoxPrice;
        private Label labelStatus;
        private ComboBox comboBoxStatus;
        private Label labelTaste;
        private Panel panelTaste;
        private TextBox textBoxTasteName;
        private Button buttonAddTaste;
        private FlowLayoutPanel flowTasteTags;
        private Label labelImage;
        private TableLayoutPanel tableImage;
        private Panel panelUploadBox;
        private PictureBox pictureBoxPreview;
        private Label labelUploadText;
        private Label labelUploadHelp;
        private Label labelDescription;
        private TextBox textBoxDescription;
        private Panel panelDesc;
        private Label labelDescHint;

        internal event Action CancelRequested;
        internal event Action<DishEditResult> SaveRequested;

        public UcDishEdit()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            WireEvents();
        }

        private void InitializeComponent()
        {
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelForm = new System.Windows.Forms.Panel();
            this.tableForm = new System.Windows.Forms.TableLayoutPanel();
            this.labelDishName = new System.Windows.Forms.Label();
            this.textBoxDishName = new System.Windows.Forms.TextBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelTaste = new System.Windows.Forms.Label();
            this.panelTaste = new System.Windows.Forms.Panel();
            this.textBoxTasteName = new System.Windows.Forms.TextBox();
            this.buttonAddTaste = new System.Windows.Forms.Button();
            this.flowTasteTags = new System.Windows.Forms.FlowLayoutPanel();
            this.labelImage = new System.Windows.Forms.Label();
            this.tableImage = new System.Windows.Forms.TableLayoutPanel();
            this.panelUploadBox = new System.Windows.Forms.Panel();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.labelUploadText = new System.Windows.Forms.Label();
            this.labelUploadHelp = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.panelDesc = new System.Windows.Forms.Panel();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescHint = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelPageTitle = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.tableForm.SuspendLayout();
            this.panelTaste.SuspendLayout();
            this.tableImage.SuspendLayout();
            this.panelUploadBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panelDesc.SuspendLayout();
            this.panelButtons.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(1191, 898);
            this.panelRoot.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelForm);
            this.panelContent.Controls.Add(this.panelButtons);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(16, 64);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1159, 818);
            this.panelContent.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.Controls.Add(this.tableForm);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(8);
            this.panelForm.Size = new System.Drawing.Size(1159, 766);
            this.panelForm.TabIndex = 0;
            // 
            // tableForm
            // 
            this.tableForm.ColumnCount = 4;
            this.tableForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableForm.Controls.Add(this.labelDishName, 0, 0);
            this.tableForm.Controls.Add(this.textBoxDishName, 1, 0);
            this.tableForm.Controls.Add(this.labelCategory, 2, 0);
            this.tableForm.Controls.Add(this.comboBoxCategory, 3, 0);
            this.tableForm.Controls.Add(this.labelPrice, 0, 1);
            this.tableForm.Controls.Add(this.textBoxPrice, 1, 1);
            this.tableForm.Controls.Add(this.labelStatus, 2, 1);
            this.tableForm.Controls.Add(this.comboBoxStatus, 3, 1);
            this.tableForm.Controls.Add(this.labelTaste, 0, 2);
            this.tableForm.Controls.Add(this.panelTaste, 1, 2);
            this.tableForm.Controls.Add(this.labelImage, 0, 3);
            this.tableForm.Controls.Add(this.tableImage, 1, 3);
            this.tableForm.Controls.Add(this.labelDescription, 0, 4);
            this.tableForm.Controls.Add(this.panelDesc, 1, 4);
            this.tableForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableForm.Location = new System.Drawing.Point(8, 8);
            this.tableForm.Name = "tableForm";
            this.tableForm.RowCount = 6;
            this.tableForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableForm.Size = new System.Drawing.Size(1143, 750);
            this.tableForm.TabIndex = 0;
            // 
            // labelDishName
            // 
            this.labelDishName.AutoSize = true;
            this.labelDishName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDishName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelDishName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelDishName.Location = new System.Drawing.Point(3, 0);
            this.labelDishName.Name = "labelDishName";
            this.labelDishName.Size = new System.Drawing.Size(104, 48);
            this.labelDishName.TabIndex = 0;
            this.labelDishName.Text = "* 菜品名称:";
            this.labelDishName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDishName
            // 
            this.textBoxDishName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDishName.Location = new System.Drawing.Point(113, 10);
            this.textBoxDishName.Margin = new System.Windows.Forms.Padding(3, 10, 20, 10);
            this.textBoxDishName.Name = "textBoxDishName";
            this.textBoxDishName.Size = new System.Drawing.Size(438, 28);
            this.textBoxDishName.TabIndex = 1;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelCategory.Location = new System.Drawing.Point(574, 0);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(104, 48);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "* 菜品分类:";
            this.labelCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxCategory.Location = new System.Drawing.Point(684, 10);
            this.comboBoxCategory.Margin = new System.Windows.Forms.Padding(3, 10, 20, 10);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(439, 26);
            this.comboBoxCategory.TabIndex = 3;
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPrice.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelPrice.Location = new System.Drawing.Point(3, 48);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(104, 48);
            this.labelPrice.TabIndex = 4;
            this.labelPrice.Text = "* 菜品价格:";
            this.labelPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPrice.Location = new System.Drawing.Point(113, 58);
            this.textBoxPrice.Margin = new System.Windows.Forms.Padding(3, 10, 20, 10);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(438, 28);
            this.textBoxPrice.TabIndex = 5;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelStatus.Location = new System.Drawing.Point(574, 48);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(104, 48);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "售卖状态:";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "启售",
            "停售"});
            this.comboBoxStatus.Location = new System.Drawing.Point(684, 58);
            this.comboBoxStatus.Margin = new System.Windows.Forms.Padding(3, 10, 20, 10);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(439, 26);
            this.comboBoxStatus.TabIndex = 7;
            // 
            // labelTaste
            // 
            this.labelTaste.AutoSize = true;
            this.labelTaste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTaste.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelTaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelTaste.Location = new System.Drawing.Point(3, 96);
            this.labelTaste.Name = "labelTaste";
            this.labelTaste.Size = new System.Drawing.Size(104, 130);
            this.labelTaste.TabIndex = 8;
            this.labelTaste.Text = "口味做法配置:";
            // 
            // panelTaste
            // 
            this.tableForm.SetColumnSpan(this.panelTaste, 3);
            this.panelTaste.Controls.Add(this.textBoxTasteName);
            this.panelTaste.Controls.Add(this.buttonAddTaste);
            this.panelTaste.Controls.Add(this.flowTasteTags);
            this.panelTaste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTaste.Location = new System.Drawing.Point(113, 99);
            this.panelTaste.Name = "panelTaste";
            this.panelTaste.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panelTaste.Size = new System.Drawing.Size(1027, 124);
            this.panelTaste.TabIndex = 9;
            // 
            // textBoxTasteName
            // 
            this.textBoxTasteName.Location = new System.Drawing.Point(0, 0);
            this.textBoxTasteName.Name = "textBoxTasteName";
            this.textBoxTasteName.Size = new System.Drawing.Size(240, 28);
            this.textBoxTasteName.TabIndex = 0;
            // 
            // buttonAddTaste
            // 
            this.buttonAddTaste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonAddTaste.FlatAppearance.BorderSize = 0;
            this.buttonAddTaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTaste.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.buttonAddTaste.ForeColor = System.Drawing.Color.White;
            this.buttonAddTaste.Location = new System.Drawing.Point(248, -1);
            this.buttonAddTaste.Name = "buttonAddTaste";
            this.buttonAddTaste.Size = new System.Drawing.Size(120, 30);
            this.buttonAddTaste.TabIndex = 1;
            this.buttonAddTaste.Text = "创建口味";
            this.buttonAddTaste.UseVisualStyleBackColor = false;
            // 
            // flowTasteTags
            // 
            this.flowTasteTags.AutoScroll = true;
            this.flowTasteTags.Location = new System.Drawing.Point(0, 40);
            this.flowTasteTags.Name = "flowTasteTags";
            this.flowTasteTags.Size = new System.Drawing.Size(760, 86);
            this.flowTasteTags.TabIndex = 2;
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelImage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelImage.Location = new System.Drawing.Point(3, 226);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(104, 220);
            this.labelImage.TabIndex = 10;
            this.labelImage.Text = "* 菜品图片:";
            // 
            // tableImage
            // 
            this.tableImage.ColumnCount = 2;
            this.tableForm.SetColumnSpan(this.tableImage, 3);
            this.tableImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableImage.Controls.Add(this.panelUploadBox, 0, 0);
            this.tableImage.Controls.Add(this.labelUploadHelp, 1, 0);
            this.tableImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableImage.Location = new System.Drawing.Point(113, 229);
            this.tableImage.Name = "tableImage";
            this.tableImage.RowCount = 1;
            this.tableImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableImage.Size = new System.Drawing.Size(1027, 214);
            this.tableImage.TabIndex = 11;
            // 
            // panelUploadBox
            // 
            this.panelUploadBox.BackColor = System.Drawing.Color.White;
            this.panelUploadBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUploadBox.Controls.Add(this.pictureBoxPreview);
            this.panelUploadBox.Controls.Add(this.labelUploadText);
            this.panelUploadBox.Location = new System.Drawing.Point(0, 10);
            this.panelUploadBox.Margin = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.panelUploadBox.Name = "panelUploadBox";
            this.panelUploadBox.Size = new System.Drawing.Size(200, 194);
            this.panelUploadBox.TabIndex = 0;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.White;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(198, 192);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 0;
            this.pictureBoxPreview.TabStop = false;
            // 
            // labelUploadText
            // 
            this.labelUploadText.BackColor = System.Drawing.Color.Transparent;
            this.labelUploadText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUploadText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelUploadText.Location = new System.Drawing.Point(0, 0);
            this.labelUploadText.Name = "labelUploadText";
            this.labelUploadText.Size = new System.Drawing.Size(198, 192);
            this.labelUploadText.TabIndex = 1;
            this.labelUploadText.Text = "上传图片";
            this.labelUploadText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUploadHelp
            // 
            this.labelUploadHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUploadHelp.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelUploadHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.labelUploadHelp.Location = new System.Drawing.Point(210, 14);
            this.labelUploadHelp.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.labelUploadHelp.Name = "labelUploadHelp";
            this.labelUploadHelp.Size = new System.Drawing.Size(817, 200);
            this.labelUploadHelp.TabIndex = 1;
            this.labelUploadHelp.Text = "图片大小不超过2M\r\n只支持 PNG JPG JPEG\r\n建议上传200*200或300*300的图片";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.labelDescription.Location = new System.Drawing.Point(3, 446);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(104, 170);
            this.labelDescription.TabIndex = 12;
            this.labelDescription.Text = "菜品描述:";
            // 
            // panelDesc
            // 
            this.tableForm.SetColumnSpan(this.panelDesc, 3);
            this.panelDesc.Controls.Add(this.textBoxDescription);
            this.panelDesc.Controls.Add(this.labelDescHint);
            this.panelDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesc.Location = new System.Drawing.Point(113, 449);
            this.panelDesc.Name = "panelDesc";
            this.panelDesc.Size = new System.Drawing.Size(1027, 164);
            this.panelDesc.TabIndex = 13;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Location = new System.Drawing.Point(0, 0);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(1027, 142);
            this.textBoxDescription.TabIndex = 0;
            // 
            // labelDescHint
            // 
            this.labelDescHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDescHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.labelDescHint.Location = new System.Drawing.Point(0, 142);
            this.labelDescHint.Name = "labelDescHint";
            this.labelDescHint.Size = new System.Drawing.Size(1027, 22);
            this.labelDescHint.TabIndex = 1;
            this.labelDescHint.Text = "未填写描述，最多200字";
            this.labelDescHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 766);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1159, 52);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(1063, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 36);
            this.buttonSave.TabIndex = 0;
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
            this.buttonCancel.Location = new System.Drawing.Point(971, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 36);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.labelPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(16, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1159, 48);
            this.panelHeader.TabIndex = 1;
            // 
            // labelPageTitle
            // 
            this.labelPageTitle.AutoSize = true;
            this.labelPageTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.labelPageTitle.Location = new System.Drawing.Point(4, 10);
            this.labelPageTitle.Name = "labelPageTitle";
            this.labelPageTitle.Size = new System.Drawing.Size(101, 30);
            this.labelPageTitle.TabIndex = 1;
            this.labelPageTitle.Text = "新增菜品";
            // 
            // UcDishEdit
            // 
            this.Controls.Add(this.panelRoot);
            this.Name = "UcDishEdit";
            this.Size = new System.Drawing.Size(1191, 898);
            this.panelRoot.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.tableForm.ResumeLayout(false);
            this.tableForm.PerformLayout();
            this.panelTaste.ResumeLayout(false);
            this.panelTaste.PerformLayout();
            this.tableImage.ResumeLayout(false);
            this.panelUploadBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panelDesc.ResumeLayout(false);
            this.panelDesc.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private void WireEvents()
        {
            buttonCancel.Click += (s, e) => CancelRequested?.Invoke();
            buttonSave.Click += (s, e) => TryRaiseSave();
            buttonAddTaste.Click += buttonAddTaste_Click;
            panelUploadBox.Click += buttonUpload_Click;
            labelUploadText.Click += buttonUpload_Click;
            pictureBoxPreview.Click += buttonUpload_Click;
            textBoxDescription.TextChanged += textBoxDescription_TextChanged;
        }

        internal void EnterAddMode(List<string> categoryOptions)
        {
            isEditMode = false;
            labelPageTitle.Text = "新增菜品";
            BindCategoryOptions(categoryOptions, string.Empty);
            comboBoxCategory.Text = string.Empty;
            textBoxDishName.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            selectedImagePath = string.Empty;
            tastes.Clear();
            RebindTasteTags();
            textBoxTasteName.Text = string.Empty;
            textBoxDescription.Text = string.Empty;
            RefreshImagePreview();
            comboBoxStatus.SelectedIndex = 0;
        }

        internal void EnterEditMode(DishEditModel model, List<string> categoryOptions)
        {
            isEditMode = true;
            labelPageTitle.Text = "修改菜品";

            DishEditModel m = model ?? new DishEditModel(string.Empty, string.Empty, string.Empty, string.Empty, "启售");
            BindCategoryOptions(categoryOptions, m.Category);
            textBoxDishName.Text = (m.Name ?? string.Empty).Trim();
            comboBoxCategory.Text = (m.Category ?? string.Empty).Trim();
            textBoxPrice.Text = (m.PriceText ?? string.Empty).Trim();
            selectedImagePath = (m.ImagePath ?? string.Empty).Trim();
            comboBoxStatus.SelectedItem = (m.StatusText ?? string.Empty).Trim();
            if (comboBoxStatus.SelectedIndex < 0)
            {
                comboBoxStatus.SelectedIndex = 0;
            }
            RefreshImagePreview();
        }

        internal void SetTastes(List<string> values)
        {
            tastes.Clear();
            if (values != null && values.Count > 0)
            {
                HashSet<string> uniq = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < values.Count; i++)
                {
                    string v = (values[i] ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(v))
                    {
                        continue;
                    }

                    if (uniq.Add(v))
                    {
                        tastes.Add(v);
                    }
                }
            }
            RebindTasteTags();
        }

        internal void UpdateCategoryOptions(List<string> categoryOptions)
        {
            string current = (comboBoxCategory.Text ?? string.Empty).Trim();
            BindCategoryOptions(categoryOptions, current);
            comboBoxCategory.Text = current;
        }

        private void BindCategoryOptions(List<string> categoryOptions, string preferText)
        {
            comboBoxCategory.BeginUpdate();
            comboBoxCategory.Items.Clear();
            if (categoryOptions != null && categoryOptions.Count > 0)
            {
                comboBoxCategory.Items.AddRange(categoryOptions.Cast<object>().ToArray());
            }
            comboBoxCategory.EndUpdate();

            string preferred = (preferText ?? string.Empty).Trim();
            if (!string.IsNullOrWhiteSpace(preferred))
            {
                for (int i = 0; i < comboBoxCategory.Items.Count; i++)
                {
                    if (comboBoxCategory.Items[i] is string s && string.Equals(s?.Trim(), preferred, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBoxCategory.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif|所有文件|*.*";
                dialog.Multiselect = false;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    selectedImagePath = dialog.FileName ?? string.Empty;
                    RefreshImagePreview();
                }
            }
        }

        private void TryRaiseSave()
        {
            string name = (textBoxDishName.Text ?? string.Empty).Trim();
            string category = (comboBoxCategory.Text ?? string.Empty).Trim();
            string priceText = (textBoxPrice.Text ?? string.Empty).Trim();
            string imagePath = (selectedImagePath ?? string.Empty).Trim();
            string statusText = comboBoxStatus.SelectedItem as string ?? string.Empty;
            string description = (textBoxDescription.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("请输入菜品名称。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxDishName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("请选择菜品分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxCategory.Focus();
                return;
            }

            decimal p;
            if (!decimal.TryParse(priceText, out p) || p < 0)
            {
                MessageBox.Show("请输入正确的价格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPrice.Focus();
                return;
            }

            SaveRequested?.Invoke(new DishEditResult(name, category, priceText, imagePath, statusText, isEditMode, description, tastes.ToList()));
        }

        private void buttonAddTaste_Click(object sender, EventArgs e)
        {
            string t = (textBoxTasteName.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(t))
            {
                textBoxTasteName.Focus();
                return;
            }

            if (!tastes.Any(x => string.Equals(x, t, StringComparison.OrdinalIgnoreCase)))
            {
                tastes.Add(t);
                RebindTasteTags();
            }

            textBoxTasteName.Text = string.Empty;
            textBoxTasteName.Focus();
        }

        private void RebindTasteTags()
        {
            flowTasteTags.SuspendLayout();
            flowTasteTags.Controls.Clear();
            for (int i = 0; i < tastes.Count; i++)
            {
                string value = tastes[i];
                Panel tag = new Panel();
                tag.Height = 28;
                tag.AutoSize = true;
                tag.Padding = new Padding(10, 4, 8, 4);
                tag.BackColor = Color.FromArgb(245, 246, 247);
                tag.Margin = new Padding(0, 0, 10, 10);

                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = value;
                lbl.ForeColor = Color.FromArgb(96, 98, 102);
                lbl.Location = new Point(6, 5);

                LinkLabel remove = new LinkLabel();
                remove.AutoSize = true;
                remove.Text = "删除";
                remove.LinkColor = Color.FromArgb(245, 108, 108);
                remove.ActiveLinkColor = Color.FromArgb(245, 108, 108);
                remove.VisitedLinkColor = Color.FromArgb(245, 108, 108);
                remove.Location = new Point(lbl.Right + 10, 5);
                remove.Tag = value;
                remove.Click += (s, e) =>
                {
                    if (s is Control c && c.Tag is string v)
                    {
                        tastes.RemoveAll(x => string.Equals(x, v, StringComparison.OrdinalIgnoreCase));
                        RebindTasteTags();
                    }
                };

                tag.Controls.Add(lbl);
                tag.Controls.Add(remove);
                remove.Left = lbl.Right + 10;
                flowTasteTags.Controls.Add(tag);
            }
            flowTasteTags.ResumeLayout();
        }

        private void RefreshImagePreview()
        {
            string path = (selectedImagePath ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                pictureBoxPreview.Image = null;
                labelUploadText.Visible = true;
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (Image img = Image.FromStream(fs))
                {
                    pictureBoxPreview.Image = (Image)img.Clone();
                }
                labelUploadText.Visible = false;
            }
            catch
            {
                pictureBoxPreview.Image = null;
                labelUploadText.Visible = true;
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            int len = (textBoxDescription.Text ?? string.Empty).Length;
            if (len <= 0)
            {
                labelDescHint.Text = "未填写描述，最多200字";
                return;
            }
            labelDescHint.Text = $"已输入{len}字，最多200字";
        }

    }

    internal sealed class DishEditModel
    {
        public string Name { get; }
        public string Category { get; }
        public string PriceText { get; }
        public string ImagePath { get; }
        public string StatusText { get; }

        public DishEditModel(string name, string category, string priceText, string imagePath, string statusText)
        {
            Name = name ?? string.Empty;
            Category = category ?? string.Empty;
            PriceText = priceText ?? string.Empty;
            ImagePath = imagePath ?? string.Empty;
            StatusText = statusText ?? string.Empty;
        }
    }

    internal sealed class DishEditResult
    {
        public string Name { get; }
        public string Category { get; }
        public string PriceText { get; }
        public string ImagePath { get; }
        public string StatusText { get; }
        public bool IsEditMode { get; }
        public string Description { get; }
        public List<string> Tastes { get; }

        public DishEditResult(string name, string category, string priceText, string imagePath, string statusText, bool isEditMode, string description, List<string> tastes)
        {
            Name = name ?? string.Empty;
            Category = category ?? string.Empty;
            PriceText = priceText ?? string.Empty;
            ImagePath = imagePath ?? string.Empty;
            StatusText = statusText ?? string.Empty;
            IsEditMode = isEditMode;
            Description = description ?? string.Empty;
            Tastes = tastes ?? new List<string>();
        }
    }
}
