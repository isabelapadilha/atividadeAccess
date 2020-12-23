using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               
                string[] lst = File.ReadAllLines(@"access.log");

                foreach (var item in lst)
                {
                    string[] row = item.Split(' ');

                    DTO data = new DTO();
                    data.TimeStamp =  string.IsNullOrEmpty(row[0])?" ":row[0];
                    data.Column =  string.IsNullOrEmpty(row[1])?" ":row[1];
                    data.Request =   string.IsNullOrEmpty(row[2])?" ":row[2];
                    data.Status =  string.IsNullOrEmpty(row[3])?" ":row[3];
                    data.Port =   string.IsNullOrEmpty(row[4])?" ":row[4];
                    data.Method =  string.IsNullOrEmpty(row[5])?" ":row[5];
                    data.EndPoint =  string.IsNullOrEmpty(row[6])?" ":row[6];
                    data.ColumnNone =string.IsNullOrEmpty(row[8])?" ":row[8];
                    data.Format =   string.IsNullOrEmpty(row[9]) ? " " : row[9];
                  

                    Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
                    t.Start(data);

                }


            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }


        public static void ThreadProc(object data)
        {
            var t = (DTO)data;

            string serverIp = "192.168.1.7";

            TcpClient client = new TcpClient(serverIp, 8088); // have my connection established with a Tcp Server 

            NetworkStream ns = client.GetStream();

            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ns, t);

            byte[] bytes = new byte[1024];
            int bytesRead = ns.Read(bytes, 0, bytes.Length);

            Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

            client.Close();

        }
     
    }
}
