using Microsoft.AspNet.SignalR;

//Hub的别名,方便前台调用
//[HubName("getMessage")]
public class MyHub : Hub
{
    /// <summary>
    /// 编写发送信息的方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="message"></param>
    public void Send(string name, string message)
    {
        //调用所有客户注册的本地的JS方法(addMessage)
        Clients.All.addMessage(name, message);
    }
}