using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ip
{
    class CheckIpInTGM
    {
        readonly string api = "YOURTOKEN";
        static List<string> rabIp = new List<string>();
        static List<string> nerabip = new List<string>();

        public CheckIpInTGM(List<string> LIp, string PathRabIp, string PathNerabIp)
        {
            Console.WriteLine("Считывание старого файла");
            var list = ReadOldF(PathNerabIp);
           
            var maxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.ForEach(LIp, new ParallelOptions { MaxDegreeOfParallelism = 100}, (ip) =>
            {
                try
                {
                    if (list.Where(x => x.Equals(ip.ToString())).Count() > 0) { }
                    else
                    {
                            var SplitIp = ip.ToString().Split(':');
                            var proxy = new WebProxy(SplitIp[0].Trim(), Convert.ToInt32(SplitIp[1])) { }; 
                            var botClient = new TelegramBotClient(api, proxy);
                            var me = botClient.GetMeAsync().Result;
                            lock (rabIp)
                                rabIp.Add(ip); 
                    }
                }
                catch
                {
                    lock (nerabip)
                        nerabip.Add(ip);
                }
            });
            Console.WriteLine(rabIp.Count() + "Rab");
            WriteIp(PathRabIp, rabIp);
             
            Console.WriteLine(nerabip.Count() + "NeRab");
            WriteIp(PathNerabIp, nerabip);
        }

        private List<string> ReadOldF(string pathF)
        {
            StreamReader f = null;
            var list = new List<string>();        

            if (File.Exists(pathF))
            {
                f = File.OpenText(pathF);

                while (!f.EndOfStream)
                {
                    list.Add(f.ReadLine());
                }
                f.Close();
            }
            return list;
        }

        private async void WriteIp(string Path, List<string> ip)
        {
            using (StreamWriter sw = new StreamWriter(Path, true, System.Text.Encoding.Default))
            {
                foreach (var elem in ip)
                    await sw.WriteLineAsync(elem.ToString());
            }              
        }
    }
}
