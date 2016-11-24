using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BingLibrary.hjb
{
    public class TcpipConnection
    {
        private TcpClient client = new TcpClient();
        private NetworkStream stream;
        public bool tcpConnected { set; get; }

        public async Task<bool> Connect(string ip, int port)
        {
            bool r = false;

            Task taskDelay = Task.Delay(6000);
            var completedTask = await Task.WhenAny(((Func<Task>)(() =>
            {
                return Task.Run(() =>
                {
                    try
                    {
                        client.SendTimeout = 600;
                        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
                        client.Connect(ipe);
                        tcpConnected = true;
                        r = true;
                    }
                    catch {; client.Close(); client = new TcpClient(); tcpConnected = false; r = false; }
                });
            }))(), taskDelay);
            if (completedTask == taskDelay)
            {
                client.Close(); client = new TcpClient();
                r = false;
            }

            return r;
        }

        public void IsOnline()
        {
            try
            {
                tcpConnected = !((client.Client.Poll(1000, SelectMode.SelectRead) && (client.Client.Available == 0)) || !client.Client.Connected);
            }
            catch { tcpConnected = false; }
        }

        public async Task<string> ReceiveAsync()
        {
            string tempS = "error";
            await ((Func<Task>)(() =>
            {
                return Task.Run(() =>
                {
                    try
                    {
                        byte[] data = new Byte[256];
                        string responseData = string.Empty;
                        stream = client.GetStream();
                        stream.ReadTimeout = 200;
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        // Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
                        tempS = Encoding.ASCII.GetString(data, 0, bytes);
                        return tempS;
                    }
                    catch
                    {
                        return "error";
                    }
                });
            }))();
            return tempS;
        }

        public async Task<string> SendAsync(string message)
        {
            try
            {
                stream = client.GetStream();
                await stream.WriteAsync(Encoding.ASCII.GetBytes(message + "\r\n"), 0, Encoding.ASCII.GetBytes(message + "\r\n").Length);

                return "";
            }
            catch { tcpConnected = false; return "error"; }
        }
    }
}
