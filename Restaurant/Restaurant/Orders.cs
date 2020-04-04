using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Orders
    {

        public IFoodRepository FoodRepo;
        public Orders(IFoodRepository FoodRepo)
        {
            this.FoodRepo = FoodRepo;
        }
        
        public List<OrderFormat> orders = new List<OrderFormat>();
        public Dictionary<string, int> items = new Dictionary<string, int>();
        public bool orderMore { get; set; }

        readonly double StarterPrice = 4.40;
        readonly double MainPrice = 7.00;
        public bool Success = false;
        public int i,index = 0;

        public double orderTotal { get; set; }


        public List<Food> GetAllFood()
        {
            return FoodRepo.SelectAllFood();
        }

        public List<OrderFormat> GetOrderedList()
        {
            return orders;
        }

        //Method to be used when you want to run the application as a console
        public void GetItem()
        {   
            Console.WriteLine("Enter Item");
            var item = Console.ReadLine();
            Console.WriteLine("Enter Quantity");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("-----------------------");
            if (!items.ContainsKey(item))
            {
                this.items.Add(item, quantity);
            }             

        }

        public void TakeOrder(bool Ordermore)
        {
            while (Ordermore)
            {
                this.GetItem();
                Ordermore = false;
            }  
        }

        public bool TakeOrder(string item, int quantity)
        {
            try
            {
                this.items.Add(item, quantity);
                return true;
            }

            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AddOrdersToList()
        {
            try
            {
                foreach (var item in items)
                {
                    var key = item.Key;
                    var value = item.Value;
                    AddOrder(key, value).Equals(true);
                    Console.WriteLine("OrderedItem : " + this.orders[index].OrderedItem);
                    Console.WriteLine("OrderPrice : " + this.orders[index].orderPrice);
                    Console.WriteLine("Quantity : " + this.orders[index].quantity);
                    index++;
                }
                return true;
            }

            catch(NullReferenceException e)
            {
                throw new Exception("Add all orders to orders List failed due to : " + e + "Search food is not available in menu");
            }

        }

        public bool AddOrder(string item, int Quantity)
        {
            try
            {
                if (this.GetFoodFromItem(item).Equals(true))
                {
                    var itemtype = this.GetItemType(item);
                    var itemprice = this.GetItemPrice(item);
                    var orderPrice = (itemprice * Quantity);
                    orders.Add(new OrderFormat { OrderedItem = item, quantity = Quantity, orderPrice = orderPrice });
                    i++;
                    return true;
                }
                return false;
            }

            catch(NullReferenceException e)
            {
                Console.WriteLine(($"Adding a food '{item}' to orders list failed as its not present in food menu : "), e);
                return false;
            }
        }

        public void UpdateOrder(string item1, string item2 = null, int quantity2 = 0 )
        {
            try
            {
                if (orders.FirstOrDefault(item => item.OrderedItem == item1).OrderedItem.Equals(item1))
                {
                    var result = orders.FirstOrDefault(item => item.OrderedItem == item1);
                    if (item2 == null)
                    {
                        var orderPrice = this.GetItemPrice(item1);
                        if (result.quantity != 0)
                        {
                            result.quantity = quantity2;
                            result.orderPrice = (orderPrice * quantity2);
                            Success = true;
                        } 
                    }
                    else                   
                    {
                        var orderPrice = this.GetItemPrice(item2);
                        if (this.GetFoodFromItem(item2).Equals(true))
                        {
                            result.OrderedItem = item2;
                            result.quantity = quantity2;
                            result.orderPrice = (orderPrice * quantity2);
                            Success = true;
                        }   
                    }                          
                }  
            }

            catch (Exception e)
            {
                Console.WriteLine(" Nothin to update, please provide correct item and quantity / Update failed due to : " + e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool RemoveOrder(string Item)
        {
            try
            {
                if (orders.FirstOrDefault(item => item.OrderedItem == Item).OrderedItem.Equals(Item))
                {
                    var removableitem = orders.FirstOrDefault(item => item.OrderedItem == Item);
                    orders.Remove(removableitem);
                    items.Remove(Item);
                    i--;
                    return orders.Any(item => item.OrderedItem == Item);
                }
                return true;
            }

            catch(Exception e)
            {
                throw new Exception("Remove order failed due to : " + e.Message);
            }
        }         

        public double CheckOut()
        {
            var ordertotal = orders.Sum(order => order.orderPrice);
            return ordertotal;
        }

        /// <summary>
        /// Use this below method to display the list of all items in order on console as of time when called.
        /// </summary>
        public void DisplayListOfOrderedItems()
        {
            Console.WriteLine();
            Console.WriteLine("Updated or Cancelled Items and Confirmed Order");
            for(int count = 0; count < i; count++)
            {
               
                Console.WriteLine("OrderedItem : " + this.orders[count].OrderedItem);
                Console.WriteLine("OrderPrice : " + this.orders[count].orderPrice);
                Console.WriteLine("Quantity : " + this.orders[count].quantity);
                
            }

        } 
        public bool AddListToFoods(string itemName, string type)
        {
            try
            {
                var result = FoodRepo.SelectAllFood();
                result.Add(new Food { item = itemName, type = type });
                return true;
            }

            catch(Exception e)
            {
                throw new Exception("Add new food to food list failed due to : " + e.Message);
            }
        }

        public bool RemoveListToFoods(string itemName)
        {
            try
            {
                var result = FoodRepo.SelectAllFood();
                result.Remove(new Food { item = itemName });
                return true;
            }

            catch (Exception e)
            {
                throw new Exception("Remove a food item from the food list failed due to : " + e.Message);
            }

        }                                     
        
        public bool GetFoodFromItem(string item)
        {
            var result = this.GetAllFood();
            return result.FirstOrDefault(sf => sf.item == item).item.Equals(item);
        }

        public string GetItemType(string item)
        {
            var result = this.GetAllFood();
            return result.FirstOrDefault(sf => sf.item == item).type;
        }

        public double GetItemPrice(string item)
        {
            var result = this.GetAllFood();
            if (result.FirstOrDefault(sf => sf.item == item).type == "starter")
                return StarterPrice;
            else
                return MainPrice;
        }
    }

    public class OrderFormat
    {
        public string OrderedItem { get; set; }
        public int quantity { get; set; }
        public double orderPrice { get; set; }
    }
}
