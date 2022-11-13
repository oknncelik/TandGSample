public class WarningResult : IResult
{
    public string ResultType => "Warning";

    public int Code { get; set; } = 100;

    public bool IsSuccess { get; set; }

    public string Message { get; set; } = "Warning !";

    public WarningResult()
    {

    }

    public WarningResult(string message)
    {
        this.Message = message;
    }

    public WarningResult(int code)
    {
        this.Code = code;
    }

    public WarningResult(string message, int code) : this(code)
    {
        this.Message = message;
    }

    public WarningResult(int code, string message, bool isSuccess) : this(message, code)
    {
        this.IsSuccess = isSuccess;
    }
}
