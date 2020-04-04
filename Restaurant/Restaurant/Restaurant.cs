using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Restaurant
    {
        public static double checkOutAmt;
        public static void Main(string[] args)
        {
            Orders s = new Orders(new FoodRepository());
            
            s.TakeOrder(true);
            s.TakeOrder(true);
            s.TakeOrder(true);
            s.AddOrdersToList();
            checkOutAmt = s.CheckOut();
            Console.WriteLine("Total Order Pric : £{0}", checkOutAmt);
            s.RemoveOrder("Vada");
            s.RemoveOrder("Samosa");
            s.DisplayListOfOrderedItems();
            s.AddOrder("Vada", 2);
            s.AddOrder("Samosa", 2);
            ///s.AddOrdersToList();
            s.DisplayListOfOrderedItems();
            s.UpdateOrder("Vada", null, 3);
            s.UpdateOrder("Samosa", "ParathaKurma", 2);
            s.DisplayListOfOrderedItems();
            checkOutAmt = s.CheckOut();
            Console.WriteLine("Total Order Pric : £{0}", checkOutAmt);
            Console.ReadKey();
        }         
    }
}
