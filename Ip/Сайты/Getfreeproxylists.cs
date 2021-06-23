using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ip
{
    class Getfreeproxylists
    {
        readonly string Uri = "https://getfreeproxylists.blogspot.com";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public Getfreeproxylists()
        {
            HtmlWeb web = new HtmlWeb();
            var HtmlDok = web.Load(Uri);
            string Dock = HtmlDok.Text;

            Regex regex = new Regex(@"<h2>HTTP.*<h2>SOCKS");
            var reg = regex.Matches(Dock);
            var regP = Regex.Matches(reg[0].ToString(), @"\d+\.\d+\.\d+\.\d+:\d+");
            list.AddRange(regP.Select(x=>x.ToString()));
        }
    }
}
