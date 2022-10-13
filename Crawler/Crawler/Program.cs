// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text.RegularExpressions;

namespace Crawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                string websiteurl = args[0];
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(websiteurl);

                string pattern = @"([^\-\._\s\:\>][\w-\.]+[^\.])@((([\w-]+\.)+))([a-zA-Z]{2,4})";

                Regex regex = new Regex(pattern);
               // Match match = regex.Match(response);
                MatchCollection matches = Regex.Matches(response, pattern);

                foreach (Match m in matches)
                {
                    Console.WriteLine(m.Groups[0].Value);

                }
            }
            catch (Exception ex)
            {
                string error = "error " + ex.ToString();
            }
        }
    }
}
