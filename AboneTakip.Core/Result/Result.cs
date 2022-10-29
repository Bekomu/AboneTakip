using AboneTakip.Core.Enums;
using System;

namespace AboneTakip.API.Core.Result
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }
        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
        }
        public string Message { get; }

        public Exception Exception { get; }

        public ResultStatus ResultStatus { get; }
    }
}
