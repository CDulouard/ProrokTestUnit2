using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using UIScripts;
using UnityEngine;

using UnityEngine.SceneManagement;
public class UdpSocket{
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int BufSize = 8 * 1024;
        private State state = new State();
        private EndPoint _epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback _recv;
        private UdpSocket _client;
        private string _clientIp;
        private int _clientPort;
        


        public class State
        {
            public byte[] buffer = new byte[BufSize];
        }

        public void Stop()
        {
            //_socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        
        public void StartServer(string address, int port)
        {
            
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();
        }

        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);
            Receive();
        }

        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                var so = (State) ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
        }

        private void Receive()
        {
            _socket.BeginReceiveFrom(state.buffer, 0, BufSize, SocketFlags.None, ref _epFrom, _recv = ar =>
            {
                var so = (State) ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref _epFrom);
                _socket.BeginReceiveFrom(so.buffer, 0, BufSize, SocketFlags.None, ref _epFrom, _recv, so);

                var msg = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                var from = _epFrom.ToString();
                
                Console.WriteLine("RECV: {0}: {1}, {2}", from, bytes,
                    msg);
                Console.WriteLine();
                ReadMessage(msg, out var id, out var content);


                switch (id)
                {
                    case 100:
                        var rx = new Regex(@"[0-9]*",
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

                        var port = content.Substring(1, content.Length - 1);
                    
                        if (int.TryParse(rx.Match(port).ToString(), out var x))
                        {
                            if (_client != null)
                            {
                                _client.Stop();
                            }
                        
                            _clientPort = x;
                            _clientIp = ReadIp(from);
                        
                            Console.WriteLine(_clientIp+ ":" + _clientPort);
                        
                            _client = new UdpSocket();
                            _client.Client(_clientIp, _clientPort);
                            _client.Send("Connected");
                            Console.WriteLine(_clientIp+ ":" + _clientPort);
                      
                        }
                        else
                        {
                            _clientPort = 0;
                        }
                        break;
                    case 1000:
                        /*    received new target position    */
                        if (ReadIp(from) == _clientIp && _clientPort != 0)
                        {
                            ReadTargetPositions(content);
                            _client.Send(Manager.status);
                        }
                        break;
                    
                    case 1001:
                        /*    Reset    */
                        if (ReadIp(from) == _clientIp && _clientPort != 0)
                        {
                            _client.Send(Manager.status);
                        }
                        break;
                    case 1002:
                        /*    received new target position    */
                        
                        if (ReadIp(from) == _clientIp && _clientPort != 0)
                        {
                            Controller.Respawn();
                            _client.Send(Manager.status);
                        }
                        break;
                    case 1003:
                        /*    received new target position    */
                        
                        if (ReadIp(from) == _clientIp && _clientPort != 0)
                        {
                            _client.Send(Controller.GetScore().ToString());
                        }
                        break;
                        
                    default:
                        Debug.Log("Unknown id");
                        break;
                }
                Console.WriteLine();
            }, state);
        }

        private static string ReadIp(string from)
        {
            var rx = new Regex(@"[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.Match(from).ToString();
        }
        private static void ReadMessage(string message, out int id, out string content)
        {
            var rx = new Regex(@"^[0-9]*",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (int.TryParse(rx.Match(message).ToString(), out var x))
            {
                id = x;
                var idLength = id.ToString().Length;
                content = message.Substring(idLength, message.Length - idLength);
                return;
            }

            content = message;
            id = -1;
        }

        private static void ReadTargetPositions(string message)
        {
            /*    Function use to read and refresh target positions    */
            Server.targetPositions = message;           
        }
       
    }
