using System;

namespace Dex.Scaper
{
    public class Program
    {
        private static void Main()
        {
            var scraper = new Scraper();
            scraper.Scrape();
            Environment.Exit(0);
        }
    }
}