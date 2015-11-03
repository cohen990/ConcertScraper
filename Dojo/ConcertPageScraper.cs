using System;
using System.Linq;
using CsQuery;
using Tests;

namespace Dojo
{
    public class ConcertPageScraper : Scraper<Concert>
    {
        public ConcertPageScraper(IHttpClientWrapper httpClientWrapper, Uri uri) : base(httpClientWrapper, uri)
        {
        }

        protected override Concert Parse(CQ dom)
        {
            var result = new Concert();

            // This desperately needs refactoring - accessing it using knowledge of which index it is in the array is unnacceptable - but time!!! T_T
            result.Artist = dom.Select("h1")[1].InnerText;

            result.City = dom.Select(".venuetown").Text();
            result.Venue = dom.Select(".venuename").Text();
            result.Date = dom.Select(".VenueDetails h2").Text();
            result.Price = dom.Select(".eventPrice strong").Text(); // does not handle sale prices.


            return result;
        }
    }
}