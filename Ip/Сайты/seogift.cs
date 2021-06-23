using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Ip
{
    class seogift
    {
        readonly string Uri = "https://seogift.ru/tools/besplatnye-proksi/";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public seogift()
        {
            HtmlWeb web = new HtmlWeb();
            var HtmlDok = web.Load(Uri);

            var q = HtmlDok.DocumentNode.SelectSingleNode("/html/body/div/main/div[1]/div/textarea/text()").InnerText;
            list.AddRange(q.Split(Environment.NewLine.ToCharArray()));
        }
    }
}
