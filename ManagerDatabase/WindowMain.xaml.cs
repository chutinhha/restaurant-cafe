using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Configuration;

namespace ManagerDatabase
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        private bool mIsFirst = true;
        private string mServerName;
        public WindowMain()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cboMayChu_GotFocus(object sender, RoutedEventArgs e)
        {
            if (mIsFirst)
            {
                string myServer = Environment.MachineName;
                DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if (myServer == servers.Rows[i]["ServerName"].ToString()) ///// used to get the servers in the local machine////
                    {

                        if ((servers.Rows[i]["InstanceName"] as string) != null)
                            cboMayChu.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                        else
                            cboMayChu.Items.Add(servers.Rows[i]["ServerName"]);
                    }
                }
            }
            if (mIsFirst)
            {
                mIsFirst = false;
            }
        }        
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //cboMayChu.IsEnabled = false;
            //btnHuy.IsEnabled = false;
            //btnHuy.IsEnabled = false;
            //DispatcherTimer newTimer = new DispatcherTimer();
            //newTimer.Interval = System.TimeSpan.FromMilliseconds(100);
            //newTimer.Tick += new EventHandler(newTimer_Tick);
            //newTimer.Start();
            if (cboMayChu.SelectedIndex>=0)
            {
                try
                {
                    mServerName = cboMayChu.Text;
                    string connStr = String.Format("Data Source={0};Initial Catalog=master;Integrated Security=True", mServerName);
                    string sql = "";
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(System.AppDomain.CurrentDomain.BaseDirectory+ "//database.sql"))
                    {
                        sql = sr.ReadToEnd();    
                    }                
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "CREATE DATABASE Karaoke;";
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            string[] check = new string[] { "\nGO" };                            
                            string[] sqlStr = sql.Split(check,StringSplitOptions.None);
                            //cmd.CommandText = sql.Replace("\nGO",";");

                            foreach (var item in sqlStr)
                            {
                                cmd.CommandText = item;
                                cmd.ExecuteNonQuery();                            
                            }
                            //WebService.exe
                            //try
                            //{
                            //    System.IO.File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "//database.sql");
                            //}
                            //catch (Exception ex)
                            //{
                            //    MessageBox.Show(ex.Message);
                            //}
                            UpdateConfig("CNVRestaurant.exe.config");
                            UpdateConfig("WebService.exe.config");
                            MessageBox.Show("Khởi tạo dữ liệu thành công!");
                            Application.Current.Shutdown();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message,"Lỗi");
                        }
                    }
                }
                catch (Exception ex)
                {

                    this.Content = ex.Message;
                } 
            }
        }
        private void UpdateConfig(string configName)
        {
            try
            {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                string configFile = System.AppDomain.CurrentDomain.BaseDirectory + "//"+configName;
                configFileMap.ExeConfigFilename = configFile;
                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                foreach (ConnectionStringSettings item in config.ConnectionStrings.ConnectionStrings)
                {
                    item.ConnectionString = item.ConnectionString.Replace(@"KHOA\SQLEXPRESS", mServerName);
                }
                try
                {
                    config.AppSettings.Settings["FirstTime"].Value = "false";
                }
                catch (Exception)
                {                    
                }
                config.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {

            if (cboMayChu.SelectedIndex >= 0)
            {
                try
                {
                    mServerName = cboMayChu.Text;
                    string connStr = String.Format("Data Source={0};Initial Catalog=master;Integrated Security=True", mServerName);
                    //string sql = "";
                    //using (System.IO.StreamReader sr = new System.IO.StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "//database.sql"))
                    //{
                    //    sql = sr.ReadToEnd();
                    //}
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "DROP DATABASE Karaoke;";
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Xóa dữ liệu thành công!");                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Lỗi");
                        }
                    }
                }
                catch (Exception ex)
                {

                    this.Content = ex.Message;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnXoaDuLieuBH_Click(object sender, RoutedEventArgs e)
        {
            if (cboMayChu.SelectedIndex >= 0)
            {
                try
                {
                    mServerName = cboMayChu.Text;
                    string connStr = String.Format("Data Source={0};Initial Catalog=master;Integrated Security=True", mServerName);
                    //string sql = "";
                    //using (System.IO.StreamReader sr = new System.IO.StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "//database.sql"))
                    //{
                    //    sql = sr.ReadToEnd();
                    //}
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "use Karaoke;delete CHITIETTACHBAN;delete TACHBAN;delete CHITIETGOPBAN;delete GOPBAN;delete CHUYENBAN;delete CHITIETLICHSUBANHANG;delete LICHSUBANHANG;delete CHITIETBANHANG;delete BANHANG;delete TONKHOTONG";
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Xóa dữ liệu thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Lỗi");
                        }
                    }
                }
                catch (Exception ex)
                {

                    this.Content = ex.Message;
                }
            }
        }
    }
}
