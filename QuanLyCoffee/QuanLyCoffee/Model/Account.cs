using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class Account
    {
        private string userName;
        private string passWord;
        private string displayName;

        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public Account(string userName, string passWord, string displayName)
        {
            this.UserName = userName;
            this.PassWord = passWord;
            this.DisplayName = displayName;
        }
        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.PassWord = row["passWord"].ToString();
            this.DisplayName = row["displayName"].ToString();
        }
    }
}
