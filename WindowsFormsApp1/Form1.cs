using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IDisposable SignalR { get; set; }
        private const string ServerUri = "http://localhost:8888"; // SignalR服务地址


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            WriteToConsole("正在启动服务...");
            Task.Run(() => { button1.Enabled = !StartServer(); }); // 异步启动SignalR服务

            //Task.Run(() => StartServer()); // 异步启动SignalR服务
            //Task.Run(() =>
            //{
            //    BtnStart.Enabled = !StartServer();
            //bool flag = StartServer();
            //BtnStart.Invoke(new Action<bool>((f) => BtnStart.Enabled = !f), flag);

            // }); // 异步启动SignalR服务

            //BtnStart.Enabled = !StartServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignalR.Dispose();
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // <summary>
        /// 启动SignalR服务，将SignalR服务寄宿在WPF程序中
        /// </summary>
        private bool StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerUri);  // 启动SignalR服务
            }
            catch (Exception ex)
            {
                WriteToConsole(ex.Message);
                return false;
            }

            WriteToConsole("服务已经成功启动，地址为：" + ServerUri);
            return true;
        }

        private delegate void WriteToConsoleDe(string msg);
        /// <summary>
        /// 将消息添加到消息列表中
        /// </summary>
        /// <param name="message"></param>
        public void WriteToConsole(string message)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action<string>((string msg) => textBox1.AppendText(message + Environment.NewLine)), message);
                return;
            }

            textBox1.AppendText(message + Environment.NewLine);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChatConnection.connection.Broadcast(this.textBox2.Text);
        }
    }
}
