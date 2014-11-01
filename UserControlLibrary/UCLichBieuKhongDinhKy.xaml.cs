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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCLichBieuKhongDinhKy.xaml
    /// </summary>
    public partial class UCLichBieuKhongDinhKy : UserControl
    {
        private Data.LICHBIEUKHONGDINHKY mLichBieuKhongDinhKy = null;
        private Data.Transit mTransit = null;
        public UCLichBieuKhongDinhKy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void btnMoi_Click(object sender, RoutedEventArgs e)
        {
            mLichBieuKhongDinhKy = null;
            SetValues();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValues())
            {
                if (mLichBieuKhongDinhKy == null)
                {
                    mLichBieuKhongDinhKy = new Data.LICHBIEUKHONGDINHKY();
                    GetValues();
                    Data.BOLichBieuKhongDinhKy.Them(mLichBieuKhongDinhKy, mTransit);
                    lbStatus.Text = "Thêm thành công";
                    LoadDanhSachLichBieu();
                    btnMoi_Click(sender, e);
                }
                else
                {
                    GetValues();
                    Data.BOLichBieuKhongDinhKy.Sua(mLichBieuKhongDinhKy, mTransit);
                    lbStatus.Text = "Cập nhật thành công";
                    LoadDanhSachLichBieu();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mLichBieuKhongDinhKy = (Data.LICHBIEUKHONGDINHKY)((ListViewItem)lvData.SelectedItems[0]).Tag;
                Data.BOMayIn.Xoa(mLichBieuKhongDinhKy.LichBieuKhongDinhKyID, mTransit);
                lbStatus.Text = "Xóa thành công";
            }
        }

        private bool CheckValues()
        {
            lbStatus.Text = "";
            if (txtTenLichBieu.Text == "")
            {
                lbStatus.Text = "Tên lịch biểu không được bỏ trống";
                return false;
            }

            return true;
        }

        private void GetValues()
        {
            mLichBieuKhongDinhKy.TenLichBieu = txtTenLichBieu.Text;
            mLichBieuKhongDinhKy.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            mLichBieuKhongDinhKy.Ngay = dtpNgay.SelectedDate;
            mLichBieuKhongDinhKy.GioBatDau = timeBatDau.TimeCurent;
            mLichBieuKhongDinhKy.GioKetThuc = timeKetThuc.TimeCurent;
            mLichBieuKhongDinhKy.Visual = ckHoatDong.IsChecked;
            mLichBieuKhongDinhKy.Visual = true;
            mLichBieuKhongDinhKy.Deleted = false;
        }

        private void LoadDanhSachLichBieu()
        {
            List<Data.LICHBIEUKHONGDINHKY> lsArray = Data.BOLichBieuKhongDinhKy.GetAll(mTransit);
            lvData.Items.Clear();
            foreach (Data.LICHBIEUKHONGDINHKY item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvData.Items.Add(li);
            }
        }

        private void LoadLoaiGia()
        {
            cbbLoaiGia.ItemsSource = Data.BOMenuLoaiGia.GetAll(mTransit);
            if (cbbLoaiGia.Items.Count > 0)
            {
                cbbLoaiGia.SelectedIndex = 0;
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mLichBieuKhongDinhKy = (Data.LICHBIEUKHONGDINHKY)((ListViewItem)lvData.SelectedItems[0]).Tag;
                SetValues();
            }
        }

        private void SetValues()
        {
            if (mLichBieuKhongDinhKy == null)
            {
                txtTenLichBieu.Text = "";
                if (cbbLoaiGia.Items.Count > 0)
                {
                    cbbLoaiGia.SelectedIndex = 0;
                }
                ckHoatDong.IsChecked = true;
                dtpNgay.SelectedDate = DateTime.Now;
                timeBatDau.TimeCurent = new TimeSpan(0, 0, 0);
                timeKetThuc.TimeCurent = new TimeSpan(0, 0, 0);

                btnThem.Content = "Thêm lịch biểu";
            }
            else
            {
                txtTenLichBieu.Text = mLichBieuKhongDinhKy.TenLichBieu;
                cbbLoaiGia.SelectedValue = mLichBieuKhongDinhKy.LoaiGiaID;
                ckHoatDong.IsChecked = mLichBieuKhongDinhKy.Visual;
                dtpNgay.SelectedDate = mLichBieuKhongDinhKy.Ngay;
                timeBatDau.TimeCurent = (TimeSpan)mLichBieuKhongDinhKy.GioBatDau;
                timeKetThuc.TimeCurent = (TimeSpan)mLichBieuKhongDinhKy.GioKetThuc;
                btnThem.Content = "Cập nhật lịch biểu";
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiGia();
            LoadDanhSachLichBieu();
            btnMoi_Click(sender, e);
        }
    }
}
