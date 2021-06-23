using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ip
{
    class Hide_my_ip
    {

        readonly string Uri = "https://www.hide-my-ip.com/ru/proxylist.shtml";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public Hide_my_ip()
        {
            HtmlWeb web = new HtmlWeb();
            var HtmlDok = web.Load(Uri);

            int count = 0;
            string Dock = HtmlDok.Text;
            Regex regex = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            var reg = regex.Matches(Dock);
            var regP = Regex.Matches(Dock, "(?<=p\":\")\\d.+?(?=\")");
            while (true)
            {
                try
                {
                    list.Add(reg[count].ToString() + ":" + regP[count].ToString());
                    count++;
                }
                catch
                {
                    break;
                }  
            }
        }
    }
}
