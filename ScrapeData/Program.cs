using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace ScrapeData
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://www.bjjheroes.com/a-z-bjj-fighters-list";

            var webclient = new WebClient();
            var pageAsString = webclient.DownloadString(url);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(pageAsString);

            var rows = htmlDoc.DocumentNode.SelectNodes("//table/tbody/tr");


            Console.WriteLine("Starting to gather match information.");
            Console.WriteLine($"{rows.Count} fighter websites are to be processed.");

            int i = 1;
            foreach (var row in rows)
            {
                Console.Write($"({i++}/{rows.Count}) ");

                var cells = row.SelectNodes("./td");

                var firstName = cells[0].InnerText.Trim();
                var lastName = cells[1].InnerText.Trim();

                var pageUrl = cells[0].InnerHtml;
                pageUrl = pageUrl.Replace("<a href=\"", string.Empty);

                
                var index = pageUrl.IndexOf('"');

                pageUrl = pageUrl.Substring(0, index);

                if (pageUrl[0].Equals('/'))
                {
                    pageUrl = "http://www.bjjheroes.com" + pageUrl;
                }

                WebsiteToCsv(pageUrl, firstName, lastName);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Done!");

            Console.ReadLine();

        }



        public static void WebsiteToCsv(string url, string firstname, string lastname)
        { 
            //var url = args[0];

            //if (url == String.Empty)
            
            // in case you sit behind a corporate proxy
            WebClient wc = new WebClient();
            //wc.Proxy = new WebProxy("proxy.example.com", 8080);

            string page;
            try
            {
                page = wc.DownloadString(url);
            }
            catch
            {
                Console.Write($"Website for {firstname} {lastname} does not exist, skipping...\n");
                return;
            }


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            // get fighter name
            var fighterInfo = doc.DocumentNode.SelectNodes("//div[@class='fighter_info_plug']/h3[@class='name']");

            if (fighterInfo == null)
            {
                Console.Write($"No fight history for fighter {firstname} {lastname} found, skipping...\n");
                return;
            }

            //var fighterNameGrapplingRecord = fighterInfo.First().InnerText;

            //var fighterName = fighterNameGrapplingRecord.Replace("Grappling Record", string.Empty).Trim();

            // parse fight history
            var rows = doc.DocumentNode.SelectNodes("//table/tbody/tr");
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("Opponent,W/L,Method,Competition,Weight,Stage,Year");

            foreach (var row in rows)
            {
                var cells = row.SelectNodes("./td");
                string sort = cells[0].InnerText;
                string opponent = cells[1].SelectNodes("./span")[0].InnerText;
                string result = cells[2].InnerText;
                string method = cells[3].InnerText;
                string competition = cells[4].InnerText;
                string weightclass = cells[5].InnerText;
                string stage = cells[6].InnerText;
                string year = cells[7].InnerText;

                sb.Append(opponent).Append(";");
                sb.Append(result).Append(";");
                sb.Append(method).Append(";");
                sb.Append(competition).Append(";");
                sb.Append(weightclass).Append(";");
                sb.Append(stage).Append(";");
                sb.Append(year).AppendLine();

            }


            var fighterNameFile = firstname.Replace(" ", string.Empty) + "_" + lastname.Replace(" ", string.Empty);

            // write to csv
            //var filePath = fighterName.Replace(" ", "_").ToLower() + ".csv";
            var filePath = "..//..//..//Data//" + fighterNameFile + ".csv";
            System.IO.File.WriteAllText(filePath, sb.ToString());

            Console.Write($"Fight history for fighter {firstname} {lastname} successfully scraped!\n");
        }
    }
}
