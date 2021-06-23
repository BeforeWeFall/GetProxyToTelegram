using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Ip
{
    class hidemy
    {
        readonly string Uri = "http://foxtools.ru/Proxy";
        List<string> list = new List<string>();

        public List<string> List { get => list; private set => list = value; }

        public  hidemy()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://hidemy.name/ru/proxy-list/?type=hs#list";

            var list = new List<string>();
            IWebElement element;
            IWebElement p;
            Thread.Sleep(7000);

            element = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div/form/div[2]/div[1]/button"));
            element.Click();
            int page = 2;
            var lines = new List<string>();
            while (true)
            {
                element = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div[4]/table"));
                lines = System.Text.RegularExpressions.Regex.Split(element.Text, "минут|минуты").ToList();
                lines.RemoveAt(0);
                try
                {
                    lines.RemoveAt(lines.Count - 1);
                }
                catch { }
                list.AddRange(lines.Where(y => Convert.ToInt32(Regex.Match(y, "\\d+(?= мс)").Value) < 1500).Select(x => Regex.Match(x, "\\d+.\\d+.\\d+.\\d+").Value + ":" + Regex.Match(x, "\\d+(?= [A-Z])").Value));

                try
                {
                    page++;
                    try
                    {
                        element = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div[5]/ul/li[" + page.ToString() + "]/a"));
                        element.Click();
                    }
                    catch
                    {
                        page++;
                        element = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div[5]/ul/li[" + page.ToString() + "]/a"));
                        element.Click();
                    }
                    Thread.Sleep(300);
                }
                catch
                {
                    break;
                }
            }
            driver.Close();
        }

    }
}

