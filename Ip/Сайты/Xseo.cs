using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ip
{
    class Xseo
    {
        readonly string Uri = "http://xseo.in/freeproxy";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public Xseo()
        {
            HtmlWeb web = new HtmlWeb();
            var HtmlDok = web.Load(Uri);
            string Dock = HtmlDok.Text;

            var reg = Regex.Matches(Dock, @"\d+\.\d+\.\d+\.\d+:\d+");
            list.AddRange(reg.Select(x => x.ToString()));
        }
    }
}
