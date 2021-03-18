using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            set => instance = value;
        }

        public bool checkLogin(string userName, string passWord)
        {
            string query = "SP_checkLogin4 @user , @pass";
            DataTable data = DataProvider.Instance.executeQuery(query, new object[] { userName, passWord });

            return data.Rows.Count > 0;
        }
        public List<Account> loadAccount(string userName, string passWord)
        {
            List<Account> listAcc = new List<Account>();
            string query = "SP_checkLogin4 @user , @pass";
            DataTable data = DataProvider.Instance.executeQuery(query, new object[] { userName, passWord});
            foreach (DataRow item in data.Rows)
            {
                Account acc = new Account(item);
                listAcc.Add(acc);
            }
            return listAcc;
        }
    }
}
