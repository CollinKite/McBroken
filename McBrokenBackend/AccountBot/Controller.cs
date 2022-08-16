using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountBot
{
    public class Controller
    {
        public async Task Start()
        {
            Account account = new Account();
            await Account.RegisterAccount("kingbob", account.DeviceID);


            //Email email = new("code1");
            //email.GetInboxAsync().Wait();
            //email.VerifyEmail().Wait();
        }
    }
}
