using Domain.Common.Enum;

namespace Application.Common.Behaviors;

public class OperationResult
{
    protected OperationResult(OperationStatus status, List<string> messages, int rows)
    {
        Status = status;
        Messages = messages ?? new List<string>();
        AffectedRows = rows;
    }

    public OperationStatus Status { get; init; }
    public bool Succeeded => Status == OperationStatus.Success;
    public List<string> Messages { get; init; }
    public int AffectedRows { get; set; }

    public static OperationResult Success(List<string> messages, int rows = 0)
        => new(OperationStatus.Success, messages, rows);

    public static OperationResult NoChanges(List<string> messages, int rows = 0)
        => new(OperationStatus.NoChanges, messages, rows);

    public static OperationResult Failure(List<string> messages, int rows = 0)
        => new(OperationStatus.Failure, messages, rows);
}

public class OperationResult<T> : OperationResult
{
    private OperationResult(OperationStatus status, T? data, List<string> messages, int rows = 0)
        : base(status, messages, rows)
    {
        Data = data;
    }

    public T? Data { get; }

    public static OperationResult<T> Success(T data, List<string> messages, int rows = 0)
        => new(OperationStatus.Success, data, messages, rows);

    public static OperationResult<T> NoChanges(T? data, List<string> messages, int rows = 0)
        => new(OperationStatus.NoChanges, data, messages, rows);

    public static OperationResult<T> Failure(List<string> messages, T? data = default, int rows = 0)
        => new(OperationStatus.Failure, data, messages, rows);

}
