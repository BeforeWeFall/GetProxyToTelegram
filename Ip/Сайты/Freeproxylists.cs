using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ip
{
    class Freeproxylists
    {
        readonly string Uri = "http://www.freeproxylists.net/ru/";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public Freeproxylists()
        {
            HtmlWeb web = new HtmlWeb();
            
            var HtmlDok = web.Load(Uri);
            string Dock = HtmlDok.Text;

            Regex regex = new Regex("(?<=IPDecode\\(\").*?(?=\")");
            var reg = regex.Matches(Dock);
            foreach(var ip in reg)
            {
                list.Add(GetIpAndHextoString(ip.ToString()));
            }
        }

        private string GetIpAndHextoString(string InputText)
        {
            InputText = InputText.Replace("%", "");
            byte[] bb = Enumerable.Range(0, InputText.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                             .ToArray();

            return Regex.Match(Encoding.ASCII.GetString(bb), @"(?<=>).*(?=<)").ToString() ;
        }
    }
}
