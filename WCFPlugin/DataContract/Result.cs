using System.Runtime.Serialization;

namespace WCFPlugin.DataContract
{
    [DataContract]
    public class Result
    {
        public const string LoginErrorMsg = "Не верный логин или пароль.";
        public const string NotInitedMsg = "Сервис не инициализирован.";
        public const string DoesntLoginedMsg = "Пользователь не залогирован в системе.";
        public const string CodeErrorMsg = "Не верный код операции";
        public const string AccessDenyMsg = "Функция не доступна для данного пользователя";

        public Result(bool isSucssied)
        {
            IsSucssied = isSucssied;
            if (!IsSucssied)
                ErrorType = ErrorTypes.UnkownError;
        }

        public Result(Result other)
        {
            IsSucssied = other.IsSucssied;
            ErrorType = other.ErrorType;
            Message = other.Message;
        }

        [DataMember]
        public bool IsSucssied { get; set; }

        [DataMember]
        public ErrorTypes? ErrorType { get; set; }

        [DataMember]
        public string Message { get; set; }

        public static Result NotInitialized()
        {
            return new Result(false)
            {
                Message = NotInitedMsg,
                ErrorType = ErrorTypes.NotInitialize
            };
        }

        public static Result LoginOrPassIsWrong()
        {
            return new Result(false)
            {
                Message = LoginErrorMsg,
                ErrorType = ErrorTypes.LoginOrPassIsWrong
            };
        }

        public static Result DoesntLogined()
        {
            return new Result(false)
            {
                Message = DoesntLoginedMsg,
                ErrorType = ErrorTypes.DoesntLogined
            };
        }

        public static Result AccsessDeny()
        {
            return new Result(false)
            {
                Message = AccessDenyMsg,
                ErrorType = ErrorTypes.LoginOrPassIsWrong
            };
        }
    }

    public enum ErrorTypes
    {
        DoesntLogined,
        ClientNotFound,
        NotInitialize,
        LoginOrPassIsWrong,
        UnkownError
    }

    [DataContract]
    public class Result<TData> : Result
        where TData : class
    {
        public Result(bool isSucssied) : base(isSucssied)
        {
        }

        public Result(TData data) : base(true)
        {
            Data = data;
        }

        public Result(Result other) : base(other)
        { 
        }

        [DataMember]
        public TData Data { get; set; }
    }
}
