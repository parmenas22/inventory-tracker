namespace api.Services.Auth
{
    public interface IEmailSender
    {
        Task SendLoginCodeAsync(string email, string code, CancellationToken cancellationToken = default);
        Task SendInvitationAsync(string email, Uri invitationUri, string roleName, CancellationToken cancellationToken = default);
    }
}
