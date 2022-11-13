public interface IResult
{
    public string ResultType { get; }
    int Code { get; set; }
    bool IsSuccess { get; set; }
    string Message { get; set; }
}
