using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class Bill
    {
        private int id;
        private DateTime dateCheckIn;
        private DateTime? dateCheckOut;
        private int idTable;
        private int statusBill;
        private int discountCoffee;

        public Bill(int id, DateTime dateCheckIn, DateTime? dateCheckOut, int idTable, int statusBill, int discountCoffee )
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.IdTable = idTable;
            this.StatusBill = statusBill;
            this.DiscountCoffee = discountCoffee;
        }

        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];  // lấy ra từng ô giá trị trong bảng cơ sở dữ liệu theo tên cột 
            this.DateCheckIn = (DateTime)row["dateCheckIn"];

            var DateCheckOutTemp = row["dateCheckOut"];

           if (DateCheckOutTemp.ToString() != "") { this.DateCheckOut = (DateTime?)DateCheckOutTemp; }

            //this.DateCheckOut = (DateTime?)row["dateCheckOut"];

            this.IdTable = (int)row["idTable"];

            this.StatusBill = (int)row["statusBill"];
            this.DiscountCoffee = (int)row["discountCoffee"];
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int IdTable { get => idTable; set => idTable = value; }
        public int StatusBill { get => statusBill; set => statusBill = value; }
        public int DiscountCoffee { get => discountCoffee; set => discountCoffee = value; }
    }
}
