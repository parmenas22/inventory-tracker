namespace api.Services.Auth
{
    public interface IEmailSender
    {
        Task SendLoginCodeAsync(string email, string code, CancellationToken cancellationToken = default);
    }
}
