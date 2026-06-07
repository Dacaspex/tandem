namespace Tandem.Api.Common;

public class ListResult<TValue, TError> : Result<List<TValue>, TError>
{
    public ListResult(bool isSuccess, List<TValue>? value, TError? error) : base(isSuccess, value, error)
    {
    }

    public new static ListResult<TValue, TError> Success(List<TValue> value)
        => new(isSuccess: true, value, error: default);

    public new static ListResult<TValue, TError> Fail(TError error)
        => new(isSuccess: false, null, error);
}