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
        public void Start()
        {
            Account account = new Account();
            Console.WriteLine(account.DeviceID);
        }
    }
}
