using System;
using System.Globalization;
using System.Windows.Forms;
using CxTaxiSlimClient.CxTaxiService;
using CxTaxiSlimClient.Properties;

namespace CxTaxiSlimClient
{

    public partial class MainForm : Form
    {
        private LoginInfo _loginInfo;
        private IClientBonusService _service;
        private ClientInfo _clientInfo;

        public MainForm()
        {
            var host = "localhost";
            var port = 23444;
            var strAddress = string.Format("http://{0}:{1}/ClientBonusService/", host, port);
            _service = new ClientBonusServiceClient("BasicHttpBinding_IClientBonusService", strAddress);
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            if (_loginInfo == null)
            {
                Login();
            }
            base.OnShown(e);

        }

        private void Login()
        {
            using (var loginForm = new LoginForm(_service))
            {
                if (loginForm.ShowDialog(this) == DialogResult.OK)
                {
                    _loginInfo = loginForm.GetLoginInfo();
                }
            }

            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (_loginInfo != null)
            {
                Text = Resources.MainForm_Title_User + _loginInfo.UserName;
                tsRelogin.Text = Resources.MainForm_Relogin;
            }
            else
            {
                Text = Resources.MainForm_NotLogined;
                tsRelogin.Text = Resources.MainForm_Login;
            }
        }

        private void tsSearchBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tsPhoneEdit.Text))
            {
                MessageBox.Show(
                    this,
                    Resources.MainForm_PhoneCantBeEmpty,
                    Resources.MainForm_SearchClient,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_loginInfo == null)
            {
                MessageBox.Show(
                    this,
                    Resources.MainForm_MustBeLogined,
                    Resources.MainForm_SearchClient,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            var result = _service.GetClientByPhone(tsPhoneEdit.Text, _loginInfo);
            if (result.IsSucssied)
            {
                _clientInfo = result.Data;
            }
            else
            {
                _clientInfo = null;
                MessageBox.Show(
                    this,
                    result.Message,
                    Resources.MainForm_SearchClient,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (_clientInfo == null)
            {
                teName.Text = null;
                teBirthday.Text = null;
                teBillingNumber.Text = null;
                teBalance.Text = null;
            }
            else
            {
                teName.Text = _clientInfo.Name;
                teBirthday.Text = _clientInfo.Birthday.HasValue
                    ? _clientInfo.Birthday.Value.ToShortDateString()
                    : "Не указано";
                teBillingNumber.Text = _clientInfo.BillingAccountNumber;
                teBalance.Text = _clientInfo.Balance.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void tsRelogin_Click(object sender, EventArgs e)
        {
            if (_loginInfo != null)
            {
                _service.Logout(_loginInfo);
                _loginInfo = null;
            }

            Login();
        }

        private void tsPayInBtn_Click(object sender, EventArgs e)
        {
            using (var operationForm = new BonusOperationsForm(_clientInfo, _service, OperationType.PayIn, _loginInfo))
            {
                if (operationForm.ShowDialog(this) == DialogResult.OK)
                {
                    _clientInfo = operationForm.ClientInfo;
                    UpdateInfo();
                }
            }
        }

        private void tsPayoutBtn_Click(object sender, EventArgs e)
        {
            using (var operationForm = new BonusOperationsForm(_clientInfo, _service, OperationType.PayOut, _loginInfo))
            {
                if (operationForm.ShowDialog(this) == DialogResult.OK)
                {
                    _clientInfo = operationForm.ClientInfo;
                    UpdateInfo();
                }
            }
        }
    }
}
