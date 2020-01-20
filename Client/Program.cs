using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(12923, "127.0.0.1");
            client.Start();

            while (true)
            {
                string command;
                command = Console.ReadLine();
                if (command == "Выход")
                {
                    client.Stop();
                    break;
                }
            }
        }
    }
}
