using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ip
{
    class webanetlabs
    {
        readonly string Uri = "https://webanetlabs.net/publ/9-1-0-15";
        List<string> list = new List<string>();
        public List<string> List { get => list; set => list = value; }

        public webanetlabs()
        {
            HtmlWeb web = new HtmlWeb();
            var HtmlDok = web.Load(Uri);

            string Dock = HtmlDok.Text;
            Regex regex = new Regex(@"\d*\.\d*\.\d*\.\d*:\d*");
            var reg = regex.Matches(Dock);
            foreach (var t in reg)
                list.Add(t.ToString());
        }
    }
}
