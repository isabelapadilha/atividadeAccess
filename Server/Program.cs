using Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            bool done = false;

            var listener = new TcpListener(IPAddress.Any, 8088);

            listener.Start();
            Context db = new Context();
            while (!done)
            {
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();

                IFormatter formatter = new BinaryFormatter();

                DTO p = (DTO)formatter.Deserialize(ns);

                db.log.Add(p);
                db.SaveChanges();


                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                try
                {
                    ns.Write(byteTime, 0, byteTime.Length);
                    ns.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            listener.Stop();

        }
    }
}
