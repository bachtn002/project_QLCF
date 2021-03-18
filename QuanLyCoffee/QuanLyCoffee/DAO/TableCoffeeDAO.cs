using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class TableCoffeeDAO
    {
        private static TableCoffeeDAO instance;

        public static TableCoffeeDAO Instance
        {
            get { if (instance == null) instance = new TableCoffeeDAO(); return instance; }
            set => instance = value;
        }

        public List<TableCoffee> loadTableCoffee()
        {
            List<TableCoffee> listTable = new List<TableCoffee>();
            string query = "SP_ListTableCoffee";
            DataTable data = DataProvider.Instance.executeQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TableCoffee tableCoffee = new TableCoffee(item);
                listTable.Add(tableCoffee);
            }
            return listTable;
        }

        public List<TableCoffee> loadNameTable(int id)
        {
            List<TableCoffee> list = new List<TableCoffee>();
            string query = "SP_loadNameTable @id";
            DataTable data = DataProvider.Instance.executeQuery(query, new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                TableCoffee tableCoffee = new TableCoffee(item);
                list.Add(tableCoffee);
            }
            return list;
        }

        public void switchTable(int idTable1, int idTable2)
        {
            DataProvider.Instance.executeQuery("SP_SwitchTable @idTable1 , @idTable2", new object[] { idTable1, idTable2 }); ;
        }
    }
}
