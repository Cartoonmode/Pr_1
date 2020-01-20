using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace practice_1
{
    class User
    {
        private Socket Socket { get; set; }
        private Server Server;
        private int Id { get; set; }
        private int Tasks = 0;

        public User(Server Server, Socket Socket, int Id)
        {
            this.Socket = Socket;
            this.Server = Server;
            this.Id = Id;
        }

        public void Processing()
        {
            try
            {
                while (Server.Work)
                {       
                    
                    int number = Reading();
                    Tasks++;
                    Task task = new Task(() => Working(number));
                    task.Start();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("User " + Id +" Error: " + e.Message);
            }

            Exit();
        }

        private int Reading()
        {
            byte[] receiveBuffer = new byte[4];
            Socket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
            Console.WriteLine("User " + Id + " получен запрос.");
            return BitConverter.ToInt32(receiveBuffer,0);
        }

        private void Working(int number)
        {
            Thread.Sleep(7000);
            if (Tasks>1)
            {
                Console.WriteLine("Операция прервана");
                Answer(BitConverter.GetBytes(-1));
                Tasks--;
                return;
            }
            Tasks--;
            Answer(BitConverter.GetBytes(number + 100));
        }

        private void Answer(byte[] answer)
        {
            Socket.Send(answer);
            Console.WriteLine("User " + Id + " отправлен ответ на запрос.");
        }

        private void Exit()
        {
            Socket.Close();
        }
    }
}
