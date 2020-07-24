using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "192.168.1.121";
            int port = 5001;
            Console.WriteLine("Input text to print: ");
            string InputValue = Console.ReadLine();
            Socket Soc = Connect(ip, port);
            SendReceiveTest1(Soc, InputValue);
            Disconnect(Soc, ip, port);
        }

        public static void Disconnect(Socket Soc, string host, int port)
        {
            ConsoleKeyInfo k;

            Console.WriteLine("Press ESC to exit...");
            while (true)
            {
                k = Console.ReadKey(true);
                Soc.Close();
                Console.WriteLine("Connection Closed");
                System.Threading.Thread.Sleep(1000);
                if (k.Key == ConsoleKey.Escape)
                    break;
            }
        }

        public static Socket Connect(string host, int port)
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream,
               ProtocolType.Tcp);
                Console.WriteLine("Establishing Connection to {0}",
                    host);
                var result = s.BeginConnect(host, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                if (!success)
                {
                    throw new Exception("Failed to connect.");
                }
                return s;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }




        }

        public static void SendReceiveTest1(Socket server, string SendText)
        {
            string end = "\r";
            byte[] msg = Encoding.UTF8.GetBytes(SendText+end);
            byte[] bytes = new byte[256];
            try
            {
                int i = server.Send(msg);
                Console.WriteLine("Sent {0} bytes.", i);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }
    }
}
