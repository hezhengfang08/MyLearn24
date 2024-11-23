using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Myself.SimpleEvent.Bus.Mail
{
    public class MyMailSender : ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        public MyMailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task DoSendAsync()
        {
            await _emailSender.SendAsync(
               "target@domain.com",     // target email address
               "Email subject",         // subject
               "This is email body..."  // email body
           );
        }
    }
}
