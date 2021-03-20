using QuanLyCoffee.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCoffee
{
    public partial class fThongKe : Form
    {
        public fThongKe()
        {
            InitializeComponent();
        }

        void loadListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
           dataGridView1.DataSource= BillDAO.Instance.getListBillByDate(dateCheckIn, dateCheckOut);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }
    }
}
