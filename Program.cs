using StockQuotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace StockPivots
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpClient _client = new HttpClient();
            List<string> quotes = new List<string>();

            if (args.Length < 1)
            { quotes.Add("msft"); }
            else
            {
                string[] data = args[0].Split(',');
                quotes.Add(data.ToString());
            }

            string APIKey = "GUUNDXU41QUOVFW9";
            StringBuilder response = new StringBuilder();

            for (int i = 0; i < quotes.Count; i++)
            {
                var url = string.Format(
               "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={0}&apikey={1}", args[i], APIKey);
                var uri = new Uri(url, UriKind.Absolute);
                response.Append(_client.GetStringAsync(uri).Result);
            }
            ParseWebResult sendToParser = new ParseWebResult(response);
            response.Clear();
        }
    }
}


