using CxTaxiSlimClient.CxTaxiService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CxTaxiSlimClient
{
    public partial class UserEditForm : Form
    {
        private static MD5 _md5Hash = MD5.Create();
        public UserParam User { get; private set; }

        private bool _createMode;
        private bool _passIsChanged;

        public UserEditForm(UserParam user, bool createMode)
        {
            _createMode = createMode;
            InitializeComponent();

            teRate.MaxValue = 1.00;
            if (createMode)
            {
                User = new UserParam();
                User.Roles = new RoleTypes[0];
                tePass.Text = null;
            }
            else
            {
                User = user;
                tePass.Text = "hiden_pass";
                teLogin.Enabled = false;
                teLogin.ReadOnly = true;
            }

            teLogin.Text = User.Login;            
            teName.Text = User.UserName;
            teRate.Text = User.Rate.ToString();
            cbPayIn.Checked = User.Roles.Contains(RoleTypes.Payin);
            cbPayOut.Checked = User.Roles.Contains(RoleTypes.Payout);
            _passIsChanged = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_createMode)
            {
                if (string.IsNullOrWhiteSpace(teLogin.Text))
                {
                    MessageBox.Show(
                        this,
                        "Логин не должен быть пустым",
                        "Управление пользователями",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
                User.Login = teLogin.Text;
            }

            if (string.IsNullOrWhiteSpace(tePass.Text))
            {
                MessageBox.Show(
                    this,
                    "Пароль не должен быть пустым",
                    "Управление пользователями",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (_passIsChanged)
            {
                User.Password = GetMd5Hash(tePass.Text);
            }

            User.UserName = teName.Text;
            User.Rate = double.Parse(teRate.Text);
            var roles = new List<RoleTypes>(2);
            if (cbPayIn.Checked)
                roles.Add(RoleTypes.Payin);
            if (cbPayOut.Checked)
                roles.Add(RoleTypes.Payout);

            if (!roles.Any())
            {
                MessageBox.Show(
                    this,
                    "У пользователя нет ни одной роли.",
                    "Управление пользователями",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            User.Roles = roles.ToArray();

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        public static string GetMd5Hash(string input)
        {
            byte[] data = _md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void tePass_TextChanged(object sender, EventArgs e)
        {
            _passIsChanged = true;
        }
        
    }
}
