using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFPlugin.DataContract;

namespace WCFPlugin.Contract
{
    [ServiceContract]
    public interface IClientBonusService
    {
        [OperationContract]
        Result<LoginInfo> Login(string login, string pass, RoleTypes roleType);

		[OperationContract]
        void Logout(Guid sessionGuid);

        /// <summary>
        /// Поиск клиента по телефону
        /// </summary>
        [OperationContract]
        Result<ClientInfo> GetClientByPhone(string phone, Guid sessionGuid); 

        /// <summary>
        /// Создание нового клиента
        /// </summary>
        [OperationContract]
        Result<ClientInfo> CreateNewClient(string phone, string name, Guid sessionGuid);

        /// <summary>
        /// Списание указанной суммы со счета клиента
        /// </summary>
        [OperationContract]
        Result<ClientInfo> PayoutFromAccount(long clientId, int operationCode, Guid sessionGuid);

        /// <summary>
        /// Зачисление указанной суммы на счет клиента.
        /// </summary>
        [OperationContract]
        Result<ClientInfo> PayInToAccount(long clientId, int operationCode, Guid sessionGuid);

        /// <summary>
        /// Отправить код операции зачисления клиенту по указанному телефону
        /// </summary>
        [OperationContract]
        Result SendPayinCode(long clientId, string phone, double summ, SendMethod sendMethod, Guid sessionGuid);

        /// <summary>
        /// Отправить код операции списания со счета клиенту по указанному телефону
        /// </summary>
        [OperationContract]
        Result SendPayoutCode(long clientId, string phone, double summ, SendMethod sendMethod, Guid sessionGuid);

        [OperationContract]
        Result<IEnumerable<UserInfo>> GetUsersList(Guid sessionGuid);

        [OperationContract]
        Result CreateUser(UserParam user, Guid sessionGuid);

        [OperationContract]
        Result<UserParam> GetUser(string userLogin, Guid sessionGuid);

        [OperationContract]
        Result UpdateUser(UserParam user, Guid sessionGuid);

        [OperationContract]
        Result RemoveUser(string userLogin, Guid sessionGuid);
        
        [OperationContract]
        Result ChangeAdminPass(string newPass, Guid sessionGuid);
    }

    public enum SendMethod
    {
        Call,
        SMS,
        Both
    }
}
