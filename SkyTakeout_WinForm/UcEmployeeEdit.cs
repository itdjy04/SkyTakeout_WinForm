using System;
using System.Linq;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class UcEmployeeEdit : UserControl
    {
        private bool allowSaveAndContinue;
        private bool isEditMode;

        internal event Action CancelRequested;
        internal event Action<EmployeeEditResult> SaveRequested;

        public UcEmployeeEdit()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            WireEvents();
        }

        private void WireEvents()
        {
            buttonBack.Click += (s, e) => CancelRequested?.Invoke();
            buttonCancel.Click += (s, e) => CancelRequested?.Invoke();
            buttonSave.Click += (s, e) => TryRaiseSave(false);
            buttonSaveContinue.Click += (s, e) => TryRaiseSave(true);
        }

        internal void EnterAddMode(bool allowSaveAndContinue)
        {
            this.allowSaveAndContinue = allowSaveAndContinue;
            isEditMode = false;
            labelPageTitle.Text = "添加员工";
            buttonSaveContinue.Visible = allowSaveAndContinue;

            textBoxUsername.ReadOnly = false;
            textBoxUsername.Text = string.Empty;
            textBoxName.Text = string.Empty;
            textBoxPhone.Text = string.Empty;
            radioMale.Checked = true;
            radioFemale.Checked = false;
            textBoxIdNumber.Text = string.Empty;
        }

        internal void EnterEditMode(EmployeeEditModel model)
        {
            allowSaveAndContinue = false;
            isEditMode = true;
            labelPageTitle.Text = "修改员工信息";
            buttonSaveContinue.Visible = false;

            EmployeeEditModel m = model ?? new EmployeeEditModel(0, string.Empty, string.Empty, string.Empty, "男", string.Empty, 1);
            textBoxUsername.ReadOnly = true;
            textBoxUsername.Text = (m.Username ?? string.Empty).Trim();
            textBoxName.Text = (m.Name ?? string.Empty).Trim();
            textBoxPhone.Text = (m.Phone ?? string.Empty).Trim();
            string sex = (m.Sex ?? string.Empty).Trim();
            radioMale.Checked = sex != "女";
            radioFemale.Checked = sex == "女";
            textBoxIdNumber.Text = (m.IdNumber ?? string.Empty).Trim();
        }

        private void TryRaiseSave(bool saveAndContinue)
        {
            if (saveAndContinue && !allowSaveAndContinue)
            {
                saveAndContinue = false;
            }

            string username = (textBoxUsername.Text ?? string.Empty).Trim();
            string name = (textBoxName.Text ?? string.Empty).Trim();
            string phone = (textBoxPhone.Text ?? string.Empty).Trim();
            string sex = radioFemale.Checked ? "女" : "男";
            string idNumber = (textBoxIdNumber.Text ?? string.Empty).Trim();

            if (!isEditMode)
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("请输入账号。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxUsername.Focus();
                    return;
                }
                if (username.Length > 32)
                {
                    MessageBox.Show("账号长度不能超过 32。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxUsername.Focus();
                    return;
                }
                if (username.Any(char.IsWhiteSpace))
                {
                    MessageBox.Show("账号不能包含空格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxUsername.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("请输入员工姓名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 11 || !phone.All(char.IsDigit))
            {
                MessageBox.Show("请输入正确的手机号（11位数字）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(idNumber) || idNumber.Length != 18)
            {
                MessageBox.Show("请输入正确的身份证号（18位）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxIdNumber.Focus();
                return;
            }

            SaveRequested?.Invoke(new EmployeeEditResult(username, name, phone, sex, idNumber, saveAndContinue));
        }
    }
}

