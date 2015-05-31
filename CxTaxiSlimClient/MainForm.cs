using System;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_loginInfo != null && _service != null)
            {
                try
                {
                    _service.Logout(_loginInfo.SessionGuid);
                }
                catch
                {
                    
                }
                
            }
            base.OnClosing(e);
        }

        private void Login()
        {
            using (var loginForm = new LoginForm(_service))
            {
                if (loginForm.ShowDialog(this) == DialogResult.OK)
                {
                    _loginInfo = loginForm.GetLoginInfo();
                    _service = loginForm.GetService();
                    switch (_loginInfo.RoleType)
                    {
                        case RoleTypes.Payin:
                            tsPayInBtn.Enabled = true;
                            tsPayoutBtn.Enabled = false;
                            tsUserControl.Enabled = false;
                            tsUserControl.Visible = false;
                            break;
                        case RoleTypes.Payout:
                            tsPayInBtn.Enabled = false;
                            tsPayoutBtn.Enabled = true;
                            tsUserControl.Enabled = false;
                            tsUserControl.Visible = false;
                            break;
                        case RoleTypes.Admin:
                            tsPayInBtn.Enabled = true;
                            tsPayoutBtn.Enabled = true;
                            tsUserControl.Enabled = true;
                            tsUserControl.Visible = true;
                            break;
                        default:
                            tsPayInBtn.Enabled = false;
                            tsPayoutBtn.Enabled = false;
                            tsUserControl.Enabled = false;
                            tsUserControl.Visible = false;
                            break;
                    }
                }
                else
                {
                    _loginInfo = null;
                    _clientInfo = null;
                    tsPayInBtn.Enabled = false;
                    tsPayoutBtn.Enabled = false;
                    tsUserControl.Enabled = false;
                    tsUserControl.Visible = false;
                }
            }

            UpdateTitle();
            UpdateInfo();
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
            Search();
        }

        private void Search()
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
                return;
            }

            ResultOfClientInfoxdEytY2q result;
            try
            {
                result = _service.GetClientByPhone(tsPhoneEdit.Text, _loginInfo.SessionGuid);
            }
            catch (EndpointNotFoundException)
            {
                result = new ResultOfClientInfoxdEytY2q
                {
                    IsSucssied = false,
                    Message = "Не удалось подключиться к серверу. Сервер не найден.\nПроверьте настройки подлючения"
                };
            }
            catch (Exception exception)
            {
                result = new ResultOfClientInfoxdEytY2q
                {
                    IsSucssied = false,
                    Message = string.Format("Не удалось выполнить операцию.\nОшибка: {0}", exception.Message)
                };
            }
            
            if (result.IsSucssied)
            {
                _clientInfo = result.Data;
            }
            else
            {
                _clientInfo = null;
                if (_loginInfo.RoleType == RoleTypes.Payin && result.ErrorType == ErrorTypes.ClientNotFound)
                {
                    var messageResult = MessageBox.Show(
                        this,
                        string.Format("{0}\n{1}", result.Message, Resources.MainForm_AddNewClient),
                        Resources.MainForm_SearchClient,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (messageResult == DialogResult.Yes)
                        CreateNewClient();
                }
                else
                {
                    MessageBox.Show(
                        this,
                        result.Message,
                        Resources.MainForm_SearchClient,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            UpdateInfo();
        }

        private void CreateNewClient()
        {
            using (var createForm = new NewClientForm(tsPhoneEdit.Text))
            {
                if (createForm.ShowDialog(this) != DialogResult.OK)
                    return;

                ResultOfClientInfoxdEytY2q result;
                try
                {
                    result = _service.CreateNewClient(createForm.GetPhone(), createForm.GetName(), _loginInfo.SessionGuid);
                }
                catch (EndpointNotFoundException)
                {
                    result = new ResultOfClientInfoxdEytY2q
                    {
                        IsSucssied = false,
                        Message = "Не удалось подключиться к серверу. Сервер не найден.\nПроверьте настройки подлючения"
                    };
                }
                catch (Exception exception)
                {
                    result = new ResultOfClientInfoxdEytY2q
                    {
                        IsSucssied = false,
                        Message = string.Format("Не удалось выполнить операцию.\nОшибка: {0}", exception.Message)
                    };
                }

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
                        Resources.NewClientForm_Title_NewClient,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                UpdateInfo();
            }
        }

        private void UpdateInfo()
        {
            if (_clientInfo == null)
            {
                teName.Text = null;
                teBirthday.Text = null;
                teBillingNumber.Text = null;
                teBalance.Text = null;
                teDiscount.Text = null;
                teService.Text = null;
                teOrdersDone.Text = null;
                teOrdersCanceled.Text = null;
            }
            else
            {
                teName.Text = _clientInfo.Name;
                teBirthday.Text = _clientInfo.Birthday.HasValue
                    ? _clientInfo.Birthday.Value.ToShortDateString()
                    : "Не указано";
                teBillingNumber.Text = _clientInfo.BillingAccountNumber;
                teBalance.Text = _clientInfo.Balance.ToString(CultureInfo.InvariantCulture);

                teDiscount.Text = _clientInfo.DefaultDiscountCardNumber;
                teService.Text = _clientInfo.DefaultServiceName;
                teOrdersDone.Text = (_clientInfo.OrdersCounter ?? 0).ToString();
                teOrdersCanceled.Text = (_clientInfo.OrdersCanceled ?? 0).ToString();
            }

            if (_loginInfo != null && _loginInfo.RoleType == RoleTypes.Payout)
            {
                teBalance.Visible = true;
                lblBalance.Visible = true;
            }
            else
            {
                teBalance.Visible = false;
                lblBalance.Visible = false;
            }
        }

        private void tsRelogin_Click(object sender, EventArgs e)
        {
            if (_loginInfo != null)
            {
                try
                {
                    _service.Logout(_loginInfo.SessionGuid);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }

                _loginInfo = null;
            }

            Login();
        }

        private void tsPayInBtn_Click(object sender, EventArgs e)
        {
            if (_clientInfo == null)
                return;

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
            if (_clientInfo == null)
                return;

            using (var operationForm = new BonusOperationsForm(_clientInfo, _service, OperationType.PayOut, _loginInfo))
            {
                if (operationForm.ShowDialog(this) == DialogResult.OK)
                {
                    _clientInfo = operationForm.ClientInfo;
                    UpdateInfo();
                }
            }
        }

        private void tsPhoneEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != 8)
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+' && !string.IsNullOrWhiteSpace(tsPhoneEdit.Text))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                e.Handled = true;
                Search();
            }
        }

        private void tsUserControl_Click(object sender, EventArgs e)
        {
            using (var userControlForm = new UserControlForm(_service, _loginInfo))
            {
                userControlForm.ShowDialog(this);
            }
        }
    }
}
