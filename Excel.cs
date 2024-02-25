using ExcelDataReader;
using StockQuotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPivots
{
    internal class Excel
    {
        public Dictionary<string, List<QuoteClass>> ReadExcel()
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var inFilePath = "Stocks.xlsx";

            var AllStocks = new Dictionary<string, List<QuoteClass>>();
            var unsortedSpecificStock = new List<QuoteClass>();

            //open the file for reading
            using (var inFile = File.Open(inFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration() { FallbackEncoding = Encoding.GetEncoding(1252) }))

                                     
                    //try to read the whole document and make classes from it
                    while (reader.Read())
                    {
                        unsortedSpecificStock.Add(new QuoteClass()

                        {
                            //populate the Quote Class
                            Name = "name".ToString(),
                            Date = Convert.ToDateTime("date"),
                            Open = Convert.ToInt32("open"),
                            High = Convert.ToInt32("high"),
                            Low = Convert.ToInt32("low"),
                            Close = Convert.ToInt32("close")

                        });
                        AllStocks.Add("name".Distinct().ToString(), unsortedSpecificStock);
                        reader.Read();
                    }
            }

            return (Dictionary<string, List<QuoteClass>>)AllStocks.OrderBy(x => x);
        }
    }
}


