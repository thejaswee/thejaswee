using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestaurantCheckOut
{
    public class TestCases
    {
        public ITestData testData;

        public TestCases(ITestData testData)
        {
            this.testData = testData;
        }

        public List<OrderFormat> data, allOrders;
        public Orders orders = new Orders(new FoodRepository());
        public bool AddOrderTest()
        {   
            data = testData.GetData();
            Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
            Assert.IsTrue(orders.TakeOrder(data[1].OrderedItem, data[1].quantity));
            Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
            Assert.IsTrue(orders.TakeOrder(data[3].OrderedItem, data[3].quantity));
            Assert.IsTrue(orders.AddOrdersToList());
            allOrders = orders.GetOrderedList();
            Assert.AreEqual(allOrders[0].OrderedItem, data[0].OrderedItem);
            Assert.AreEqual(allOrders[0].orderPrice, data[0].orderPrice);
            Assert.AreEqual(allOrders[0].quantity, data[0].quantity);
            Assert.AreEqual(allOrders[1].OrderedItem, data[1].OrderedItem);
            Assert.AreEqual(allOrders[1].orderPrice, data[1].orderPrice);
            Assert.AreEqual(allOrders[1].quantity, data[1].quantity);
            Assert.AreEqual(allOrders[3].OrderedItem, data[3].OrderedItem);
            Assert.AreEqual(allOrders[3].orderPrice, data[3].orderPrice);
            Assert.AreEqual(allOrders[3].quantity, data[3].quantity);
            this.TestDataClear();
            return true;
            ///Assert.AreEqual(allOrders[0], data[0]);
            ///Assert.IsTrue(allOrders.Contains(new OrderFormat{ OrderedItem = "Samosa", quantity = 1, orderPrice = 4.4 }));
        }

        public bool UpdateOrderTest()
        {
            data = testData.GetData();
            Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
            Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
            Assert.IsTrue(orders.AddOrdersToList());
            allOrders = orders.GetOrderedList();
            Assert.AreEqual(allOrders[0].OrderedItem, data[0].OrderedItem);
            Assert.AreEqual(allOrders[0].orderPrice, data[0].orderPrice);
            Assert.AreEqual(allOrders[0].quantity, data[0].quantity);
            Assert.AreEqual(allOrders[1].OrderedItem, data[2].OrderedItem);
            Assert.AreEqual(allOrders[1].orderPrice, data[2].orderPrice);
            Assert.AreEqual(allOrders[1].quantity, data[2].quantity);
            ////The below 2 steps updates the Order
            orders.UpdateOrder(data[0].OrderedItem, null, 3); ////Update the quantity alone
            orders.UpdateOrder(data[2].OrderedItem, data[3].OrderedItem, data[3].quantity); ////Updated the item and the quantity
            allOrders = orders.GetOrderedList();
            Assert.AreNotEqual(allOrders[0].orderPrice, data[0].orderPrice);
            Assert.AreNotEqual(allOrders[0].quantity, data[0].quantity);
            Assert.AreEqual(allOrders[0].quantity, 3); ///checks qantoty is updated
                                                       ///Below steps asserts that product is not the same prior order list indes, but updated to new item
            Assert.AreNotEqual(allOrders[1].OrderedItem, data[1].OrderedItem);
            Assert.AreNotEqual(allOrders[1].orderPrice, data[1].orderPrice);
            Assert.AreNotEqual(allOrders[1].quantity, data[1].quantity);
            Assert.AreEqual(allOrders[1].OrderedItem, data[3].OrderedItem);
            Assert.AreEqual(allOrders[1].orderPrice, data[3].orderPrice);
            Assert.AreEqual(allOrders[1].quantity, data[3].quantity);
            this.TestDataClear();
            return true;
        }

        public bool RemoveOrderTest()
        {
            data = testData.GetData();
            Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
            Assert.IsTrue(orders.TakeOrder(data[1].OrderedItem, data[1].quantity));
            Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
            Assert.IsTrue(orders.TakeOrder(data[3].OrderedItem, data[3].quantity));
            Assert.IsTrue(orders.AddOrdersToList());
            allOrders = orders.GetOrderedList();
            Assert.AreEqual(allOrders.Count, 4);
            ////Remove orders from orders list
            orders.RemoveOrder(data[1].OrderedItem);
            orders.RemoveOrder(data[2].OrderedItem);
            Assert.AreEqual(allOrders[0].OrderedItem, data[0].OrderedItem);
            Assert.AreEqual(allOrders[0].orderPrice, data[0].orderPrice);
            Assert.AreEqual(allOrders[0].quantity, data[0].quantity);
            Assert.AreEqual(allOrders[1].OrderedItem, data[3].OrderedItem);
            Assert.AreEqual(allOrders[1].orderPrice, data[3].orderPrice);
            Assert.AreEqual(allOrders[1].quantity, data[3].quantity);
            allOrders = orders.GetOrderedList();
            Assert.AreEqual(allOrders.Count, 2);
            this.TestDataClear();
            return true;
        }

        public bool CheckOutTest()
        {
            data = testData.GetData();
            Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
            Assert.IsTrue(orders.TakeOrder(data[1].OrderedItem, data[1].quantity));
            Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
            Assert.IsTrue(orders.TakeOrder(data[3].OrderedItem, data[3].quantity));
            Assert.IsTrue(orders.AddOrdersToList());
            allOrders = orders.GetOrderedList();
            Assert.AreEqual(orders.CheckOut(), 34.2);///Checks the checkoutprice
            this.TestDataClear();
            return true;
        }

        public bool AddUpdateRemoveCheckOutTest()
        {
            data = testData.GetData();
            Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
            Assert.IsTrue(orders.TakeOrder(data[1].OrderedItem, data[1].quantity));
            Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
            Assert.IsTrue(orders.TakeOrder(data[3].OrderedItem, data[3].quantity));
            Assert.IsTrue(orders.AddOrdersToList());
            orders.UpdateOrder("Samosa", "SouthIndianThaali", 2); //update the order
            orders.RemoveOrder("NorthIndianThaali"); //removes from order
            orders.AddOrder("PaneerTikka", 2);///Adds an extra order to the orders list
            allOrders = orders.GetOrderedList();  ///gets the updated and removed confirmed order
            Assert.AreEqual(allOrders[0].OrderedItem, "Vada");
            Assert.AreEqual(allOrders[1].OrderedItem, "SouthIndianThaali");
            Assert.AreEqual(allOrders[2].OrderedItem, "ParathaKurma");
            Assert.AreEqual(allOrders[3].OrderedItem, "PaneerTikka");
            Assert.AreEqual(orders.CheckOut(), 45.6, 0.000000001);
            this.TestDataClear();
            return true;
        }

        public bool AddUnavailableFoodOrderTest()
        {
            try
            {
                data = testData.GetData();
                Assert.IsTrue(orders.TakeOrder(data[4].OrderedItem, data[4].quantity));
                Assert.IsTrue(orders.AddOrdersToList());
                allOrders = orders.GetOrderedList();
                Assert.AreEqual(allOrders[0].OrderedItem, data[4].OrderedItem);
                Assert.AreEqual(allOrders[0].orderPrice, data[4].orderPrice);
                Assert.AreEqual(allOrders[0].quantity, data[4].quantity);
                this.TestDataClear();
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateUnavailableFoodOrderTest()
        {
            try
            {
                data = testData.GetData();
                Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
                Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
                Assert.IsTrue(orders.AddOrdersToList());
                allOrders = orders.GetOrderedList();
                ////The below 2 steps updates the Order
                orders.UpdateOrder(data[0].OrderedItem, null, 3); ////Update the quantity alone
                orders.UpdateOrder(data[2].OrderedItem, data[4].OrderedItem, data[4].quantity); ////Updated the item and the quantity
                allOrders = orders.GetOrderedList();
                this.TestDataClear();
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool RemoveUnavailableOrderTest()
        {
            try
            {
                data = testData.GetData();
                Assert.IsTrue(orders.TakeOrder(data[0].OrderedItem, data[0].quantity));
                Assert.IsTrue(orders.TakeOrder(data[1].OrderedItem, data[1].quantity));
                Assert.IsTrue(orders.TakeOrder(data[2].OrderedItem, data[2].quantity));
                Assert.IsTrue(orders.TakeOrder(data[3].OrderedItem, data[3].quantity));
                Assert.IsTrue(orders.AddOrdersToList());
                allOrders = orders.GetOrderedList();
                Assert.AreEqual(allOrders.Count, 4);
                ////Remove orders from orders list
                orders.RemoveOrder(data[1].OrderedItem);
                orders.RemoveOrder(data[5].OrderedItem);
                Assert.AreEqual(allOrders[0].OrderedItem, data[0].OrderedItem);
                Assert.AreEqual(allOrders[0].orderPrice, data[0].orderPrice);
                Assert.AreEqual(allOrders[0].quantity, data[0].quantity);
                Assert.AreEqual(allOrders[1].OrderedItem, data[3].OrderedItem);
                Assert.AreEqual(allOrders[1].orderPrice, data[3].orderPrice);
                Assert.AreEqual(allOrders[1].quantity, data[3].quantity);
                allOrders = orders.GetOrderedList();
                Assert.AreEqual(allOrders.Count, 2);
                this.TestDataClear();
                return true;
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void TestDataClear()
        {
            data.Clear();
            allOrders.Clear();
        }
    }
}
