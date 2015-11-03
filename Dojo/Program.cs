using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tests;

namespace Dojo
{
	class Program
	{

        static void Main(string[] args)
        {
            var result = Task.Run(() => Scrape());
            while (!result.IsCompleted)
            {
            }

            Console.Out.WriteLine("Done");
            Console.In.ReadLine();
        }

        static async Task Scrape()
		{
            Console.Out.WriteLine("Starting...");

            var client = new HttpClientWrapper();

            var scraper = new ListingPageScraper(client, new Uri("http://www.wegottickets.com/searchresults/page/0/latest"));

		    ListingPage concertListings = await scraper.Scrape();

		    foreach (var listing in concertListings.Uris)
		    {
		        var concertPageScraper = new ConcertPageScraper(client, listing);

		        var concert = await concertPageScraper.Scrape();

                Console.Out.WriteLine($"{concert.Artist} playing at {concert.City} {concert.Venue}, on {concert.Date}. Tickets for {concert.Price}.");
		    }

            return;
		}
	}

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client = new HttpClient();

        public Task<string> GetStringAsync(Uri uri)
        {
            return _client.GetStringAsync(uri);
        }
    }

    public interface IHttpClientWrapper
    {
        Task<string> GetStringAsync(Uri uri);
    }
}
