using System;
using System.Threading.Tasks;
using CsQuery;
using Dojo;

namespace Tests
{
    public abstract class Scraper<T>
    {
        protected IHttpClientWrapper HttpClientClient;
        protected readonly Uri Uri;

        protected Scraper(IHttpClientWrapper httpClientWrapper, Uri uri)
        {
            HttpClientClient = httpClientWrapper;
            Uri = uri;
        }

        public async Task<T> Scrape()
        {
            var content = await HttpClientClient.GetStringAsync(Uri);

            var dom = new CQ(content);

            return Parse(dom);
        }

        protected abstract T Parse(CQ dom);
    }
}