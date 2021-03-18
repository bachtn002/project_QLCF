using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            set => instance = value;
        }

        public List<Food> getListFood()
        {
            List<Food> listFood = new List<Food>();
            string query = "SELECT * FROM Food";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }

        public List<Food> getFoodByIdCategory(int idCategory)
        {
            List<Food> listFood = new List<Food>();
            
            DataTable data = DataProvider.Instance.executeQuery("SELECT * FROM Food	WHERE idCategory="+idCategory);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }
    }
}
