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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCLichBieuKhongDinhKy.xaml
    /// </summary>
    public partial class UCLichBieuKhongDinhKy : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.LICHBIEUKHONGDINHKY mItem = null;
        List<Data.LICHBIEUKHONGDINHKY> lsArrayDeleted = null;

        public UCLichBieuKhongDinhKy(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void LoadDanhSach()
        {
            List<Data.LICHBIEUKHONGDINHKY> lsArray = Data.BOLichBieuKhongDinhKy.GetAll(mTransit);
            lvData.Items.Clear();
            foreach (var item in lsArray)
            {
                AddList(item);
            }
        }

        private void AddList(Data.LICHBIEUKHONGDINHKY item)
        {
            ListViewItem li = new ListViewItem();
            li.Content = item;
            li.Tag = item;
            lvData.Items.Add(li);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.LICHBIEUKHONGDINHKY)li.Tag;
            }
        }


        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemLichBieuKhongDinhKy win = new UserControlLibrary.WindowThemLichBieuKhongDinhKy(mTransit);
            if (win.ShowDialog() == true)
            {
                AddList(win._Item);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.LICHBIEUKHONGDINHKY)li.Tag;

                UserControlLibrary.WindowThemLichBieuKhongDinhKy win = new UserControlLibrary.WindowThemLichBieuKhongDinhKy(mTransit);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.Edit = true;
                    li.Tag = win._Item;
                    li.Content = win._Item;
                    lvData.Items.Refresh();
                }
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.LICHBIEUKHONGDINHKY)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.LICHBIEUKHONGDINHKY>();
                }
                if (mItem.LichBieuKhongDinhKyID > 0)
                    lsArrayDeleted.Add(mItem);
                lvData.Items.Remove(lvData.SelectedItems[0]);
                if (lvData.Items.Count > 0)
                {
                    lvData.SelectedIndex = 0;
                }
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.LICHBIEUKHONGDINHKY> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.LICHBIEUKHONGDINHKY)li.Tag;
                if (mItem.LichBieuKhongDinhKyID == 0 || mItem.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.LICHBIEUKHONGDINHKY>();
                    lsArray.Add(mItem);
                }
            }
            Data.BOLichBieuKhongDinhKy.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            MessageBox.Show("Lưu thành công");
        }

        public void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnLuu_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnThem_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                btnDanhSach_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.F2)
            {
                btnSua_Click(null, null);
                return;
            }
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                btnXoa_Click(null, null);
                return;
            }
        }

        private void btnDanhSach_Click(object sender, RoutedEventArgs e)
        {
            mItem = null;
            lsArrayDeleted = null;
            LoadDanhSach();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();

        }
    }
}
