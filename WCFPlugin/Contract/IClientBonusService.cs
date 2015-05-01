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
        void Logout(LoginInfo loginInfo);

        /// <summary>
        /// Поиск клиента по телефону
        /// </summary>
        [OperationContract]
		Result<ClientInfo> GetClientByPhone(string phone, LoginInfo loginInfo); 

        /// <summary>
        /// Создание нового клиента
        /// </summary>
        [OperationContract]
        Result<ClientInfo> CreateNewClient(string phone, string name, LoginInfo loginInfo);

        /// <summary>
        /// Списание указанной суммы со счета клиента
        /// </summary>
        [OperationContract]
        Result<ClientInfo> PayoutFromAccount(long clientId, int operationCode, LoginInfo loginInfo);

        /// <summary>
        /// Зачисление указанной суммы на счет клиента.
        /// </summary>
        [OperationContract]
        Result<ClientInfo> PayInToAccount(long clientId, int operationCode, LoginInfo loginInfo);

        /// <summary>
        /// Отправить код операции зачисления клиенту по указанному телефону
        /// </summary>
        [OperationContract]
        Result SendPayinCode(long clientId, string phone, double summ, SendMethod sendMethod, LoginInfo loginInfo);

        /// <summary>
        /// Отправить код операции списания со счета клиенту по указанному телефону
        /// </summary>
        [OperationContract]
        Result SendPayoutCode(long clientId, string phone, double summ, SendMethod sendMethod, LoginInfo loginInfo);
    }

    public enum SendMethod
    {
        Call,
        // ReSharper disable once InconsistentNaming
        SMS,
        Both
    }
}
