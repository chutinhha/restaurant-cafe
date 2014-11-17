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
    /// Interaction logic for UCNhapKho.xaml
    /// </summary>
    public partial class UCNhapKho : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.BONhapKho mItem = null;
        private List<Data.BONhapKho> lsArrayDeleted = null;
        private Data.BONhapKho BONhapKho = null;

        public UCNhapKho(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            dtpThoiGian.SelectedDate = DateTime.Now;
            BONhapKho = new Data.BONhapKho(transit);
        }

        private void LoadDanhSach()
        {
            lvData.ItemsSource = BONhapKho.GetAll(mTransit, (DateTime)dtpThoiGian.SelectedDate);
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                mItem = (Data.BONhapKho)lvData.SelectedItems[0];
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            UserControlLibrary.WindowThemNhapKho win = new UserControlLibrary.WindowThemNhapKho(mTransit, BONhapKho);
            if (win.ShowDialog() == true)
            {
                LoadDanhSach();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                ListViewItem li = (ListViewItem)lvData.SelectedItems[0];
                mItem = (Data.BONhapKho)li.Tag;

                UserControlLibrary.WindowThemNhapKho win = new UserControlLibrary.WindowThemNhapKho(mTransit, BONhapKho);
                win._Item = mItem;
                if (win.ShowDialog() == true)
                {
                    win._Item.NhapKho.Edit = true;
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
                mItem = (Data.BONhapKho)((ListViewItem)lvData.SelectedItems[0]).Tag;
                if (lsArrayDeleted == null)
                {
                    lsArrayDeleted = new List<Data.BONhapKho>();
                }
                if (mItem.NhapKho.NhapKhoID > 0)
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
            List<Data.BONhapKho> lsArray = null;
            foreach (ListViewItem li in lvData.Items)
            {
                mItem = (Data.BONhapKho)li.Tag;
                if (mItem.NhapKho.NhapKhoID == 0 || mItem.NhapKho.Edit == true)
                {
                    if (lsArray == null)
                        lsArray = new List<Data.BONhapKho>();
                    lsArray.Add(mItem);
                }
            }
            BONhapKho.Luu(lsArray, lsArrayDeleted, mTransit);
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
