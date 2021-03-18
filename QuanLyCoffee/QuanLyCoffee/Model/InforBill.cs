using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class InforBill
    {
        private int id;
        private int idBill;
        private int idFood;
        private int countFood;

        public InforBill(int id, int idBill, int idFood, int countFood)
        {
            this.Id = id;
            this.IdBill = idBill;
            this.IdFood = idFood;
            this.CountFood = countFood;
        }

        public InforBill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdBill = (int)row["idBill"];
            this.IdFood = (int)row["idFood"];
            this.CountFood = (int)row["countFood"];

        }
        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int CountFood { get => countFood; set => countFood = value; }
    }
}
