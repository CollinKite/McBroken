using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Controller controller = new();
            await controller.Start();
        }

        
        
    }
}