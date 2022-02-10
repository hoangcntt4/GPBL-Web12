namespace Misa.Amis.Core.Entities
{
  /// <summary>
  /// Dinh dang json tra ve khi co loi
  /// </summary>
  public class ApiError
  {
    /// <summary>
    /// Thong tin loi (User)
    /// </summary>
    public string UserMessage { get; set; }
    /// <summary>
    /// Thong tin loi (Developer)
    /// </summary>
    public string DevMessage { get; set; }
    /// <summary>
    /// Chi tiet loi
    /// </summary>
    public object? Data { get; set; }

    public ApiError(string userMessage, string devMessage, object? data)
    {
      UserMessage = userMessage;
      DevMessage = devMessage;
      Data = data;
    }
    public ApiError(string userMsg, Exception exc) : this(userMsg, exc.Message, exc.Data)
    {

    }
    public ApiError(Exception exc) : this(exc.Message, exc)
    {

    }
  }
}
