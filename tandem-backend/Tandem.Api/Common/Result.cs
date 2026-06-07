namespace Tandem.Api.Common;

public class Result<TValue, TError> : EmptyResult<TError>
{
    public Result(bool isSuccess, TValue? value, TError? error) : base(isSuccess, error)
    {
        Value = value;
    }

    public TValue? Value { get; }

    public static Result<TValue, TError> Success(TValue value)
        => new(isSuccess: true, value, error: default);

    public new static Result<TValue, TError> Fail(TError error)
        => new(isSuccess: false, default, error);
}

public class Result<TValue> : Result<TValue, string>
{
    public Result(bool isSuccess, TValue? value, string? error) : base(isSuccess, value, error)
    {
    }

    public new static Result<TValue> Success(TValue value)
        => new(true, value, null);
}