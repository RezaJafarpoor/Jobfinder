namespace Jobfinder.Domain.Commons;

public class Response<TResult>
{
    public bool IsSuccess { get;}
    public List<string> Errors { get; }
    public TResult? Data { get; } 

    private Response(bool isSuccess, List<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
    private Response(bool isSuccess, List<string> errors, TResult response)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Data = response;
    }
    

    public static Response<TResult> Success() => new(true, []);
    public static Response<TResult> Success(TResult result) => new(true,[], result)
;    public static Response<TResult> Failure(List<string> errors) => new(false, errors);
    public static Response<TResult> Failure(string errors) => new(false, [errors]);


}