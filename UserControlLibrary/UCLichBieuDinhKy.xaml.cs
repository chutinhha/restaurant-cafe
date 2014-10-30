using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCLichBieuDinhKy.xaml
    /// </summary>
    public partial class UCLichBieuDinhKy : UserControl
    {
        private Data.LICHBIEUDINHKY mLichBieuKhongDinhKy = null;
        private Data.Transit mTransit = null;

        public UCLichBieuDinhKy(Data.Transit transit)
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
                    mLichBieuKhongDinhKy = new Data.LICHBIEUDINHKY();
                    GetValues();
                    Data.BOLichBieuDinhKy.Them(mLichBieuKhongDinhKy);
                    lbStatus.Text = "Thêm thành công";
                    LoadDanhSachLichBieu();
                    btnMoi_Click(sender, e);
                }
                else
                {
                    GetValues();
                    Data.BOLichBieuDinhKy.Sua(mLichBieuKhongDinhKy);
                    lbStatus.Text = "Cập nhật thành công";
                    LoadDanhSachLichBieu();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mLichBieuKhongDinhKy = (Data.LICHBIEUDINHKY)((ListViewItem)lvData.SelectedItems[0]).Tag;
                Data.BOMayIn.Xoa(mLichBieuKhongDinhKy.LichBieuDinhKyID);
                lbStatus.Text = "Xóa thành công";
            }
        }

        private void cbbTheLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbTheLoai.SelectedValue != null)
            {
                int TheLoaiID = (int)cbbTheLoai.SelectedValue;
                switch (TheLoaiID)
                {
                    case 1:
                        LoadLoaiLichBieuBatDau(1);
                        LoadLoaiLichBieuKetThuc(1);
                        lbGiaTriBatDau.Text = "Thứ bắt đầu";
                        lbGiaTriKetThuc.Text = "Thứ kết thúc";
                        break;

                    case 2:
                        LoadLoaiLichBieuBatDau(2);
                        LoadLoaiLichBieuKetThuc(2);
                        lbGiaTriBatDau.Text = "Ngày bắt đầu";
                        lbGiaTriKetThuc.Text = "Ngày kết thúc";
                        break;

                    case 3:
                        LoadLoaiLichBieuBatDau(2);
                        LoadLoaiLichBieuKetThuc(3);
                        lbGiaTriBatDau.Text = "Ngày";
                        lbGiaTriKetThuc.Text = "Tháng";
                        break;

                    default:
                        break;
                }
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

            if (txtUuTien.Text == "")
                txtUuTien.Text = "1";
            return true;
        }

        private void GetValues()
        {
            mLichBieuKhongDinhKy.TenLichBieu = txtTenLichBieu.Text;
            mLichBieuKhongDinhKy.TheLoaiID = (int)cbbTheLoai.SelectedValue;
            mLichBieuKhongDinhKy.LoaiGiaID = (int)cbbLoaiGia.SelectedValue;
            mLichBieuKhongDinhKy.GiaTriBatDau = (int)cbbGiaTriBatDau.SelectedValue;
            mLichBieuKhongDinhKy.GiaTriKetThuc = (int)cbbGiaTriKetThuc.SelectedValue;
            mLichBieuKhongDinhKy.UuTien = System.Convert.ToInt32(txtUuTien.Text);
            mLichBieuKhongDinhKy.Visual = true;
            mLichBieuKhongDinhKy.Deleted = false;
            mLichBieuKhongDinhKy.GioBatDau = timeBatDau.TimeCurent;
            mLichBieuKhongDinhKy.GioKetThuc = timeKetThuc.TimeCurent;

            switch (mLichBieuKhongDinhKy.TheLoaiID)
            {
                case 1:
                    if (mLichBieuKhongDinhKy.GiaTriBatDau == mLichBieuKhongDinhKy.GiaTriKetThuc)
                    {
                        mLichBieuKhongDinhKy.TenHienThi = cbbGiaTriBatDau.Text;
                    }
                    else
                    {
                        mLichBieuKhongDinhKy.TenHienThi = cbbGiaTriBatDau.Text + " - " + cbbGiaTriKetThuc.Text;
                    }
                    break;

                case 2:
                    if (mLichBieuKhongDinhKy.GiaTriBatDau == mLichBieuKhongDinhKy.GiaTriKetThuc)
                    {
                        mLichBieuKhongDinhKy.TenHienThi = cbbGiaTriBatDau.Text;
                    }
                    else
                    {
                        mLichBieuKhongDinhKy.TenHienThi = cbbGiaTriBatDau.Text + " - " + cbbGiaTriKetThuc.Text;
                    }
                    break;

                case 3:
                    mLichBieuKhongDinhKy.TenHienThi = cbbGiaTriBatDau.Text + " " + cbbGiaTriKetThuc.Text;
                    break;

                default:
                    break;
            }
        }

        private void LoadDanhSachLichBieu()
        {
            List<Data.LICHBIEUDINHKY> lsArray = Data.BOLichBieuDinhKy.GetAll();
            lvData.Items.Clear();
            foreach (Data.LICHBIEUDINHKY item in lsArray)
            {
                ListViewItem li = new ListViewItem();
                li.Content = item;
                li.Tag = item;
                lvData.Items.Add(li);
            }
        }

        private void LoadLoaiGia()
        {
            cbbLoaiGia.ItemsSource = Data.BOMenuLoaiGia.GetAll();
            if (cbbLoaiGia.Items.Count > 0)
            {
                cbbLoaiGia.SelectedIndex = 0;
            }
        }

        private void LoadLoaiLichBieuBatDau(int TheLoaiID)
        {
            cbbGiaTriBatDau.ItemsSource = Data.BOLoaiLichBieu.GetAll(TheLoaiID);
            if (cbbGiaTriBatDau.Items.Count > 0)
                cbbGiaTriBatDau.SelectedIndex = 0;
        }

        private void LoadLoaiLichBieuKetThuc(int TheLoaiID)
        {
            cbbGiaTriKetThuc.ItemsSource = Data.BOLoaiLichBieu.GetAll(TheLoaiID);
            if (cbbGiaTriKetThuc.Items.Count > 0)
                cbbGiaTriKetThuc.SelectedIndex = 0;
        }

        private void LoadTheLoai()
        {
            cbbTheLoai.ItemsSource = Data.BOTheLoaiLichBieu.GetAll();
            if (cbbTheLoai.Items.Count > 0)
            {
                cbbTheLoai.SelectedIndex = 0;
            }
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mLichBieuKhongDinhKy = (Data.LICHBIEUDINHKY)((ListViewItem)lvData.SelectedItems[0]).Tag;
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
                if (cbbTheLoai.Items.Count > 0)
                {
                    cbbTheLoai.SelectedIndex = 0;
                }
                txtUuTien.Text = "1";
                timeBatDau.TimeCurent = new TimeSpan(0, 0, 0);
                timeKetThuc.TimeCurent = new TimeSpan(0, 0, 0);

                btnThem.Content = "Thêm lịch biểu";
            }
            else
            {
                txtTenLichBieu.Text = mLichBieuKhongDinhKy.TenLichBieu;
                cbbLoaiGia.SelectedValue = mLichBieuKhongDinhKy.LoaiGiaID;
                cbbTheLoai.SelectedValue = mLichBieuKhongDinhKy.TheLoaiID;
                cbbTheLoai_SelectionChanged(null, null);
                cbbGiaTriBatDau.SelectedValue = mLichBieuKhongDinhKy.GiaTriBatDau;
                cbbGiaTriKetThuc.SelectedValue = mLichBieuKhongDinhKy.GiaTriKetThuc;
                txtUuTien.Text = mLichBieuKhongDinhKy.UuTien.ToString();
                timeBatDau.TimeCurent = (TimeSpan)mLichBieuKhongDinhKy.GioBatDau;
                timeKetThuc.TimeCurent = (TimeSpan)mLichBieuKhongDinhKy.GioKetThuc;
                btnThem.Content = "Cập nhật lịch biểu";
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTheLoai();
            LoadLoaiGia();
            LoadDanhSachLichBieu();
            btnMoi_Click(sender, e);
        }
    }
}