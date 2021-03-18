using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            set => instance = value;
        }
        public int getIdBillByIdTable(int idTable)
        {
            //string query = "SP_GetIdBillByIdTable @IdTable";
            DataTable data = DataProvider.Instance.executeQuery("SELECT * FROM Bill WHERE idTable="+idTable+" AND statusBill=0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

        public void  insertBill(int idTable)
        {
            string query = "SP_InsertBill @idTable";
            DataProvider.Instance.executeNonQuery(query, new object[] { idTable });
        }
        

        public int getMaxIdBill()
        {
            try
            {
               return (int)DataProvider.Instance.executeScalar("SELECT MAX(id) FROM Bill");
            }
            catch (Exception)
            {

                return 1;
            }       
        }

        public void checkOut(int idBill, int discountCoffee, float totalPrice)
        {
            string query = "UPDATE Bill SET statusBill=1, dateCheckOut=GETDATE() , "+"discountCoffee = "+discountCoffee+", totalPrice = "+totalPrice+" WHERE id = " + idBill;
            DataProvider.Instance.executeNonQuery(query);
        }

        public List<Bill> getBill(int id)
        {
            List<Bill> listBill = new List<Bill>();
            string query = "SELECT * FROM Bill WHERE id="+id;
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows) 
            {
                Bill bill = new Bill(item);
                listBill.Add(bill);
            }
            return listBill;
        }
        public DataTable getListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            return DataProvider.Instance.executeQuery("SP_GetListBillByDate @dateChekIn , @dateCheckOut", new object[] { dateCheckIn, dateCheckOut });

        }
    }
}
