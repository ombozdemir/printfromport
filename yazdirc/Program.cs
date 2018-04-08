using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6;
namespace yazdirc
{
    class Program
    {
        private const int listenPort = 33333;

        private static void StartListener()
        {
            bool done = false;

            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);
                    Console.WriteLine("Received broadcast from {0} :\n {1}\n", groupEP.ToString(), Encoding.UTF8.GetString(bytes, 0, bytes.Length));

                    Printer defaultPrinter = null;
                    PrinterCollection pc = new PrinterCollection();
                  
                    for (int i = 0; i < pc.Count; i++)
                    {
                        if (pc[i].IsDefaultPrinter)
                        {
                            defaultPrinter = pc[i];
                            //   textBox1.Text += defaultPrinter.DeviceName + "\r\n";
                            //  textBox1.Text += "Found\r\n";
                            break;
                        }
                    }
                    //string datam = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    string datam2 = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    string[] words2 = datam2.Split('*');
                   // string[] words = datam.Split('*');
                    DateTime localDate = DateTime.Now;
                    Console.WriteLine(localDate.ToString());
              
                    foreach (string word in words2)
                    {
                        Console.WriteLine(word);
                        defaultPrinter.Print(word);
                    }
                 //   defaultPrinter.Print("Hello World");
                    defaultPrinter.EndDoc();
                    Console.Clear();
                
                }
              
  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }
        public void PrintFile(object sender, EventArgs e)
        {
      
        }
        public static int Main()
        {
            StartListener();

            return 0;
        }
    }
}
