using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;


namespace Bulkybook.Utility;
public class TimerService : IHostedService
{
    private readonly IEmailSender _emailSender;
    private Timer _timer;

    public TimerService(IEmailSender emailSender)
    {
     //   _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        _emailSender = emailSender;
    }

    private void DoWork(object state)
    {
        _emailSender.SendEmailAsync("olasubomiodekunle@gmail.com", "testNotification", "<p>New not Created</p>");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}