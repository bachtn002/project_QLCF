using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.Model
{
    public class TableCoffee
    {
        private int id;
        private string nameTable;
        private string statusTable;

        public int Id { get => id; set => id = value; }
        public string NameTable { get => nameTable; set => nameTable = value; }
        public string StatusTable { get => statusTable; set => statusTable = value; }

        public TableCoffee(int id, string nameTable, string statusTable)
        {
            this.Id = id;
            this.NameTable = nameTable;
            this.StatusTable = statusTable;
        }

        public TableCoffee(DataRow row)
        {
            this.Id = (int)row["id"];
            this.NameTable = row["nameTable"].ToString();
            this.StatusTable = row["statusTable"].ToString();

        }
    }
}
