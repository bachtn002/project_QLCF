using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class FoodCategoryDAO
    {
        private static FoodCategoryDAO instance;

        public static FoodCategoryDAO Instance
        {
            get { if (instance == null) instance = new FoodCategoryDAO(); return instance; }
            set => instance = value;
        }
        public List<FoodCategory> getListCategory()
        {
            List<FoodCategory> listCategory = new List<FoodCategory>();
            string query = "SELECT * FROM FoodCategory";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                FoodCategory foodCategory = new FoodCategory(item);
                listCategory.Add(foodCategory);
            }
            return listCategory;
        }
    }
}
