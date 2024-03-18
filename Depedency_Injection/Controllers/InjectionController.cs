using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Depedency_Injection.Controllers
{
    public interface IMessageService
    {
        string SendMessage(string message);
    }

    // Step 2: Implement the service
    public class EmailService : IMessageService
    {
        int emailCounter;   // counter resets in BasicInjection() below, but not with .AddSingleton() injections in InjectionController2.cs
        public EmailService()
        {
            emailCounter = 0;
        }
        public string SendMessage(string message)
        {
            emailCounter++;
            Console.WriteLine($"Sending email #{emailCounter}: {message}");
            return $"Sending email #{emailCounter}: {message}";
        }
    }

    // Step 3: Create a class that depends on the service
    public class NotificationService
    {
        private readonly IMessageService _messageService;

        // Step 4: Use constructor injection to inject the dependency
        public NotificationService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public string Notify(string message)
        {
            // Step 5: Use the injected dependency
            return _messageService.SendMessage(message);
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class InjectionController : ControllerBase
    {

        private readonly ILogger<InjectionController> _logger;

        public InjectionController(ILogger<InjectionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("BasicInjection", Name = "BasicInjectionName")]
        public string Get()
        {
            // Step 6: Setup dependency injection
            IMessageService emailService = new EmailService(); // In a real app, this would be resolved through the DI container

            // Step 7: Inject the dependency into the class
            NotificationService notificationService = new NotificationService(emailService);

            // Step 8: Use the class with injected dependency
            return notificationService.Notify("Hello, world!");
        }
    }
}
