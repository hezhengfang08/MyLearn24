using My.CommuniTest;
using System.IO.Ports;
using System.Net.Sockets;

namespace My.CommuniTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            SocketTest socketTest = new SocketTest();
            socketTest.Test();
        }
    }
}
