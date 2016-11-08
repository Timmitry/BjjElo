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
            var url = args[0];

            if (url == String.Empty)
                url = "http://www.bjjheroes.com/bjj-fighters/roger-gracie-bio";

            // in case you sit behind a corporate proxy
            WebClient wc = new WebClient();
            wc.Proxy = new WebProxy("proxy.example.com", 8080);
            var page = wc.DownloadString(url);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            // get fighter name
            var fighterInfo = doc.DocumentNode.SelectNodes("//div[@class='fighter_info_plug']/h3[@class='name']");
            var fighterNameGrapplingRecord = fighterInfo.First().InnerText;

            var fighterName = fighterNameGrapplingRecord.Replace("Grappling Record", string.Empty).Trim();

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

                sb.Append(opponent).Append(",");
                sb.Append(result).Append(",");
                sb.Append(method).Append(",");
                sb.Append(competition).Append(",");
                sb.Append(weightclass).Append(",");
                sb.Append(stage).Append(",");
                sb.Append(year).AppendLine();
                
            }

            // write to csv
            var filePath = fighterName.Replace(" ", "_").ToLower() +  ".csv";
            System.IO.File.WriteAllText(filePath, sb.ToString());
        }
    }
}
