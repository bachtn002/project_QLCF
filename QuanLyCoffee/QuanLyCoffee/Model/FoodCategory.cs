using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class FoodCategory
    {
        private int id;
        private string nameCategory;

        public FoodCategory(int id, string nameCategory)
        {
            this.Id = id;
            this.NameCategory = nameCategory;
        }
        public FoodCategory(DataRow row)
        {
            this.Id = (int)row["id"];
            this.NameCategory = row["nameCategory"].ToString();
        }
        public int Id { get => id; set => id = value; }
        public string NameCategory { get => nameCategory; set => nameCategory = value; }
    }
}
