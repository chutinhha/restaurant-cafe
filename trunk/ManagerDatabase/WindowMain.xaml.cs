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
        private static string BACKUP_FOLDER = "CNV_BACKUP";
        private bool mIsFirst = true;
        private string mServerName;
        public WindowMain()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartSqlBrower();            
        }
        private void StartSqlBrower()
        {
            using (System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("SQLBrowser"))
            {
                if (sc.Status!=System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    try
                    {
                        sc.Start();
                    }
                    catch (Exception)
                    {                        
                    }
                }
            }
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
            if (cboMayChu.SelectedIndex>=0)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(txtDuongDan.Text + BACKUP_FOLDER);
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
                            cmd.CommandText = "CREATE DATABASE Karaoke "+
                                               "ON ("+
                                                   "NAME = Karaoke,"+
                                                   "FILENAME = '"+txtDuongDan.Text+BACKUP_FOLDER+"\\"+"Karaoke.mdf',"+
                                                   "SIZE = 10,"+
                                                   "MAXSIZE = 100,"+
                                                   "FILEGROWTH = 5 "+
                                                ") "+
                                                "LOG ON ( "+
                                                    "NAME = Sales_log,"+
                                                    "FILENAME = '" + txtDuongDan.Text + BACKUP_FOLDER + "\\" + "Karaoke.ldf'," +
                                                    "SIZE = 5MB,"+
                                                    "MAXSIZE = 100MB,"+
                                                    "FILEGROWTH = 5MB );";
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
                            
                            MessageBox.Show("Khởi tạo dữ liệu thành công!");                            
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

        private void CopyAllFile()
        {
            var dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var list = dir.GetFiles();
            foreach (var item in list)
            {
                try
                {                    
                    System.IO.File.Copy(item.FullName,txtDuongDan.Text+BACKUP_FOLDER+"\\"+item.Name);
                }
                catch (Exception)
                {                 
                }
            }
        }
        private void StartApp()
        {
            if (cboMayChu.SelectedIndex >= 0)
            {
                string connStr = String.Format("Data Source={0};Initial Catalog=master;Integrated Security=True", mServerName);
                using (var con=new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT count(*) FROM master.dbo.sysdatabases where name = 'Karaoke';";
                    object obj = cmd.ExecuteScalar();
                    if (Convert.ToInt32(obj)>0)
                    {
                        UpdateConfig("CNVRestaurant.exe.config");
                        UpdateConfig("WebService.exe.config");
                        CopyAllFile();
                        Application.Current.Shutdown();
                    }
                }                
            }
            MessageBox.Show("Không tồn tại dữ liệu.");
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

        private void btnDuongDan_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            openFileDialog.SelectedPath = txtDuongDan.Text;            
            if (openFileDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                txtDuongDan.Text = openFileDialog.SelectedPath;

            }
        }

        private void btnBatDau_Click(object sender, RoutedEventArgs e)
        {
            StartApp();
        }
    }
}
