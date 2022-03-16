
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace InoServerSocketTcp
{
    public class Program
    {
        static int Main(string[] args)
        {
            StartServer();
            return 0;
        }

        public static void StartServer()
        {
            //192.168.0.105 CASA
            //172.29.128.1 EMPRESA
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("172.17.144.1"), 14150);

            try
            {
                Socket listener = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                string data = null;
                byte[] bytes = null;

                //Console.WriteLine($"{DateTime.Now.Date}");
                //Console.WriteLine($"{Convert.ToInt32(DateTime.Now.ToString("HH:mm").Replace(":",""))}");
                //Console.WriteLine($"{DateTime.Now}");

                while (true)
                {
                    Socket handler = listener.Accept();

                    //while (true)
                    //{
                    //    bytes = new byte[1024];
                    //    int bytesRec = handler.Receive(bytes);
                    //    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //    if (data != null && data != "")
                    //    {
                    //        break;
                    //    }
                    //}


                    //if (data != null && data != "")
                    //{
                        Console.WriteLine("Text received : {0}", data);

                        while (true)
                        {
                            Console.WriteLine("Digite a tag:");
                            var tagg = Console.ReadLine();
                            TagReceived tag = new TagReceived(tagg);
                            var serializedContent = JsonConvert.SerializeObject(tag);
                            Debug.WriteLine(serializedContent);

                            byte[] msg = Encoding.ASCII.GetBytes(serializedContent);

                            handler.Send(msg);
                        }
                        data = null;
                    //}
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}