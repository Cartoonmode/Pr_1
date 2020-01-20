using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace practice_1
{
    class Server
    {
        private IPEndPoint IpPoint { get; set; }
        public bool Work { get; set; }
        private int Id { get; set; }

        public Server(int port, string address)
        {
            IpPoint = new IPEndPoint(IPAddress.Parse(address), port);
        }

        public void Start()
        {
            Work = true;

            Task listener = new Task(Listener);
            listener.Start();
        }

        public void Stop()
        {
            Work = false;
        }

        private void Listener()
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(IpPoint);
            listener.Listen(40);

            while (Work)
            {
                Id++;
                User user = new User(this, listener.Accept(), Id);

                Task userTask = new Task(user.Processing);
                userTask.Start();
            }

            listener.Close();
        }
    }

}
