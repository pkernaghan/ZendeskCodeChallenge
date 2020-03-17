using System;
using System.Threading.Tasks;
using ZendeskSearchRepository;
using ZendeskSearchRepository.Models;


namespace ZendeskCodeChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var searchRepository = new SearchRepository();

            ISearchRequestData requestData = new SearchRequestData();
            requestData.SearchString = @"Xylar";

            var results = await searchRepository.SearchAll(requestData);

            Console.ReadLine();
        }
    }
}
