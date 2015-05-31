using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CxTaxiSlimClient.CxTaxiService;
using CxTaxiSlimClient.Properties;

namespace CxTaxiSlimClient
{
    public partial class BonusOperationsForm : Form
    {
        public ClientInfo ClientInfo { get; private set; }

        private readonly IClientBonusService _service;
        private readonly OperationType _operationType;
        private readonly LoginInfo _loginInfo;

        private readonly string _operationTitle;

        private bool _codeWasSent;

        private delegate void ResendEnable();
        private readonly ResendEnable _resendEnable;

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public BonusOperationsForm(ClientInfo clientInfo, IClientBonusService service, OperationType operationType, LoginInfo loginInfo)
        {
            _loginInfo = loginInfo;
            _operationType = operationType;
            ClientInfo = clientInfo;
            _service = service;
            _codeWasSent = false;
            InitializeComponent();

            lblCode.Visible = false;
            teCode.Visible = false;

            teName.Text = ClientInfo.Name;
            tePhone.Text = ClientInfo.Phone;

            btnSendAgain.Visible = false;

            switch (_operationType)
            {
                case OperationType.PayIn:
                    teSumm.Visible = true;
                    lblSumm.Visible = true;
                    teOpSumm.Enabled = false;
                    teOpSumm.ReadOnly = true;
                    lblOperationSumm.Text = "Зачислить";
                    _operationTitle = "Зачисление на счет";
                    break;
                case OperationType.PayOut:
                    teSumm.Visible = false;
                    lblSumm.Visible = false;
                    lblOperationSumm.Text = "Списать";
                    teOpSumm.Enabled = true;
                    teOpSumm.ReadOnly = false;
                    teOpSumm.MaxValue = ClientInfo.Balance;
                    _operationTitle = "Списание со счета";
                    break;
                default:
                    _operationTitle = "Не известный тип операции";
                    break;
            }

            this.Text = _operationTitle;

            _resendEnable = 
                () =>
                {
                    btnSendAgain.Visible = true;
                    btnSendAgain.Enabled = true;
                };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            _tokenSource.Cancel();
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!_codeWasSent)
            {
                var result = SendCode();

                if (!result.IsSucssied)
                {
                    MessageBox.Show(
                        this,
                        result.Message,
                        _operationTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                _codeWasSent = true;
                tePhone.Enabled = false;
                teOpSumm.Enabled = false;
                lblCode.Visible = true;
                teCode.Visible = true;
                btnOk.Text = Resources.BonusOperationsForm_btnOk_Do;

                TaskToEnableResendBtn();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(teCode.Text))
                    return;

                ResultOfClientInfoxdEytY2q result;
                try
                {
                    switch (_operationType)
                    {
                        case OperationType.PayIn:
                            result = _service.PayInToAccount(ClientInfo.Id, int.Parse(teCode.Text), _loginInfo.SessionGuid);
                            break;
                        case OperationType.PayOut:
                            result = _service.PayoutFromAccount(ClientInfo.Id, int.Parse(teCode.Text), _loginInfo.SessionGuid);
                            break;
                        default:
                            result = new ResultOfClientInfoxdEytY2q
                            {
                                IsSucssied = false,
                                Message = string.Format("Тип операции \"{0}\" не определен", _operationType)
                            };
                            break;
                    }
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
                

                if (!result.IsSucssied)
                {
                    MessageBox.Show(
                        this,
                        result.Message,
                        _operationTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                ClientInfo = result.Data;
                DialogResult = DialogResult.OK;
                _tokenSource.Cancel();

                MessageBox.Show(
                    this,
                    "Операция выполнена успешно.",
                    _operationTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
        }

        private void TaskToEnableResendBtn()
        {
            Task.Delay(60 * 1000, _tokenSource.Token).ContinueWith(
                task =>
                {
                    if (!task.IsCanceled)
                        Invoke(_resendEnable);
                },
                _tokenSource.Token);
        }

        private Result SendCode()
        {
            Result result;
            var sendMethod = SendMethod.SMS;
            if (rbCall.Checked)
                sendMethod = SendMethod.Call;

            if (rbSMS.Checked)
                sendMethod = SendMethod.SMS;

            try
            {
                switch (_operationType)
                {
                    case OperationType.PayIn:
                        result = _service.SendPayinCode(ClientInfo.Id, tePhone.Text, double.Parse(teOpSumm.Text),
                            sendMethod, _loginInfo.SessionGuid);
                        break;
                    case OperationType.PayOut:
                        result = _service.SendPayoutCode(ClientInfo.Id, tePhone.Text, double.Parse(teOpSumm.Text),
                            sendMethod, _loginInfo.SessionGuid);
                        break;
                    default:
                        result = new Result
                        {
                            IsSucssied = false,
                            Message = string.Format("Тип операции \"{0}\" не определен", _operationType)
                        };
                        break;
                }
            }
            catch (EndpointNotFoundException)
            {
                result = new Result
                {
                    IsSucssied = false,
                    Message = "Не удалось подключиться к серверу. Сервер не найден.\nПроверьте настройки подлючения"
                };
            }
            catch (Exception exception)
            {
                result = new Result
                {
                    IsSucssied = false,
                    Message = string.Format("Не удалось выполнить операцию.\nОшибка: {0}", exception.Message)
                };
            }

            return result;
        }

        private void rbSMS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSMS.Checked)
                rbCall.Checked = false;
        }

        private void rbCall_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCall.Checked)
                rbSMS.Checked = false;
        }

        private void btnSendAgain_Click(object sender, EventArgs e)
        {
            var result = SendCode();

            if (!result.IsSucssied)
            {
                MessageBox.Show(
                    this,
                    result.Message,
                    _operationTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            btnSendAgain.Enabled = false;
            TaskToEnableResendBtn();
        }

        private void teSumm_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teSumm.Text))
            {
                teOpSumm.Text = (double.Parse(teSumm.Text) * _loginInfo.Rate).ToString("F2");
            }
            else
            {
                teOpSumm.Text = "0.0";
            }
        }
    }

    public enum OperationType
    {
        PayIn,
        PayOut
    }
}
