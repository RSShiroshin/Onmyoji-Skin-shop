using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PRN_Project2
{
    /// <summary>
    /// Chứa những hàm xử lý database chung cho tất cả
    /// kết nối
    /// executequẻy
    /// </summary>
    public class DataProvider
    {
        //Khai bao cac thanh phan ket noi va xu ly DB
        SqlConnection cnn; //Ket noi DB
        SqlDataAdapter da; //Xu ly cac cau lenh SQL: Select
        SqlCommand cmd; //Thuc thi cau lenh insert,update,delete

        public DataProvider()
        {
            connect();
        }
        private string getConnectionString()
        {
            string connectionString;
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true) //new de` ko co p tu tao file, sua property cua file thanhf alway copy**
                .Build();
            connectionString = config["ConnectionStrings:PRN_Project"]; //MysSaleDB la ten trong file json
            return connectionString;
        }
        private void connect()
        {
            try
            {
                string strCnn = getConnectionString();
                cnn = new SqlConnection(strCnn);
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                cnn.Open();
                //MessageBox.Show("Connect success!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Loi ket noi:" + ex.Message);
            }
        }

        //Hàm execute 1 câu lệnh select
        public DataTable executeQuery(string strSelect)
        {
            DataTable dt = new DataTable();
            try
            {
                da = new SqlDataAdapter(strSelect, cnn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Execute fail:" + ex.Message);
            }
            return dt;
        }

        //Hàm execute câu lệnh insert,update,delete
        public bool executeNonQuery(string strSQL)
        {
            try
            {
                cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                cmd.ExecuteNonQuery();   
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert/Update/Delete error:"+ex.Message);
                return false;
            }
            return true;
        }

        public bool executeNonQuery2(string strSQL, params SqlParameter[] para)
        {
            try
            {
                cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                if (para != null)
                {
                    foreach (SqlParameter p in para)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert/Update/Delete error:" + ex.Message);
                return false;
            }
            return true;
        }

        internal IDataReader executeQuery2(string strSelect, params SqlParameter[] para)
        {
            IDataReader dr = null;
            try
            {
                cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSelect;
                if(para!= null)
                {
                    foreach (SqlParameter p in para)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                dr=cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excute Query2 error:" + ex.Message);
            }
            return dr;
        }
    }
}
