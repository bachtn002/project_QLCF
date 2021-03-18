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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }
        bool login(string userName, string passWord)
        {

            return AccountDAO.Instance.checkLogin(userName, passWord);
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text;
            string passWord = textBoxPass.Text;
            if (login(userName, passWord))
            {
                fTableManager tableManager = new fTableManager();
                tableManager.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu !");
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
