using System;
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

            switch (_operationType)
            {
                case OperationType.PayIn:
                    _operationTitle = "Зачисление на счет";
                    break;
                case OperationType.PayOut:
                    _operationTitle = "Списание со счета";
                    break;
                default:
                    _operationTitle = "Не известный тип операции";
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!_codeWasSent)
            {
                Result result;
                switch (_operationType)
                {
                    case OperationType.PayIn:
                        result = _service.SendPayinCode(ClientInfo.Id, tePhone.Text, (double) teSumm.Value,
                            SendMethod.SMS, _loginInfo);
                        break;
                    case OperationType.PayOut:
                        result = _service.SendPayoutCode(ClientInfo.Id, tePhone.Text, (double)teSumm.Value,
                            SendMethod.SMS, _loginInfo);
                        break;
                    default:
                        result = new Result
                        {
                            IsSucssied = false,
                            Message = string.Format("Тип операции \"{0}\" не определен", _operationType)
                        };
                        break;
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

                _codeWasSent = true;
                tePhone.Enabled = false;
                teSumm.Enabled = false;
                lblCode.Visible = true;
                teCode.Visible = true;
                btnOk.Text = Resources.BonusOperationsForm_btnOk_Do;
            }
            else
            {
                ResultOfClientInfoxdEytY2q result;
                switch (_operationType)
                {
                    case OperationType.PayIn:
                        result = _service.PayInToAccount(ClientInfo.Id, int.Parse(teCode.Text), _loginInfo);
                        break;
                    case OperationType.PayOut:
                        result = _service.PayoutFromAccount(ClientInfo.Id, int.Parse(teCode.Text), _loginInfo);
                        break;
                    default:
                        result = new ResultOfClientInfoxdEytY2q
                        {
                            IsSucssied = false,
                            Message = string.Format("Тип операции \"{0}\" не определен", _operationType)
                        };
                        break;
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
                Close();
            }
        }
    }

    public enum OperationType
    {
        PayIn,
        PayOut
    }
}
