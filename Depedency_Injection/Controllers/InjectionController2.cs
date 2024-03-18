using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Depedency_Injection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InjectionController2 : ControllerBase
    {
        IMessageService _iMessageService;
        int counter;
        public InjectionController2(IMessageService iMessageService)
        {
            _iMessageService = iMessageService;
            // counter resets because constructor is called with every API call
            // Need to put it in a class to inject like with EmailService in InjectionController.cs
            counter = 0;
        }

        [HttpPost("NotifyThread")]
        public string NotifyThread()
        {
            counter++;
            Thread.Sleep(5000);
            return _iMessageService.SendMessage("InjectionController2 NotifyThread() test");
        }

        [HttpPost("NotifyTaskDelay")]
        public async Task<ActionResult<string>> NotifyTaskDelay([BindRequired][MaxLength(10)] string input)
        {
            counter++;
            await Task.Delay(5000);
            return _iMessageService.SendMessage(string.Format("InjectionController2 NotifyTaskDelay({0}) test", input));
        }
    }
}
