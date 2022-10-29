using AboneTakip.Core.Enums;
using System;

namespace AboneTakip.API.Core.Result
{
    public interface IResult
    {
        string Message { get; }
        Exception Exception { get; }
        ResultStatus ResultStatus { get; }
    }
}
