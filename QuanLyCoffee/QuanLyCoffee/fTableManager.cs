using QuanLyCoffee.DAO;
using QuanLyCoffee.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCoffee
{
    public partial class fTableManager : Form
    {

        //BindingSource foodList = new BindingSource();
        public fTableManager()
        {

            InitializeComponent();
            loadData();
        }
        void loadData()
        {
            getNameTableForComboBox();
            loadTableCoffee();
            loadFoodCategory();
        }
        public void loadTableCoffee()
        {
            flowLayoutPanel1.Controls.Clear();
            List<TableCoffee> listTable = TableCoffeeDAO.Instance.loadTableCoffee();
            foreach (TableCoffee item in listTable)
            {
                Button btn = new Button();
                btn.Width = 105;
                btn.Height = 105;
                btn.Font = new Font("Times New Roman", 15);
                btn.Text = item.NameTable + Environment.NewLine + item.StatusTable;
                btn.Click += Btn_Click;
                btn.Tag = item;
                //Image image20 = Image.FromFile(@"C:\DATA\image\12.jpg");
                //btn.Image = System.Drawing.Image.FromFile(@"C:\Users\ADMIN\Pictures\Saved Pictures\coffee.jpg");
                switch (item.StatusTable)
                {
                    case "Trống":
                        btn.BackColor = Color.LightGreen;

                        break;
                    default:
                        btn.BackColor = Color.OrangeRed;
                        
                        break;
                }

                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        public void loadFood(int idCategory)
        {
            List<Food> listFood = FoodDAO.Instance.getFoodByIdCategory(idCategory);
            comboBoxFood.DataSource = listFood;
            comboBoxFood.DisplayMember = "nameFood";

        }

        public void loadFoodCategory()
        {

            List<FoodCategory> listFoodCategory = FoodCategoryDAO.Instance.getListCategory();
            comboBoxMenu.DataSource = listFoodCategory;
            comboBoxMenu.DisplayMember = "nameCategory";

        }

        void showDanhSachMon(int id)// đây là id Table
        {
            listView1.Items.Clear();
            List<DanhSachMon> listDS = DanhSachMonDAO.Instance.getListDSMon(id);
            long totalPrice = 0;
            foreach (DanhSachMon item in listDS)
            {
                ListViewItem listViewItem = new ListViewItem(item.NameFood.ToString());
                listViewItem.SubItems.Add(item.CountFood.ToString());
                listViewItem.SubItems.Add(item.PriceFood.ToString());
                listViewItem.SubItems.Add(item.PriceTotal.ToString());
                totalPrice = totalPrice + item.PriceTotal;
                listView1.Items.Add(listViewItem);
            }
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            textBoxTongTien.Text = totalPrice.ToString("c", cultureInfo);
        }

      /*  void showDanhSachMon1(int id)// đây là id Table
        {
            listView1.Items.Clear();
            List<DanhSachMon> listDS = DanhSachMonDAO.Instance.getListDSMon(id);
            long totalPrice = 0;
            foreach (DanhSachMon item in listDS)
            {
                ListViewItem listViewItem = new ListViewItem(item.NameFood.ToString());
                listViewItem.SubItems.Add(item.CountFood.ToString());
                listViewItem.SubItems.Add(item.PriceFood.ToString());
                listViewItem.SubItems.Add(item.PriceTotal.ToString());
                totalPrice = totalPrice + item.PriceTotal;
                listView1.Items.Add(listViewItem);
            }
            //CultureInfo cultureInfo = new CultureInfo("vi-VN");
            //textBoxTongTien.Text = totalPrice.ToString("c", cultureInfo);
        }    */

        void getNameTableForComboBox()
        {
            List<TableCoffee> list = TableCoffeeDAO.Instance.loadTableCoffee();

            comboBoxChuyenBan.DataSource = list;
            comboBoxChuyenBan.DisplayMember = "nameTable";

        }
        void getNameTable(int id)
        {
            List<TableCoffee> list = TableCoffeeDAO.Instance.loadNameTable(id);
            foreach (TableCoffee item in list)
            {
                textBox9.Text = item.NameTable;
                textBoxViTriBan.Text = item.NameTable;

            }
        }

        void getDateCheckIn(int idBill)
        {
            List<Bill> list = BillDAO.Instance.getBill(idBill);
            foreach (Bill item in list)
            {
                textBoxCheckIn.Text = Convert.ToDateTime(item.DateCheckIn).ToString();
            }
        }

        void getDateCheckIn_ver2()
        {
            DateTime dateTime = DateTime.Now;
            textBoxCheckIn.Text = Convert.ToDateTime(dateTime).ToString("G");
        }

        void getDateCheckOut_ver2()
        {
            DateTime dateTime = DateTime.Now;
            textBoxCheckOut.Text = Convert.ToDateTime(dateTime).ToString("G");
        }
        void updateStatusTable(int id)
        {

        }

        /*void addBindingFood()
        {
            textBoxTenMon.DataBindings.Add(new Binding("Text", dataGridViewMenu.DataSource, "NameFood"));
            textBoxGia.DataBindings.Add(new Binding("Text", dataGridViewMenu.DataSource, "PriceFood"));
        }*/

        #region Event
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            TableCoffee tableCoffee = listView1.Tag as TableCoffee;
            int idBill = BillDAO.Instance.getIdBillByIdTable(tableCoffee.Id);
            int discountCoffee = (int)numericUpDown2.Value;

            double totalPrice = Convert.ToDouble(textBoxTongTien.Text.Split(',')[0].Replace(".", ""));
            double finalTotalPrice = totalPrice - ((totalPrice / 100) * discountCoffee);
            if (idBill != -1)
            {

                if (MessageBox.Show("Bạn có chắc muốn thanh toán cho " + tableCoffee.NameTable + "\nTổng tiền = " + finalTotalPrice + " VNĐ", "Thông Báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {

                    textBox9.Clear();
                    buttonThem.Enabled = false;
                    buttonGiam.Enabled = false;
                    buttonXoa.Enabled = false;
                    buttonXuatBill.Enabled = false;
                    buttonBatDau.Enabled = false;
                    buttonHuyBan.Enabled = false;
                    buttonKetThuc.Enabled = false;
                    buttontTachBan.Enabled = false;
                    buttonGopBan.Enabled = false;
                    buttonChuyenBan.Enabled = false;
                    button7.Enabled = false;
                    CultureInfo cultureInfo = new CultureInfo("vi-VN");
                    label17.Text = finalTotalPrice.ToString("c", cultureInfo);
                    BillDAO.Instance.checkOut(idBill, discountCoffee, (float)finalTotalPrice);
                    //showDanhSachMon1(tableCoffee.Id);
                    loadTableCoffee();
                    getDateCheckOut_ver2();
                }
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            button7.Enabled = true;
            buttonThem.Enabled = true;
            buttonGiam.Enabled = true;
            buttonXoa.Enabled = true;
            buttonXuatBill.Enabled = true;
            buttonBatDau.Enabled = true;
            buttonChuyenBan.Enabled = true;
            buttonGopBan.Enabled = true;
            buttonHuyBan.Enabled = true;
            buttontTachBan.Enabled = true;
            buttonKetThuc.Enabled = true;

            int idTable = ((sender as Button).Tag as TableCoffee).Id;
            int idBill = BillDAO.Instance.getIdBillByIdTable(idTable);
            listView1.Tag = ((sender as Button)).Tag;
            getNameTable(idTable);
            showDanhSachMon(idTable);
            label17.ResetText();
            textBoxCheckIn.Clear();
            textBoxCheckOut.Clear();
            getDateCheckIn(idBill);

        }

        private void batDau_Click(object sender, EventArgs e)
        {
            // update status của bảng TableCoffee từ 0 --> 1 theo id TableCoffee
            // int idTable = ((sender as Button).Tag as TableCoffee).Id;
            //  updateStatusTable(idTable);

        }

        private void comboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

            int idCategory = 0;
            ComboBox cb = sender as ComboBox;
            FoodCategory selected = cb.SelectedItem as FoodCategory;
            idCategory = selected.Id;
            loadFood(idCategory);

        }

        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login f = new Login();
            f.Show();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {

            buttonHuyBan.Enabled = true;
            buttonKetThuc.Enabled = true;
            buttontTachBan.Enabled = true;
            buttonGopBan.Enabled = true;
            buttonChuyenBan.Enabled = true;

            TableCoffee tableCoffee = listView1.Tag as TableCoffee;
            int idBill = BillDAO.Instance.getIdBillByIdTable(tableCoffee.Id);

            int idFood = (comboBoxFood.SelectedItem as Food).Id;
            int countFood = (int)numericUpDown1.Value;
            if (idBill == -1)
            {
                getDateCheckIn_ver2();
                BillDAO.Instance.insertBill(tableCoffee.Id);
                InforBillDAO.Instance.insertInforBill(BillDAO.Instance.getMaxIdBill(), idFood, countFood);

            }
            else
            {
                InforBillDAO.Instance.insertInforBill(idBill, idFood, countFood);

            }


            showDanhSachMon(tableCoffee.Id);
            loadTableCoffee();
            textBoxCheckOut.Clear();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonGiam_Click(object sender, EventArgs e)
        {
            TableCoffee table = listView1.Tag as TableCoffee;
            int idBill = BillDAO.Instance.getIdBillByIdTable(table.Id);

            int idFood = (comboBoxFood.SelectedItem as Food).Id;

            int countFood = (int)numericUpDown1.Value;
            if (idBill == -1)
            {
                buttonGiam.Enabled = true;

            }
            else
            {
                InforBillDAO.Instance.insertInforBillVer2(idBill, idFood, countFood);
                showDanhSachMon(table.Id);
                loadTableCoffee();
                textBoxCheckOut.Clear();
            }
        }

        private void buttonChuyenBan_Click(object sender, EventArgs e)
        {
            int idTable1 = (listView1.Tag as TableCoffee).Id;
            int idTable2 = (comboBoxChuyenBan.SelectedItem as TableCoffee).Id;
            if (MessageBox.Show(String.Format("Bạn có muốn chuyển bàn {0} qua bàn {1}", (listView1.Tag as TableCoffee).NameTable, (comboBoxChuyenBan.SelectedItem as TableCoffee).NameTable),
                "Thong bao", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableCoffeeDAO.Instance.switchTable(idTable1, idTable2);
            }
            loadTableCoffee();
            showDanhSachMon(idTable2);
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongKe f = new fThongKe();
            f.Show();
        }

        #endregion

        
    }
}
