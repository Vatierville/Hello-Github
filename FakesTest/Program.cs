using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesTest
{
    public interface IStockFeed
    {
        int GetSharePrice(string company);
    }

    public class StockFeed : IStockFeed
    {
        public int GetSharePrice(string company)
        {
            return 666;
        }
    }

    public class StockAnalyzer
    {
        private IStockFeed stockFeed;
        public StockAnalyzer(IStockFeed feed)
        {
            stockFeed = feed;
        }
        public int GetContosoPrice()
        {
            return stockFeed.GetSharePrice("COOO");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
