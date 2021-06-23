using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace Ip
{
    class Foxtools
    {
        readonly string Uri = "http://foxtools.ru/Proxy";
        List<string> list = new List<string>();

        public List<string> List { get => list; private set => list = value; }

        public Foxtools()
        {
            HtmlWeb web = new HtmlWeb();
            var HtmlDok = web.Load(Uri);
            int count = 1;
            int countp = 1;
            while (countp <= 5)
            {
                while (count <= 31)
                {
                    try
                    {
                        list.Add(HtmlDok.DocumentNode.SelectSingleNode("//*[@id='theProxyList']/tbody/tr[" + count + "]/td[2]").InnerText + ":" +
                        HtmlDok.DocumentNode.SelectSingleNode("//*[@id='theProxyList']/tbody/tr[" + count + "]/td[3]").InnerText);
                        count++;
                    }
                    catch
                    {
                        break;
                    }
                }

                countp++;
                count = 1;
                HtmlDok = web.Load(Uri + "?page=" + countp);
            }
        }
    }
}
