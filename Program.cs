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
            
            List<string> quotes = new List<string>();

            if (args == null)
            {
                args = new string[1];

                args[0] = "msft";
                quotes.Add("msft"); }
            else
            {
                string[] data = args[0].Split(',');
                quotes.Add(data.ToString());
            }

            string APIKey = "GUUNDXU41QUOVFW9";
            StringBuilder request = new StringBuilder();
            StringBuilder response = new StringBuilder();

            HttpClient _client = new HttpClient();
            for (int i = 0; i < quotes.Count; i++)
            {
                request.Append(string.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={0}&apikey={1}", args[i], APIKey));
                                
                var uri = new Uri(request.ToString(), UriKind.Absolute);
                response.Append(_client.GetStringAsync(uri).Result);
                request.Clear();
            }
            ParseWebResult sendToParser = new ParseWebResult(response);
            response.Clear();
        }
    }
}


