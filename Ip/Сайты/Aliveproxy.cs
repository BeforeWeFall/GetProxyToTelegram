using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Ip
{
    class Aliveproxy
    {
        readonly string[] Uri = 
            { 
                "http://aliveproxy.com/proxy-list-port-80/",
                "http://aliveproxy.com/proxy-list-port-81/",
                "http://aliveproxy.com/proxy-list-port-3128/",
                "http://aliveproxy.com/proxy-list-port-8000/",
                "http://aliveproxy.com/proxy-list-port-8080/" 
            };
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public Aliveproxy()
        {
            HttpWebRequest req;
            HttpWebResponse res;

            foreach (var page in Uri)
            {
                req = (HttpWebRequest)WebRequest.Create(page);
                res = (HttpWebResponse)req.GetResponse();
                var HtmlDock = new StreamReader(res.GetResponseStream()).ReadToEnd();
                var ip = Regex.Matches(HtmlDock, @"\d+\.\d+\.\d+\.\d+:\d+");
                list.AddRange(ip.Select(x => x.ToString()));
            }
            Console.WriteLine(list.Count());
        }
    }
}
