namespace Tandem.Api.Common;

public class EmptyResult<TError>
{
    public EmptyResult(bool isSuccess, TError? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public TError? Error { get; }

    public static EmptyResult<TError> Success() => new(isSuccess: true, default);

    public static EmptyResult<TError> Fail(TError error) => new(isSuccess: false, error);
}