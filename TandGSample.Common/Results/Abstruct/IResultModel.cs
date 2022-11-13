public interface IResultModel<T> : IResult
{
    T Result { get; set; }
}
