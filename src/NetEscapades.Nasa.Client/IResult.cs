namespace NetEscapades.Nasa
{
    public interface IResult<T> where T: class
    {
        bool IsSuccess { get; }
        T Data { get; }
        string ErrorMessage { get; }
    }
}
