public class SuccessResult<T> : IResultModel<T>
{
    public string ResultType => "Success";
    public bool IsSuccess { get; set; } = true;
    public int Code { get; set; } = 0;
    public string Message { get; set; } = "Success";
    public int Count { get; set; }
    public T? Result { get; set; }

    public SuccessResult()
    {

    }

    public SuccessResult(T? Result)
    {
        this.Result = Result;
    }

    public SuccessResult(T? Result, int count) : this(Result)
    {
        this.Count = count;
    }

    public SuccessResult(T? Result, string message) : this(Result)
    {
        this.Message = message;
    }

    public SuccessResult(T? result, string message, int count) : this(result, message)
    {
        this.Count = count;
    }
}
