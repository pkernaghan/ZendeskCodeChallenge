using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskSearchManager;
using ZendeskSearchManager.Model;

namespace ZendeskCodeChallenge
{
    public class Program
    {
        private const string TerminateCommand = @"EXIT";
        private static string userInput;

        private static async Task Main(string[] args)
        {
            try
            {
                PrintWelcome();
                ExecuteSearchApplication();
                TerminationPrompt();
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Zendesk Search is Terminating due to error. Please wait");
                Console.WriteLine(@"Error Details: " + ex.Message);
                Thread.Sleep(2000);
            }
            finally
            {
                Shutdown();
            }
        }


        private static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"========================================================================");
            Console.WriteLine(@"========================================================================");
            Console.WriteLine(@"=	Welcome to the Zendesk Code Test	         						");
            Console.WriteLine(@"=	Please submit text string to search the repository for matches.      ");
            Console.WriteLine(@"========================================================================");
            Console.WriteLine(@"========================================================================");
        }

        private static void TerminationPrompt()
        {
            Console.WriteLine(@"");
            Console.WriteLine(@"Press Enter/ Return to Exit....");
            Console.ReadLine();
        }

        private static void Shutdown()
        {
            Console.WriteLine(@"Shutting down Zendesk Search Application");
            Console.WriteLine(@"Please wait...");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        public static void ExecuteSearchApplication()
        {
            do
            {
                ISearchManager searchManager = new SearchManager.SearchManager();
                ISearchRequest searchRequest = new SearchRequest();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    var searchText = userInput.Trim().ToUpper();
                    searchRequest.SearchText = searchText;
                    searchManager.SearchAll(searchRequest);
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(@"Please Enter Search Text: ");
                userInput = Console.ReadLine();
            } while (userInput != TerminateCommand);
        }
    }
}