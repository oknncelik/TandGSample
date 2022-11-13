public class ErrorResult : IResult
{
    public string ResultType => "Error";
    public int Code { get; set; } = -1;
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; } = "Error !";

    public ErrorResult()
    {

    }

    public ErrorResult(string message)
    {
        this.Message = message;
    }

    public ErrorResult(int code)
    {
        this.Code = code;
    }

    public ErrorResult(int code, string message) : this(code)
    {
        this.Message = message;
    }
}
