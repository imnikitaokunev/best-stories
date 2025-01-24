namespace BestStories.HackerRankApi;

public class Response<TResult>
{
    private Response(TResult result)
    {
        Result = result ?? throw new ArgumentNullException(nameof(result));
    }
    
    private Response(string error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error));
    }
    
    public bool IsSuccess => Result != null;
    public TResult Result { get; set; }
    public string Error { get; set; }
    
    public static Response<TResult> Success(TResult result) => new(result);
    public static Response<TResult> Failed(string error) => new(error);
}