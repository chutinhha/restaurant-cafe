using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCQuanLyGia.xaml
    /// </summary>
    public partial class UCQuanLyGia : UserControl
    {
        private Data.Transit mTransit = null;
        private Data.MENUKICHTHUOCMON mMenuKichThuocMon = null;
        private Data.MENUGIA _MenuGia = null;

        public UCQuanLyGia(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvData.SelectedItems.Count > 0)
            {
                _MenuGia = (Data.MENUGIA)((ListViewItem)lvData.SelectedItems[0]).Tag;
                cbbLoaiGiaBan.SelectedValue = _MenuGia.LoaiGiaID;
                txtGiaBan.Text = _MenuGia.Gia.ToString();
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cbbLoaiGiaBan.Items.Count > 0)
            {
                _MenuGia = Data.BOMenuGia.GetByID((int)cbbLoaiGiaBan.SelectedValue, mMenuKichThuocMon.KichThuocMonID, mTransit);
                if (txtGiaBan.Text == "")
                {
                    txtGiaBan.Text = "0";
                }
                if (_MenuGia == null)
                {
                    _MenuGia = new Data.MENUGIA();
                    _MenuGia.Gia = System.Convert.ToDecimal(txtGiaBan.Text);
                    _MenuGia.KichThuocMonID = mMenuKichThuocMon.KichThuocMonID;
                    _MenuGia.LoaiGiaID = (int)cbbLoaiGiaBan.SelectedValue;
                    Data.BOMenuGia.Them(_MenuGia, mTransit);
                }
                else
                {
                    _MenuGia.Gia = System.Convert.ToDecimal(txtGiaBan.Text);
                    Data.BOMenuGia.CapNhat(_MenuGia, mTransit);
                }
            }
            LoadDanhSachGiaBan();
            _MenuGia = null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            uCMenu.OnEventMenu += new ControlLibrary.UCMenu.EventMenu(uCMenu_OnEventMenu);
            uCMenu.Init(mTransit);
            LoadLoaiGia();
        }

        private void uCMenu_OnEventMenu(object ob)
        {
            if (ob is Data.MENUKICHTHUOCMON)
            {
                mMenuKichThuocMon = (Data.MENUKICHTHUOCMON)ob;
                txtLoaiBan.Text = mMenuKichThuocMon.TenLoaiBan;
                LoadDanhSachGiaBan();
            }
        }

        public void LoadLoaiGia()
        {
            cbbLoaiGiaBan.ItemsSource = Data.BOMenuLoaiGia.GetAll(mTransit);
            if (cbbLoaiGiaBan.Items.Count > 0)
                cbbLoaiGiaBan.SelectedIndex = 0;
        }

        private void LoadDanhSachGiaBan()
        {
            if (mMenuKichThuocMon != null)
            {
                List<Data.MENUGIA> lsArray = Data.BOMenuGia.GetAll(mMenuKichThuocMon.KichThuocMonID, mTransit);
                lvData.Items.Clear();
                foreach (Data.MENUGIA item in lsArray)
                {
                    ListViewItem li = new ListViewItem();
                    li.Content = item;
                    li.Tag = item;
                    lvData.Items.Add(li);
                }
            }
        }

        private void cbbLoaiGiaBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mMenuKichThuocMon != null && cbbLoaiGiaBan.SelectedValue != null)
            {
                _MenuGia = Data.BOMenuGia.GetByID((int)cbbLoaiGiaBan.SelectedValue, mMenuKichThuocMon.KichThuocMonID, mTransit);
                if (_MenuGia != null)
                    txtGiaBan.Text = _MenuGia.Gia.ToString();
                else
                    txtGiaBan.Text = "0";
            }
        }
    }
}