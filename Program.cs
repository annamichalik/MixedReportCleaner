using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedReportCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;


            var xmlPath = Path.Combine(baseDirectory, @"Example\crawlExample.log");
            var xmlResultPath = Path.Combine(baseDirectory, @"Example\cleanedCrawlExample.log");
            var all = File.ReadAllText(xmlPath);
            var linesToKeep = File.ReadLines(xmlPath).Where(l => (!l.StartsWith("<debug") && !l.StartsWith("uniq") && l.StartsWith("<")));
            int idx = 0;
            var list = linesToKeep.ToList();
            var lastIndex =list.Count -1;
            var onlyMixedWihOpen = new List<string>(); 
            foreach (var line in list)
            {

                if (line.StartsWith("<open") )
                {
                    if ((lastIndex != idx))
                    {
                        var next = list[idx + 1];
                        if (next.Contains("<MIXED "))
                            onlyMixedWihOpen.Add(line);
                    }
                  

                }
                else
                {
                    if(!line.StartsWith("</open>"))
                        onlyMixedWihOpen.Add(line);
                }
                idx++;
            }
            File.WriteAllLines(xmlResultPath, onlyMixedWihOpen);
            Console.ReadKey();
        }
    }
}
