using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ChatConnection: PersistentConnection
    {
        private static int _connections = 0;
        public static IConnection connection;

        protected override Task OnConnected(IRequest request, string connectionId)
        {
            connection = Connection;
            Interlocked.Increment(ref _connections);
            //广播消息
            Connection.Broadcast("新的连接加入，连接ID：" + connectionId + ",已有连接数：" + _connections);
            return Connection.Send(connectionId, "双向连接成功，连接ID：" + connectionId);
        }

        /// <summary>
        /// 连接断开 
        /// </summary>
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Decrement(ref _connections);
            return Connection.Broadcast(connectionId + "退出连接，已有连接数：" + _connections);
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var message = connectionId + "发送内容>>" + data;
            return Connection.Broadcast(message);
        }

    }
}
