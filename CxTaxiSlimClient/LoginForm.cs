using System;
using System.Windows.Forms;
using CxTaxiSlimClient.CxTaxiService;
using CxTaxiSlimClient.Properties;

namespace CxTaxiSlimClient
{
    public partial class LoginForm : Form
    {
        private IClientBonusService _service;
        public LoginForm(IClientBonusService service)
        {
            _service = service;
            InitializeComponent();
        }

        private LoginInfo _loginInfo;

        private async void button2_Click(object sender, EventArgs e)
        {
             
            if (string.IsNullOrWhiteSpace(teLogin.Text))
            {
                MessageBox.Show(
                    this,
                    Resources.LoginForm_UserNameCantBeEmpty, 
                    Resources.LoginForm_MessageCaption, 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //TODO RoleType
            var result = await _service.LoginAsync(teLogin.Text, tePass.Text, RoleTypes.Payin);
            //TODO Show progress

            if (!result.IsSucssied)
            {
                MessageBox.Show(
                    this,
                    result.Message,
                    Resources.LoginForm_MessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _loginInfo = result.Data;
            DialogResult = DialogResult.OK;
            Close();
        }

        public LoginInfo GetLoginInfo()
        {
            return _loginInfo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
