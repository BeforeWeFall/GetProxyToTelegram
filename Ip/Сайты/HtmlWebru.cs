using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Ip
{
    class HtmlWebru
    {
        readonly string Uri = "https://htmlweb.ru/json/proxy/get?country_not=RU&short&perpage=1000";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public HtmlWebru()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            var htm = new StreamReader(resp.GetResponseStream()).ReadToEnd();

            var reg = Regex.Matches(htm, @"\d+.\d+.\d+.\d+:\d+");
            list.AddRange(reg.Select(x => x.ToString()));
            foreach (var q in list)
                Console.WriteLine(q);
        }
    }
}
