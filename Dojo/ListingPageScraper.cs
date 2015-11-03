using System;
using System.Linq;
using CsQuery;
using Dojo;

namespace Tests
{
    public class ListingPageScraper : Scraper<ListingPage>
    {
        public ListingPageScraper(IHttpClientWrapper httpClientWrapper, Uri uri) : base(httpClientWrapper, uri)
        {
        }

        protected override ListingPage Parse(CQ dom)
        {
            var results = dom.Select(".TicketListing a.event_link");

            // this needs proper null handling
            var uris = results.Select(result => new Uri(result.GetAttribute("href"))).ToList();

            string nextPage = dom.Select(".pagination_link_text.nextlink").FirstOrDefault()?.GetAttribute("href");

            Uri nextPageUri;

            Uri.TryCreate(nextPage, UriKind.Absolute, out nextPageUri);

            return new ListingPage(uris, nextPageUri);
        }
    }
}