using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication14
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Get Ready...");                
                System.Threading.Thread.Sleep(1000);
                while (Console.KeyAvailable) Console.ReadKey(true); //flushes buffer to prevent attack queueing
                Console.Clear();
                Console.WriteLine("Attack!");
                string x = ReadKey(1000);
                if (x != "FAIL")
                {
                    Console.WriteLine($"Attack: {x}");
                    System.Threading.Thread.Sleep(1000);
                }
                else { break;
                }
            }
        }
        static string ReadKey(int timeoutms)
        {
            string resultstr = "";
            string location = "Torso";
            while (true)
            {
                ReadKeyDelegate d = Console.ReadKey;
                IAsyncResult result = d.BeginInvoke(true, null, null);
                result.AsyncWaitHandle.WaitOne(timeoutms);//timeout e.g. 15000 for 15 secs
                
                if (result.IsCompleted)
                {
                    ConsoleKeyInfo k = d.EndInvoke(result);
                    if (k.Key == ConsoleKey.NumPad8) { location = "Head";}
                    else if (k.Key == ConsoleKey.NumPad7) { location = "LeftArm"; }
                    else if (k.Key == ConsoleKey.NumPad9) { location = "RightArm"; }
                    else if (k.Key == ConsoleKey.NumPad5) { location = "Torso"; }
                    else if (k.Key == ConsoleKey.NumPad4) { location = "LeftArm"; }
                    else if (k.Key == ConsoleKey.NumPad6) { location = "RightArm"; }
                    else if (k.Key == ConsoleKey.NumPad1) { location = "LeftLeg"; }
                    else if (k.Key == ConsoleKey.NumPad2) { location = "Groin"; }
                    else if (k.Key == ConsoleKey.NumPad3) { location = "RightLeg"; }
                    else { resultstr += k.KeyChar.ToString(); }
                    
                }
                else
                {
                    return location + " " +resultstr;
                }
                if (resultstr.Length == 3) { return location + " " + resultstr; }
            }
           
        }
        delegate ConsoleKeyInfo ReadKeyDelegate(bool x);
    }
}
