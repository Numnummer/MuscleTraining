namespace Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;

/// <summary>
/// Ответ на запрос <see cref="SendResetPasswordCodeRequest"/>
/// </summary>
public class SendResetPasswordCodeResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result">Результат</param>
    public SendResetPasswordCodeResponse(string result)
    {
        Result = result;
    }

    /// <summary>
    /// Результат
    /// </summary>
    public string Result { get; set; }
}