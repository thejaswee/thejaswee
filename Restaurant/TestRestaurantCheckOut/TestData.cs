using Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestaurantCheckOut
{
    public class TestData  : ITestData
    {
        public List<OrderFormat> data = new List<OrderFormat>();
        public List<OrderFormat> GetData()
        {
            data.Add(new OrderFormat { OrderedItem = "Vada", orderPrice = 8.8, quantity = 2 });
            data.Add(new OrderFormat { OrderedItem = "Samosa", orderPrice = 4.4, quantity = 1 });
            data.Add(new OrderFormat { OrderedItem = "NorthIndianThaali", orderPrice = 7, quantity = 1 });
            data.Add(new OrderFormat { OrderedItem = "ParathaKurma", orderPrice = 14, quantity = 2 });
            data.Add(new OrderFormat { OrderedItem = "InvalidFood", orderPrice = 3, quantity = 1 });
            return data;
        }
    }

    public interface ITestData
    {
        List<OrderFormat> GetData();
    }
}
