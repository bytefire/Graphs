using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Graphs.Library
{
    /// <summary>
    /// A very basic web crawler. Since it doesn't persist any data, it will run out of memory if executed as it is.
    /// </summary>
    public class WebCrawler
    {
        /// <summary>
        /// Crawls the web in breadth firs manner.
        /// </summary>
        /// <param name="startingUrl">The root URL.</param>
        /// <param name="processUrl">Function delegete. If this returns true, then crawl method would return.</param>
        public void Crawl(string startingUrl, Func<string, bool> processUrl = null)
        {
            Queue<string> q = new Queue<string>();
            HashSet<string> visitedUrls = new HashSet<string>();
            string url;
            string webPage;
            IEnumerable<string> linkedUrls;

            q.Enqueue(startingUrl);
            visitedUrls.Add(startingUrl);

            while (q.Count > 0)
            {
                url = q.Dequeue();
                // if the processing was successful then exit. this is important because of memory limits.
                if (processUrl(url))
                {
                    return;
                }

                webPage = ReadWebPage(url);
                linkedUrls = ExtractUrls(webPage);

                foreach (string linkedUrl in linkedUrls)
                {
                    if (!visitedUrls.Contains(linkedUrl))
                    {
                        visitedUrls.Add(linkedUrl);
                        q.Enqueue(linkedUrl);
                    }
                }
            }
        }

        private string ReadWebPage(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string webPage = String.Empty;

            // ImproveTODO: rather than read the page and then search for linked URLs,
            //      it will be better to read the stream line-by-line and return URLs via IEnumerator.
            using (StreamReader sr = new StreamReader(data))
            {
                webPage = sr.ReadToEnd();
            }
            return webPage;
        }

        private IEnumerable<string> ExtractUrls(string webPage)
        {
            // regex for url: http://(\\w+\\.)*(\\w+)
            // TestTODO: check if this regex is correct.
            MatchCollection matches = Regex.Matches(webPage, 
                                        @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");
            List<string> linkedUrls = new List<string>();
            foreach (Match match in matches)
            {
                linkedUrls.Add(match.Value);
            }
            return linkedUrls;
        }
    }
}
