using Rider.Application.Gateways;

namespace Rider.Infrastructure.Gateways;

public class MailerGatewayFake : IMailerGateway
{
    public void Send(string recipient, string subject, string message)
    {
        Console.WriteLine($"Sending email to {recipient}: {message}");
    }
}