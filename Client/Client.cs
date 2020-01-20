using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        private IPEndPoint IpPoint { get; set; }
        public bool Work { get; set; }
        private Socket Socket { get; set; }

        public Client(int port, string address)
        {
            IpPoint = new IPEndPoint(IPAddress.Parse(address), port);          
        }

        public void Start()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Connect(IpPoint);
            Console.WriteLine("Соединение установленно.");
            Work = true;

            Task processingSend = new Task(ProcessingSend);
            processingSend.Start();

            Task processingReceive = new Task(ProcessingReceive);
            processingReceive.Start();
        }

        public void Stop()
        {
            Socket.Close();
            Work = false;
        }

        private void ProcessingSend()
        {
            try
            {
                while (Work)
                {
                    Socket.Send(BitConverter.GetBytes(new Random().Next(10, 99)));
                    Console.WriteLine("Запрос отправлен.");
                    Thread.Sleep(4500);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Work = false;
            }

        }

        private void ProcessingReceive()
        {
            try
            {
                while (Work)
                {
                    byte[] answer = new byte[4];
                    Socket.Receive(answer, answer.Length, SocketFlags.None);
                    if (BitConverter.ToInt32(answer, 0) == -1)
                    {
                        Console.WriteLine("Исполнение задачи отменено");
                    }
                    else
                    {
                        Console.WriteLine("Число увеличенное на 100: " + BitConverter.ToInt32(answer, 0));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Work = false;
            }
        }
    }
}
