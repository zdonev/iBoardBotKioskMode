using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = System.Timers;
using System.Windows.Forms;
using iBoardBotKioskMode.Properties;
using System.Net;
using System.Collections.Specialized;

namespace iBoardBotKioskMode
{
    
    class Program
    {
        private static T.Timer aTimer;

        public static object ConfigurationManager { get; private set; }

        static void Main(string[] args)
        {
            
            aTimer = new System.Timers.Timer(Settings.Default.DrawingTimerInSeconds * 1000);
            aTimer.Elapsed += OnTimedEvent;
            // Hook up the Elapsed event for the timer. 
            aTimer.AutoReset = true;
            Console.WriteLine("The timer should fire every {0} seconds.",
                            Settings.Default.DrawingTimerInSeconds);
            aTimer.Enabled = true;
            aTimer.Start();

            Console.WriteLine("Please press ENTER to exit!");
            Console.ReadLine();

            KillAllChromeInstances();
        }


        private static void OnTimedEvent(Object source, T.ElapsedEventArgs e)
        {
            KillAllChromeInstances();
            EmptyQueue();

            aTimer.Stop();
            var f = new Form();
            f.TopMost = true;

            var a = new FormsApp.ModalForm();
            a.TopMost = true;
            a.ShowDialog();
            
            if (a.DialogResult == DialogResult.OK)
            {
                var uri = string.Format("{0}index.php?APPID={1}", Settings.Default.BaseURI, Settings.Default.APPID);
                ChromeWrapper chrome = new ChromeWrapper(uri);
                
                Clear();
                aTimer.Start();
            }
            
            //chrome.SendKey((char)122);// F11
        }

        private static void KillAllChromeInstances()
        {
            var procs = Process.GetProcesses();

            foreach (var proc in procs)
            {
                if (proc.ProcessName.ToLower() == "chrome")
                {
                    proc.Kill();
                }
            }
        }


        public static void Clear(int x1 = 0, int y1 = 0, int x2 = 358, int y2 = 105)
        {
            using (var client = new WebClient())
            {
                client.BaseAddress = Settings.Default.BaseURI;
                client.UploadValues("pErase.php", new NameValueCollection {
                     {"APPID", Settings.Default.APPID},
                     {"X1", x1.ToString()},
                     {"Y1", y1.ToString()},
                     {"X2", x2.ToString()},
                     {"Y2", y2.ToString()},
                 });
            }
        }


        private static void EmptyQueue()
        {
            using (var client = new WebClient())
            {
                client.BaseAddress = Settings.Default.BaseURI;

                var uri = string.Format("{0}iwbb_clear.php?APPID={1}", Settings.Default.BaseURI, Settings.Default.APPID);

                var text = client.DownloadString(uri);

                 
            }

            //var client = new WebClient();

            //client.BaseAddress = Settings.Default.BaseURI;
            //client.

        }
    }

    
}
