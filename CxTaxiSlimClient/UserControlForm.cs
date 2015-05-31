using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CxTaxiSlimClient
{
    using CxTaxiService;
    using System.ServiceModel;

    public partial class UserControlForm : Form
    {
        private IClientBonusService _service;
        private LoginInfo _loginInfo;

        public UserControlForm(IClientBonusService service, LoginInfo loginInfo)
        {
            
            _service = service;
            _loginInfo = loginInfo;

            InitializeComponent();
            UpdateUsersGrid();
        }

        private void UpdateUsersGrid()
        {
            var users = _service.GetUsersList(_loginInfo.SessionGuid);
            gridUser.Rows.Clear();
            foreach (var item in users.Data)
            {
                gridUser.Rows.Add(new object[] { item.Login, item.Name });
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(teNewPass.Text))
            {
                MessageBox.Show(
                    this,
                    "Пароль не может быть пустым",
                    "Смена пароля",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!teNewPass.Text.Equals(teCheckPass.Text))
            {
                MessageBox.Show(
                    this,
                    "Пароли не совпадают.",
                    "Смена пароля",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            DoServiceCall(
                "Пароль был успешно изменен",
                () => { return _service.ChangeAdminPass(teNewPass.Text, _loginInfo.SessionGuid); });
            
        }

        private bool DoServiceCall<TResult>(string doneMsg, Func<TResult> callFunc) where TResult : Result
        {
            Result result;
            try
            {
                result = callFunc();
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

            if (!result.IsSucssied)
            {
                MessageBox.Show(
                    this,
                    result.Message,
                    "Управление пользователями",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (!string.IsNullOrWhiteSpace(doneMsg))
            {
                MessageBox.Show(
                    this,
                    doneMsg,
                    "Управление пользователями",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            return result.IsSucssied;
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            string userName;
            if (!SelectedUser(out userName))
                return;

            if (MessageBox.Show(
                    this,
                    string.Format("Удалить пользователя \"{0}\"", userName),
                    "Управление пользователями",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes)
            {
                if (DoServiceCall("Пользователь удален",
                    () => 
                    {
                        return _service.RemoveUser(userName, _loginInfo.SessionGuid);
                    }))
                {
                    UpdateUsersGrid();
                }
            }
        }

        private bool SelectedUser(out string userName)
        {
            var row = gridUser.CurrentRow;
            if (row == null)
            {
                MessageBox.Show(
                    this,
                    "Пользователь не выбран",
                    "Управление пользователями",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                userName = null;
                return false;
            }

            userName = row.Cells[0].Value as string;
            return true;
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            using(var userForm = new UserEditForm(null, true))
            {
                if (userForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    if (DoServiceCall("Пользователь успешно добавлен",
                        () =>
                        {
                            return _service.CreateUser(userForm.User, _loginInfo.SessionGuid);
                        }))
                    {
                        UpdateUsersGrid();
                    }
                }
            }
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            string userName;
            if (!SelectedUser(out userName))
                return;

            UserParam user = null;

            if (!DoServiceCall(null, 
                () => 
                {
                    var result = _service.GetUser(userName, _loginInfo.SessionGuid);
                    user = result.Data;
                    return result;
                }))
            {
                return;
            }

            using (var userForm = new UserEditForm(user, false))
            {
                if (userForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    if (DoServiceCall("Пользователь успешно изменен",
                        () =>
                        {
                            return _service.UpdateUser(userForm.User, _loginInfo.SessionGuid);
                        }))
                    {
                        UpdateUsersGrid();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
