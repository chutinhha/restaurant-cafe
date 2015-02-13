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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCTile.xaml
    /// </summary>
    public partial class UCTile : UserControl
    {
        public UCTile()
        {
            InitializeComponent();
        }

        private Data.Transit mTransit = null;
        public void SetTransit(Data.Transit transit)
        {
            mTransit = transit;
            LoadData();
        }

        private void LoadData()
        {
            DispatcherTimer newTimer = new DispatcherTimer();
            newTimer.Interval = System.TimeSpan.FromSeconds(1);
            newTimer.Tick += newTimer_Tick;
            newTimer.Start();
            if (mTransit != null)
            {
                if (mTransit.NhanVien != null)
                {
                    btnNhanVien.Content = mTransit.NhanVien.TenNhanVien;
                }
            }
            lbPhienBan.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lbTenNhaHang.Text = "Phần mềm bán hàng";
        }

        private void newTimer_Tick(object sender, object e)
        {
            lbTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void btnMinimized_Click(object sender, RoutedEventArgs e)
        {
            if (OnEventMinimized != null)
            {
                OnEventMinimized();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (OnEventExit != null)
            {
                OnEventExit();
            }
        }

        public string TenChucNang
        {
            set
            {
                lbTenChucNang.Text = value;
            }
        }

        public delegate void OnExit();
        public delegate void OnMinimized();
        public event OnExit OnEventExit;
        public event OnMinimized OnEventMinimized;
        public delegate void ChangeLogin();
        public event ChangeLogin OnChangeLogin;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (OnEventMinimized == null)
                btnMinimized.Visibility = System.Windows.Visibility.Hidden;
            if (OnEventExit == null)
                btnExit.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if (OnChangeLogin != null)
            {
                OnChangeLogin();
            }
        }
    }
}

