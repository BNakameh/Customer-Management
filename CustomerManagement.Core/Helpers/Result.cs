namespace CustomerManagement.Core.Helpers;
public sealed class Result<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public List<string> Errors { get; init; } = [];

    public static Result<T> Ok(T data) =>
        new() { Success = true, Data = data };

    public static Result<T> Fail(params string[] errors) =>
        new() { Success = false, Errors = errors.ToList() };

    public static Result<T> Fail(List<string> errors) =>
        new() { Success = false, Errors = errors };
}