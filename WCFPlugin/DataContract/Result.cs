using System.Runtime.Serialization;

namespace WCFPlugin.DataContract
{
    [DataContract]
    public class Result
    {
        public const string LoginErrorMsg = "Не верный логин или пароль.";
        public const string NotInitedMsg = "Сервис не инициализирован.";
        public const string DoesntLoginedMsg = "Пользователь не залогирован в системе.";
        public const string AccsessDenyMsg = "Вы не имеете прав на данную операцию.";
        public const string CodeErrorMsg = "Не верный код операции";

        public Result(bool isSucssied)
        {
            IsSucssied = isSucssied;
            if (!IsSucssied)
                ErrorType = ErrorTypes.UnkownError;
        }

        [DataMember]
        public bool IsSucssied { get; set; }

        [DataMember]
        public ErrorTypes? ErrorType { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    public enum ErrorTypes
    {
        DoesntLogined,
        AccsessDeny,
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

        [DataMember]
        public TData Data { get; set; }

        public static Result<TData> NotInitialized()
        {
            return new Result<TData>(false)
            {
                Message = NotInitedMsg,
                ErrorType = ErrorTypes.NotInitialize
            };
        }

        public static Result<TData> LoginOrPassIsWrong()
        {
            return new Result<TData>(false)
            {
                Message = LoginErrorMsg,
                ErrorType = ErrorTypes.LoginOrPassIsWrong
            };
        }

        public static Result<TData> DoesntLogined()
        {
            return new Result<TData>(false)
            {
                Message = DoesntLoginedMsg,
                ErrorType = ErrorTypes.DoesntLogined
            };
        }

        public static Result<TData> AccsessDeny()
        {
            return new Result<TData>(false)
            {
                Message = DoesntLoginedMsg,
                ErrorType = ErrorTypes.DoesntLogined
            };
        }
    }
}
