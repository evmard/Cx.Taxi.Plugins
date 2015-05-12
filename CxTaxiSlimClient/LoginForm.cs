using System;
using System.ServiceModel;
using System.Windows.Forms;
using CxTaxiSlimClient.CxTaxiService;
using CxTaxiSlimClient.Properties;

namespace CxTaxiSlimClient
{
    public partial class LoginForm : Form
    {
        private IClientBonusService _service;

        private ClientParams _params;
        public LoginForm(IClientBonusService service)
        {
            _service = service;
            InitializeComponent();
            _params = ClientParams.Load();
            teLogin.Text = _params.UserName;
            
            if (_params.RoleType.HasValue)
            {
                switch (_params.RoleType.Value)
                {
                    case RoleTypes.Payin:
                        cbRole.SelectedIndex = 0;
                        break;
                    case RoleTypes.Payout:
                        cbRole.SelectedIndex = 1;
                        break;
                }
            }

            teIp.Text = _params.HostName;
            tePort.Text = _params.Port.ToString();
        }

        private LoginInfo _loginInfo;

        private async void btnLogin_Click(object sender, EventArgs e)
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

            if (_service == null)
            {
                var strAddress = string.Format("http://{0}:{1}/ClientBonusService/", _params.HostName, _params.Port);
                _service = new ClientBonusServiceClient("BasicHttpBinding_IClientBonusService", strAddress);
            }

            RoleTypes roleType = cbRole.SelectedIndex == 0 ? RoleTypes.Payin : RoleTypes.Payout;

            ResultOfLoginInfoxdEytY2q result;

            try
            {
                result = await _service.LoginAsync(teLogin.Text, tePass.Text, roleType);
            }
            catch (EndpointNotFoundException)
            {
                result = new ResultOfLoginInfoxdEytY2q
                {
                    IsSucssied = false,
                    Message = "Не удалось подключиться к серверу. Сервер не найден.\nПроверьте настройки подлючения"
                };
            }
            catch (Exception exception)
            {
                result = new ResultOfLoginInfoxdEytY2q
                {
                    IsSucssied = false,
                    Message = string.Format("Не удалось подключиться к серверу.\nОшибка: {0}", exception.Message)
                };
            }

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

            if (chSaveUser.Checked)
            {
                _params.UserName = teLogin.Text;
                _params.RoleType = roleType;
                _params.Save();
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

        private void btnNetSave_Click(object sender, EventArgs e)
        {
            try
            {
                _params.HostName = teIp.Text;
                _params.Port = int.Parse(tePort.Text);
                var strAddress = string.Format("http://{0}:{1}/ClientBonusService/", _params.HostName, _params.Port);
                _service = new ClientBonusServiceClient("BasicHttpBinding_IClientBonusService", strAddress);

                _params.Save();

                MessageBox.Show(
                    this,
                    Resources.LoginForm_ConnectionParamsAreSaved,
                    Resources.LoginForm_MessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    Resources.LoginForm_btnNetSave_BadData,
                    Resources.LoginForm_MessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _params = ClientParams.Load();
            }
        }

        internal IClientBonusService GetService()
        {
            return _service;
        }

        private void btnNetCancel_Click(object sender, EventArgs e)
        {
            teIp.Text = _params.HostName;
            tePort.Text = _params.Port.ToString();
        }

        private void tePass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnLogin_Click(this, e);
            }
        }
    }
}
