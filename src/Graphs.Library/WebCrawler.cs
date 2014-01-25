﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        private IEnumerable<string> ExtractUrls(string webPage)
        {
            // regexp for url: http://(\\w+\\.)*(\\w+)
            throw new NotImplementedException();
        }
    }
}
