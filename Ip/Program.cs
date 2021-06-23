using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;

namespace Ip
{
    class Program
    {
        static void Main()
        {            

            Console.WriteLine("Начало сбора proxy " + DateTime.Now.TimeOfDay);

            List<string> list = new List<string>();

            list.AddRange(new hidemy().List);
            list.AddRange(new Aliveproxy().List);
            list.AddRange(new HtmlWebru().List);
            list.AddRange(new Freeproxylists().List); 
            list.AddRange(new Foxtools().List);
            list.AddRange(new seogift().List);
            list.AddRange(new Hide_my_ip().List);
            list.AddRange(new Getfreeproxylists().List);
            list.AddRange(new Xseo().List);
            list.AddRange(new webanetlabs().List);
            Console.WriteLine(list.Count);

            list = list.Distinct().ToList();
            Console.WriteLine(list.Count);

            Console.WriteLine("Начало проверки в тг " + DateTime.Now.TimeOfDay);

            CheckIpInTGM check = new CheckIpInTGM(list, @"C:\Users\ULBelykh\Desktop\RabIp.txt", @"C:\Users\ULBelykh\Desktop\NeRabIp.txt");

            Console.WriteLine("Загрузка в гугл" + DateTime.Now.TimeOfDay);
            GoogleLoad google = new GoogleLoad(@"YOURPATH");
        }
    }
}
