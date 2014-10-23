using System;
using System.Windows;
using System.Windows.Controls;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCNewMon.xaml
    /// </summary>
    public partial class UCNewMon : UserControl
    {
        private int NhomID = 0;

        public UCNewMon(int nhomID)
        {
            InitializeComponent();
            NhomID = nhomID;
        }

        public delegate void EvenDanhSachBanClick(Data.MENUMON mon);

        public event EvenDanhSachBanClick OnEvenDanhSachBanClick;

        public Data.MENUMON _Mon { get; set; }

        public void CapNhat()
        {
            if (_Mon != null)
            {
                GetData();
                Data.BOMenuMon.CapNhat(_Mon);
            }
            else
            {
                GetData();
                Data.BOMenuMon.Them(_Mon);
            }
        }

        public void GetData()
        {
            if (_Mon == null)
            {
                _Mon = new Data.MENUMON();
            }
            _Mon.TenDai = txtTenDai.Text;
            _Mon.TenNgan = txtTenNgan.Text;
            if (txtSapXep.Text == "")
                _Mon.SapXep = 0;
            else
                _Mon.SapXep = Convert.ToInt32(txtSapXep.Text.Trim());
        }

        public void Xoa()
        {
            Data.BOMenuMon.Xoa(_Mon.MonID);
        }

        private void btnDanhSachBan_Click(object sender, RoutedEventArgs e)
        {
            OnEvenDanhSachBanClick(_Mon);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Mon != null)
            {
                txtTenDai.Text = _Mon.TenDai;
                txtTenNgan.Text = _Mon.TenNgan;
                txtSapXep.Text = _Mon.SapXep.ToString();
                btnDanhSachBan.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                btnDanhSachBan.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}