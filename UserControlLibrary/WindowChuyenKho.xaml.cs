﻿using System;
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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowChuyenKho.xaml
    /// </summary>
    public partial class WindowChuyenKho : Window
    {
        private Data.KaraokeEntities mKaraokeEntities;
        private Data.Transit mTransit = null;
        private Data.BOChuyenKho mBOChuyenKho;
        public WindowChuyenKho(Data.Transit transit,Data.BOChuyenKho chuyenKho,Data.KaraokeEntities kara)
        {
            InitializeComponent();
            mTransit = transit;
            mKaraokeEntities = kara;
            mBOChuyenKho = chuyenKho;
        }

        public Data.BOChuyenKho _Item { get; set; }
        

        private void LoadData(ListBox lv,Data.KHO kho)
        {
            lv.ItemsSource = Data.BOTonKho.GetTonKhoByKho(mKaraokeEntities, kho);
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            //if (lsArray != null && lsArray.Count > 0)
            //{
            //    if (CheckValues())
            //    {
            //        if (_Item == null)
            //        {
            //            _Item = new Data.BOChuyenKho();
            //            _Item.ChuyenKho.Visual = true;
            //            _Item.ChuyenKho.Deleted = false;
            //            _Item.ChuyenKho.Edit = false;
            //            _Item.ChuyenKho.NhanVienID = mTransit.NhanVien.NhanVienID;
            //        }
            //        GetValues();
            //        foreach (var item in lsArray)
            //        {
            //            item.ChiTietChuyenKho.TONKHO.DonViTinh = item.ChiTietChuyenKho.TONKHO.DonViTinh * item.LoaiBan.KichThuocBan;
            //            item.ChiTietChuyenKho.TONKHO.SoLuongNhap = item.ChiTietChuyenKho.TONKHO.DonViTinh * item.ChiTietChuyenKho.TONKHO.SoLuongNhap;
            //            item.ChiTietChuyenKho.TONKHO.SoLuongTon = item.ChiTietChuyenKho.TONKHO.SoLuongNhap;
            //        }
            //        //BOQuanLyKho.ChuyenKho(_Item, lsArray, mTransit);
            //        BOChuyenKho.Them(_Item, lsArray, mTransit);

            //        UserControlLibrary.WindowMessageBox.ShowDialog(lbTieuDe.Text + " thành công");
            //        DialogResult = true;
            //    }
            //}
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            //WindowChonMon win = new WindowChonMon(mTransit, false, true, false, true);
            //if (win.ShowDialog() == true)
            //{
            //    Data.BOChiTietChuyenKho item = new Data.BOChiTietChuyenKho();
            //    item.ChiTietChuyenKho.TONKHO = new Data.TONKHO();
            //    item.ChiTietChuyenKho.TONKHO.SoLuongNhap = 1;
            //    item.ChiTietChuyenKho.TONKHO.GiaNhap = 0;
            //    item.ChiTietChuyenKho.TONKHO.GiaBan = win._ItemKichThuocMon.MenuKichThuocMon.GiaBanMacDinh;
            //    item.ChiTietChuyenKho.TONKHO.NgaySanXuat = DateTime.Now;
            //    item.ChiTietChuyenKho.TONKHO.NgayHetHan = DateTime.Now;
            //    item.ChiTietChuyenKho.TONKHO.DonViTinh = win._ItemKichThuocMon.KichThuocLoaiBan;
            //    item.ChiTietChuyenKho.TONKHO.MonID = win._ItemKichThuocMon.MenuMon.MonID;
            //    item.MenuMon = win._ItemKichThuocMon.MenuMon;
            //    item.ListLoaiBan = lsLoaiBan.Where(s => s.DonViID == win._ItemKichThuocMon.MenuMon.DonViID).ToList();
            //    if (item.ListLoaiBan.Count > 0)
            //    {
            //        item.ChiTietChuyenKho.TONKHO.LoaiBanID = item.ListLoaiBan[0].LoaiBanID;
            //        item.LoaiBan = item.ListLoaiBan[0];
            //        item.ChiTietChuyenKho.TONKHO.DonViID = item.LoaiBan.DonViID;
            //    }
            //    if (lsArray == null)
            //        lsArray = new List<Data.BOChiTietChuyenKho>();
            //    lsArray.Add(item);
            //    lvData.Items.Refresh();
            //}
        }

        private void btnChuyenKho_Click(object sender, RoutedEventArgs e)
        {
            if (cbbKhoDen.SelectedItem!=null)
            {
                var kho = cbbKhoDen.SelectedItem as Data.KHO;
                var tonkho = ((Button)sender).DataContext as Data.BOTonKho;
                if (tonkho!=null)
                {
                    mBOChuyenKho.KhoDenID = kho.KhoID;
                    mBOChuyenKho.TonKho = tonkho.TonKho;
                    mBOChuyenKho.SoLuong = 1;
                    mBOChuyenKho.Chuyen();
                    LoadTonKhoDen();
                    UserControlLibrary.WindowMessageBox.ShowDialog("Chuyển kho thành công");
                }
            }
            else
            {
                UserControlLibrary.WindowMessageBox.ShowDialog("Chọn kho cần chuyển");
            }
        }

        private void cbbLoaiBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool CheckValues()
        {
            return true;
        }

        private void GetValues()
        {
            //_Item.ChuyenKho.KhoDiID = 0;
            //_Item.ChuyenKho.KhoDenID = 0;
            //_Item.ChuyenKho.NgayChuyen = DateTime.Now;
            //if (cbbKhoDi.Items.Count > 0)
            //{
            //    _Item.ChuyenKho.KhoDiID = (int)cbbKhoDi.SelectedValue;
            //    _Item.KhoDi = (Data.KHO)cbbKhoDi.SelectedItem;
            //}
            //if (cbbKhoDen.Items.Count > 0)
            //{
            //    _Item.ChuyenKho.KhoDenID = (int)cbbKhoDen.SelectedValue;
            //    _Item.KhoDen = (Data.KHO)cbbKhoDen.SelectedItem;
            //}
        }

        private void LoadKhoHang()
        {
            cbbKhoDi.ItemsSource = Data.BOKho.GetAll(mKaraokeEntities);
            lvData.ItemsSource = null;
            //if (cbbKhoDi.Items.Count > 0)
            //    cbbKhoDi.SelectedIndex = 0;

            //cbbKhoDen.ItemsSource = Data.BOKho.GetAll(mKaraokeEntities);
            //if (cbbKhoDen.Items.Count > 0)
            //    cbbKhoDen.SelectedIndex = 0;
        }        
        private void SetValues()
        {
            //if (_Item == null)
            //{
            //    if (cbbKhoDi.Items.Count > 0)
            //        cbbKhoDi.SelectedIndex = 0;
            //    if (cbbKhoDen.Items.Count > 0)
            //        cbbKhoDen.SelectedIndex = 0;
            //    //btnLuu.Content = mTransit.StringButton.Them + " chuyển kho";
            //    lbTieuDe.Text = "Thêm chuyển kho";
            //}
            //else
            //{
            //    cbbKhoDi.SelectedValue = _Item.ChuyenKho.KhoDiID;
            //    cbbKhoDen.SelectedValue = _Item.ChuyenKho.KhoDenID;
            //    //btnLuu.Content = mTransit.StringButton.Luu + " chuyển kho";
            //    lbTieuDe.Text = "Sửa nhập kho";
            //}
        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            LoadKhoHang();
            SetValues();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnLuu_Click(null, null);
                return;
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                btnHuy_Click(null, null);
                return;
            }
        }

        private void cbbKhoDi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbKhoDi.SelectedItem!=null)
            {                
                var kho = cbbKhoDi.SelectedItem as Data.KHO;
                cbbKhoDen.ItemsSource = Data.BOKho.GetAll(mKaraokeEntities, kho.KhoID);
                lvData1.ItemsSource = null;
                lvData.ItemsSource = Data.BOTonKho.GetTonKhoByKho(mKaraokeEntities, kho);
            }
        }

        private void cbbKhoDen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTonKhoDen();
        }
        private void LoadTonKhoDen()
        {
            if (cbbKhoDen.SelectedItem != null)
            {
                var kho = cbbKhoDen.SelectedItem as Data.KHO;
                lvData1.ItemsSource = Data.BOTonKho.GetTonKhoByKho(mKaraokeEntities, kho);
            }
        }
    }
}
