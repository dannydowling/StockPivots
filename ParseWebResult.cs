using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockQuotes
{
    internal class ParseWebResult
    {
        public ParseWebResult(StringBuilder response)
        {
            //quoteArray is a JArray that holds the response unordered
            var quoteArray = JArray.Parse(response.ToString());

            var quotes = quoteArray.Select(x => x["name"].ToString()).Distinct().OrderBy(x => x);

            var quoteDictionary = (Dictionary<string, List<QuoteClass>>)quotes.Select(q => new
            {
                QuoteName = q,
                Quotes = quoteArray.Where(x => x["name"].ToString() == q)
                         .Select(_ => new QuoteClass
                         {
                             //populate the Quote Class
                             Name = _["name"].ToString(),
                             Date = Convert.ToDateTime(_["date"]),
                             Open = Convert.ToInt32(_["open"]),
                             High = Convert.ToInt32(_["high"]),
                             Low = Convert.ToInt32(_["low"]),
                             Close = Convert.ToInt32(_["close"])
                         })
            });

            for (int j = 0; j < quoteDictionary.Count; j++)
            {
                // above we're counting the number of stock names in the dictionary
                //below we're counting the number of data points each stock name has
                var quoteList = quotes.OrderBy(quoteArray.Values).ToList();

                for (int h = 0; h < quoteDictionary.ElementAt(j).Value.Count; h++)
                {
                    List<QuoteClass> indexingQuotes = quoteDictionary.ElementAt(j).Value;
                    for (int i = 0; i < indexingQuotes.Count - 4; i++)
                    {
                        if (indexingQuotes[i].Open > indexingQuotes[i + 1].High && indexingQuotes[i].Close < indexingQuotes[i + 1].Low)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("{0}", quoteDictionary.ElementAt(j).Key);
                            Console.WriteLine("Pivot downside {0}", indexingQuotes[i].Date.ToShortDateString());
                        }
                        if (indexingQuotes[i].Open < indexingQuotes[i + 1].Low && indexingQuotes[i].Close > indexingQuotes[i + 1].High)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("{0}", quoteDictionary.ElementAt(j).Key);
                            Console.WriteLine("Pivot upside {0}", indexingQuotes[i].Date.ToShortDateString());
                        }
                    }
                }
            }
        }
    }
}

