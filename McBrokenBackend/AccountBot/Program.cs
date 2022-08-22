using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Email email = new("test16");
            email.GetInboxAsync().Wait();
            email.VerifyEmail().Wait();

        }

        
        
    }
}