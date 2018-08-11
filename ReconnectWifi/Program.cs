using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ReconnectWifi
{
    class Program
    {
        const int DEFAULT_TIME = 60; // in seconds

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a wifi name.");
                return;
            }

            string wifiName = args[0];
            int checkTime = args.Length > 1 ? int.Parse(args[1]) : DEFAULT_TIME;

            while (true)
            {
                if (!PingHost("8.8.8.8") && !PingHost("8.8.8.8") && !PingHost("8.8.8.8"))
                {
                    //string cmd = "wlan connect profile=\"Pretty Fly For A Wi-Fi\" ssid=\"Pretty Fly For A Wi-Fi\"";
                    string cmd = $"wlan connect name={wifiName}";
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = "netsh.exe";
                    proc.StartInfo.Arguments = cmd;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.Start();

                    Console.WriteLine(proc.StandardOutput.ReadToEnd());
                }
                Console.WriteLine($"Connected, trying again in {checkTime} seconds");
                System.Threading.Thread.Sleep(checkTime * 1000);
            }
        }


        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();

            try
            {
                PingReply reply = pinger.Send(nameOrAddress);

                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException e)
            {
                // Discard PingExceptions and return false;
                Console.WriteLine(e.Message);
                return false;
            }

            return pingable;
        }
    }
}
