using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class FoodRepository : IFoodRepository
    {
        public List<Food> SelectAllFood()
        {

        List<Food> foods = new List<Food>
        {
            new Food
            {
                item = "Vada" ,
                type = "starter"
            } ,

            new Food
            {
                item = "PaneerTikka",
                type = "starter"
            } ,

            new Food
            {
                item = "Samosa",
                type = "starter"
            },

            new Food
            {
                item = "ParathaKurma" ,
                type = "mains"
            } ,

            new Food
            {
                item = "NorthIndianThaali",
                type = "mains"
            } ,

            new Food
            {
                item = "SouthIndianThaali",
                type = "mains"
            }

            };

            return foods;
        }  
       
    }
    public interface IFoodRepository
    {
        List<Food> SelectAllFood();      

    }
}

