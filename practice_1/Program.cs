using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(12923, "127.0.0.1");
            server.Start();

            while (true)
            {
                string command;
                command = Console.ReadLine();
                if(command == "Выход")
                {
                    server.Stop();
                    break;
                }

                if(command == "Перезапуск")
                {
                    server.Stop();
                    server.Start();
                }
            }         
        }
    }
}
