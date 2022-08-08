using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ForTest
{
    public class fromAgility
    {
        public string UrlToStrings(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);

            HtmlNode allData = document.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]");

            HtmlNode date = allData.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
            if (date is not null)
            {
                date.Remove();
            }

            HtmlNode socBlock = allData.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
            if (socBlock is not null)
            {
                socBlock.Remove();
            }

            HtmlNode nav = allData.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[3]");
            if (nav is not null)
            {
                nav.Remove();
            }

            HtmlNode socBlock1 = allData.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[3]");
            if (socBlock1 is not null)
            {
                socBlock1.Remove();
            }

            HtmlNode commentABl = allData.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[3]");
            if (commentABl is not null)
            {
                commentABl.Remove();
            }

            HtmlNode disqus_thread = allData.SelectSingleNode(@"div[@id='disqus_thread']");
            if (disqus_thread is not null)
            {
                disqus_thread.Remove();
            }

            HtmlNode disqus_recommendations = allData.SelectSingleNode(@"div[@id='disqus_recommendations']");
            if (disqus_recommendations is not null)
            {
                disqus_recommendations.Remove();
            }

            HtmlNode adsbygoogle = allData.ChildNodes.FirstOrDefault(i => i.Name == "div").ChildNodes.FirstOrDefault(i => i.Name == "ins");
            if (adsbygoogle is not null)
            {
                adsbygoogle.Remove();
            }
            return allData.InnerHtml.Replace("./", "https://metanit.com/sharp/mvc5/");
        }
        public List<string> SendUrlSorted(string url)
        {
            List<string>? urls = new List<string>();
            string firstUrl = "./1.1.php";
            urls?.Add(url + firstUrl.Replace("./", ""));
            for (int i = 0; i < int.MaxValue; i++)
            {
                Console.WriteLine(url + firstUrl.Replace("./", ""));
                string secondUrl = NextUrlDefined(url + firstUrl.Replace("./", ""));
                if (secondUrl == "")
                {
                    break;
                }
                urls?.Add(url + secondUrl.Replace("./", ""));
                firstUrl = secondUrl;
            }
            return urls;
        }
        public string NextUrlDefined(string url)
        {
            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);

                HtmlNode allData = document.DocumentNode.SelectSingleNode(@"//a[contains(text(), 'Вперед')]");
                string hrefValue = allData.Attributes["href"].Value;
                return hrefValue;
            }
            catch
            {
                return "";
            }
        }
        public void SaveHtmlForString(string html, string path, string path2)
        {
            string data = File.ReadAllText(path, Encoding.UTF8);
            data = data.Replace("all_data", html);
            File.WriteAllText(path2, data);
        }
        public bool SavePdfCheck(string filePath, string output)
        {
            bool check = false;
            try
            {
                var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                using (var p = new Process())
                {
                    p.StartInfo.FileName = chromePath;
                    p.StartInfo.Arguments = $"--headless --disable-gpu --run-all-compositor-stages-before-draw  --print-to-pdf-no-header --print-to-pdf=\"{output}\" --no-margins \"{filePath}\"";
                    p.Start();
                    p.WaitForExit();
                }
                check = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                check = false;
            }
            return check;
        }
        public void Progression(string url)
        {
            List<string> urls = SendUrlSorted(url);/*File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "html", "newUrl.txt")).Split(",").ToList();*/
            int i = 1;
            string directory = "";
            foreach (var url_1 in urls)
            {
                string testDirectory = GetSubDirectoryName(url_1);
                string folder = "c:\\testMVC\\";
                if (testDirectory is not "")
                    directory = testDirectory;
                string FileName = GetTitleUrl(url_1) + ".pdf";
                string fileName = Path.Combine(folder, directory, (i.ToString("000") + " " + FileName));
                if (!File.Exists(fileName))
                {
                    string data = UrlToStrings(url_1);
                    SaveHtmlForString(data, Path.Combine(Directory.GetCurrentDirectory(), "html\\index.html"), Path.Combine(Directory.GetCurrentDirectory(), "html\\index1.html"));

                    if (!Directory.Exists(Path.Combine(folder, directory)))
                    {
                        Directory.CreateDirectory(Path.Combine(folder, directory));
                    }
                    SavePdfCheck(Path.Combine(Directory.GetCurrentDirectory(), "html\\index1.html"), fileName);
                    i++;
                    Console.WriteLine(fileName);
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
            Console.WriteLine("tamoms");
        }
        public string GetTitleUrl(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            HtmlNode sell1 = document.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h2[1]");
            if (sell1 is not null)
            {
                var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h2[1]").InnerText;
                return title;
            }
            else
            {
                return "";
            }
        }

        public string GetSubDirectoryName(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            HtmlNode sell1 = document.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h1[1]");

            if (sell1 is not null)
            {
                var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h1[1]").InnerText;
                return title;
            }
            else
            {
                return "";
            }
        }
    }
}