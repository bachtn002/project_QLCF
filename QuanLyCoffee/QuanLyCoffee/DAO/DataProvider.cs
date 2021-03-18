using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCoffee.DAO
{

    public class DataProvider
    {
        string connectionString = "Data Source=DESKTOP-2RNHE1C\\SQLEXPRESS;Initial Catalog=qlcaphe;Integrated Security=True";
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            set => instance = value;
        }

        public DataTable executeQuery(string query, object[] parameter = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Thực hiện kết nối đến DB

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);// Thuc thi cau lenh query

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);//Tao adapter để đổ dữ liệu về DataSource

                sqlDataAdapter.Fill(dataTable);

                sqlConnection.Close();
            }


            return dataTable;
        }

        public int executeNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Thực hiện kết nối đến DB

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);// Thuc thi cau lenh query

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
            return data;
        }

        public object executeScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Thực hiện kết nối đến DB

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);// Thuc thi cau lenh query

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteScalar();

                sqlConnection.Close();
            }


            return data;
        }
    }
}
