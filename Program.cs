using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpC = new TcpClient();
            tcpC.Connect("192.168.168.194", 5555);
            NetworkStream strm = tcpC.GetStream();
            string str = " ";

            while (str != "stop")
            {
                str = Console.ReadLine() + "\r\n";
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] msg = asen.GetBytes(str);

                strm.Write(msg,0, msg.Length);

                byte[] bb = new byte[100];
                int k = strm.Read(bb, 0, 100);

                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                Console.WriteLine();
            }
            tcpC.Close();
            

        }
    }
}
