using System;
using System.Threading.Tasks;
using ZendeskSearchManager;
using ZendeskSearchManager.Model;

namespace ZendeskCodeChallenge
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ISearchManager searchManager = new SearchManager.SearchManager();
            ISearchRequest searchRequest = new SearchRequest();
            searchRequest.SearchText = @"A Catastrophe in Korea (North)";

            searchManager.SearchAll(searchRequest);

            Console.ReadLine();
        }
    }
}