namespace Rider.Application.Gateways;

public interface IMailerGateway
{
    void Send(string recipient, string subject, string message);
}
