using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class Food
    {
        
        private string nameFood;
        private float priceFood;
        private int id;
        private int idCategory;
        

        public Food(string nameFood, float priceFood,int id , int idCategory )
        {
            
            this.NameFood = nameFood;
            this.PriceFood = priceFood;
            this.Id = id;
            this.IdCategory = idCategory;
            
        }

        public Food(DataRow row)
        {
            
            this.NameFood = row["nameFood"].ToString();
            this.PriceFood = (float)Convert.ToDouble(row["priceFood"].ToString());
            this.Id = (int)row["id"];
            this.IdCategory = (int)row["idCategory"];
        }
        
        public string NameFood { get => nameFood; set => nameFood = value; }
        public float PriceFood { get => priceFood; set => priceFood = value; }
        public int Id { get => id; set => id = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        
    }
}
