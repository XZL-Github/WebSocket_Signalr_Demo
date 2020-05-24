using SuperSocket.SocketBase;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebSocketServer();

            server.NewSessionConnected += ServerNewSessionConnected;
            server.NewMessageReceived += ServerNewMessageRecevied;
            server.SessionClosed += ServerSessionClosed;

            try
            {
                server.Setup("127.0.0.1", 4141);
                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static void ServerSessionClosed(WebSocketSession session, CloseReason value)
        {
            Console.WriteLine(session.Origin);
        }

        public static void ServerNewMessageRecevied(WebSocketSession session, string value)
        {
            Console.WriteLine(value);
            session.Send("已收到：" + value);
        }

        /// <param name="session"></param>
        public static void ServerNewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine(session.Origin);
        }
    }
}
