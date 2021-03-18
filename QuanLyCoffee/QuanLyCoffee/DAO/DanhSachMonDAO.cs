using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class DanhSachMonDAO
    {
        private static DanhSachMonDAO instance;

        public static DanhSachMonDAO Instance
        {
            get { if (instance == null) instance = new DanhSachMonDAO(); return instance; }
            set => instance = value;
        }

        public List<DanhSachMon> getListDSMon(int idTable)
        {
            List<DanhSachMon> listDSM = new List<DanhSachMon>();
            string query = "SELECT F.nameFood, BI.countFood, F.priceFood, F.priceFood*BI.countFood AS priceTotal FROM Bill AS B, BillInfor AS BI, Food AS F , TableCoffee AS T WHERE BI.idBill=B.id AND BI.idFood=F.id AND F.id=BI.idFood AND B.idTable= T.id AND B.statusBill=0 AND T.id=" + idTable;
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DanhSachMon ds = new DanhSachMon(item);
                listDSM.Add(ds);
            }

            return listDSM;
        }
    }
}
