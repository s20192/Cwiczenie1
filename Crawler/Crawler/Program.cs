// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Crawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string websiteurl;

            try
            {
                websiteurl = args[0];

            } catch(Exception)
            {
                throw new ArgumentNullException();
            }

           CheckUrl(websiteurl);
            
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(websiteurl);
                HashSet<string> mailMatches = new HashSet<string>();

                string pattern = @"([^\-\._\s\:\>][\w-\.]+[^\.])@((([\w-]+\.)+))([a-zA-Z]{2,4})";

                Regex regex = new Regex(pattern);
                MatchCollection matches = Regex.Matches(response, pattern);

                if(matches.Count==0)
                {
                    Console.WriteLine("Nie znaleziono żadnych adresów e-mail");
                } else
                {
                    foreach (Match m in matches)
                    {
                        mailMatches.Add(m.Value);
                    }

                    foreach (string s in mailMatches)
                    {
                        Console.WriteLine(s);
                    }
                }

                client.Dispose();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }
        }

        public static void CheckUrl(string websiteurl)
        {
            Uri uriResult;
            bool isUri = Uri.TryCreate(websiteurl, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUri)
            {
                throw new ArgumentException();
            }
        }
    }
}
