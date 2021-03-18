using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class InforBillDAO
    {
        private static InforBillDAO instance;

        public static InforBillDAO Instance
        {
            get { if (instance == null) instance = new InforBillDAO(); return instance; }
            set => instance = value;
        }

        private InforBillDAO() { }
        public List<InforBill> getListInforBill(int id)
        {
            List<InforBill> listInforBill = new List<InforBill>();
            DataTable data = DataProvider.Instance.executeQuery("SELECT * FROM BillInfor WHERE idBill="+id);
            foreach (DataRow item in data.Rows)
            {
                InforBill inforBill = new InforBill(item);
                listInforBill.Add(inforBill);
            }
            return listInforBill;
        }
        public void insertInforBill(int idBill, int idFood, int countFood)
        {
            string query = "SP_InsertBillInfor @idBill , @idFood , @countFood";
            DataProvider.Instance.executeNonQuery(query, new object[] { idBill, idFood, countFood });
        }
        public void insertInforBillVer2(int idBill, int idFood, int countFood)
        {
            string query = "SP_UpdateBillInforByCountFoodDropped @idBill , @idFood , @countFood";
            DataProvider.Instance.executeNonQuery(query, new object[] { idBill, idFood, countFood });
        }
    }
}
