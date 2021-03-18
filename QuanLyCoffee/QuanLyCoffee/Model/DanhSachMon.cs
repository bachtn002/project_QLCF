using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class DanhSachMon
    {
        private string nameFood;
        private int countFood;
        private float priceFood;
        private long priceTotal;

        public DanhSachMon(string nameFood, int countFood, float priceFood, long  priceTotal)
        {
            this.NameFood = nameFood;
            this.CountFood = countFood;
            this.PriceFood = priceFood;
            this.PriceTotal = priceTotal;
        }

        public DanhSachMon(DataRow row)
        {
            this.NameFood = row["nameFood"].ToString();
            this.CountFood = (int)row["countFood"];
            this.PriceFood = (float)Convert.ToDouble(row["priceFood"].ToString());
            this.PriceTotal = (long )Convert.ToDouble(row["priceTotal"].ToString());

        }
        public string NameFood { get => nameFood; set => nameFood = value; }
        public int CountFood { get => countFood; set => countFood = value; }
        public float PriceFood { get => priceFood; set => priceFood = value; }
        public long PriceTotal { get => priceTotal; set => priceTotal = value; }
    }
}
