using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            FleckLog.Level = LogLevel.Debug;

            var allSockets = new List<IWebSocketConnection>();

            var server = new WebSocketServer("ws://127.0.0.1:7181");

            server.Start(socket =>

            {

                socket.OnOpen = () =>

                {

                    Console.WriteLine("Open!");

                    allSockets.Add(socket);
                    socket.Send("连接成功");

                };

                socket.OnClose = () =>

                {

                    Console.WriteLine("Close!");

                    allSockets.Remove(socket);

                };

                socket.OnMessage = message =>

                {

                    Console.WriteLine(message);
                    socket.Send("message发送成功");
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));

                };

            });

            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                   socket.Send(Encoding.UTF8.GetBytes(input));
                }
                input = Console.ReadLine();
            }
        }
    }
}
