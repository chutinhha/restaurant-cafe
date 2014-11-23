using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ControlLibrary;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {
        public Data.BOMenuKichThuocMon BOMenuKichThuocMon = null;
        public Data.BOMenuMon BOMenuMon = null;
        public Data.BOMenuNhom BOMenuNhom = null;
        public Data.BOMenuKhuyenMai BOMenuKhuyenMai = null;
        public bool _IsDanhSachKhuyenMai { get; set; }
        public bool _IsDinhLuong { get; set; }
        public bool _IsThoiGian { get; set; }
        public bool _IsTonKho { get; set; }
        private double ImageHeightItems = 0;
        private double ImageWidthItems = 0;
        private bool IsRefershMenu = false;
        private List<Data.BOMenuKichThuocMon> lsMenuKichThuocMon = null;
        private List<Data.BOMenuMon> lsMenuMon = null;
        private List<Data.BOMenuNhom> lsMenuNhom = null;
        private Data.Transit mTransit = null;
        public UCMenu()
        {
            InitializeComponent();
            _IsBanHang = false;
            _IsDanhSachKhuyenMai = false;
            _IsDinhLuong = true;
            _IsTonKho = true;
            _IsThoiGian = true;
        }

        public delegate void EventMenuGroup(Data.BOMenuNhom ob);

        public delegate void EventMenuKichThuocMon(Data.BOMenuKichThuocMon ob);

        public delegate void EventMenuMon(Data.BOMenuMon ob);

        public event EventMenuKichThuocMon _OnEventMenuKichThuocMon;

        public event EventMenuMon _OnEventMenuMon;

        public event EventMenuGroup _OnEventMenuNhom;

        private enum LoaiMenu
        {
            None = 0,
            Nhom = 1,
            Mon = 2,
            KichThuocMon = 3
        }

        public bool _IsBanHang { get; set; }
        public void Init(Data.Transit transit)
        {
            SetImageSizetItems();
            mTransit = transit;
            BOMenuMon = new Data.BOMenuMon(transit);
            BOMenuNhom = new Data.BOMenuNhom(transit);
            BOMenuKichThuocMon = new Data.BOMenuKichThuocMon(transit);
            BOMenuKhuyenMai = new Data.BOMenuKhuyenMai();
            LoadData();
        }

        public void LoadData()
        {
            PageGroup = 1;
            LoadGroup();
        }

        #region Items

        private Data.BOMenuMon MenuMonIndex = null;
        private int PageItems = 1;

        private void OnEventMenuMon(Data.BOMenuMon item)
        {
            if (_OnEventMenuMon != null)
                _OnEventMenuMon(item);
        }

        public void LoadMon(int NhomID)
        {
            lsMenuMon = BOMenuMon.GetAll(NhomID, _IsBanHang, mTransit).ToList();
            if (lsMenuMon.Count > gridItems.Children.Count)
            {
                int CountItems = gridItems.Children.Count - 2;
                List<Data.BOMenuMon> lsItemsTem = lsMenuMon.Skip((PageItems - 1) * CountItems).Take(CountItems).ToList();
                bool Chay = true;
                int j = 0;
                for (int i = 0; i < lsItemsTem.Count; i++, j++)
                {
                    if (i == 0)
                    {
                        if (IsRefershMenu)
                            OnEventMenuMon(lsItemsTem[i]);
                    }
                    Chay = true;
                    while (Chay)
                    {
                        Chay = false;
                        if (Grid.GetRow(gridItems.Children[j]) != gridItems.RowDefinitions.Count - 1)
                            SetButtonItem((POSButtonMenu)gridItems.Children[j], lsItemsTem[i]);
                        else if (Grid.GetColumn(gridItems.Children[j]) > 0 && Grid.GetColumn(gridItems.Children[j]) < gridItems.ColumnDefinitions.Count - 1)
                            SetButtonItem((POSButtonMenu)gridItems.Children[j], lsItemsTem[i]);
                        else
                        {
                            Chay = true;
                            j++;
                        }
                    }
                }
                if (lsItemsTem.Count > gridItems.Children.Count - gridItems.ColumnDefinitions.Count)
                    j++;
                Chay = true;
                for (; j < gridItems.Children.Count; j++)
                {
                    Chay = true;
                    while (Chay)
                    {
                        Chay = false;
                        if (Grid.GetRow(gridItems.Children[j]) != gridItems.RowDefinitions.Count - 1)
                            SetButtonEmpty((POSButtonMenu)gridItems.Children[j]);
                        else if (Grid.GetColumn(gridItems.Children[j]) > 0 && Grid.GetColumn(gridItems.Children[j]) < gridItems.ColumnDefinitions.Count - 1)
                            SetButtonEmpty((POSButtonMenu)gridItems.Children[j]);
                        else
                        {
                            Chay = true;
                            j++;
                            if (j > gridItems.Children.Count - 1)
                                Chay = false;
                        }
                    }
                }
                SetItemPage(LoaiMenu.Mon);
            }
            else
            {
                for (int i = 0; i < lsMenuMon.Count; i++)
                {
                    if (i == 0)
                    {
                        if (IsRefershMenu)
                            OnEventMenuMon(lsMenuMon[i]);
                    }
                    SetButtonItem((POSButtonMenu)gridItems.Children[i], lsMenuMon[i]);
                }
                for (int i = lsMenuMon.Count; i < gridItems.Children.Count; i++)
                {
                    SetButtonEmpty((POSButtonMenu)gridItems.Children[i]);
                }
                SetItemPage(LoaiMenu.None);
            }
        }

        private void btnItemBack_Click(object sender, RoutedEventArgs e)
        {
            LoaiMenu lm = (LoaiMenu)btnItemBack.Tag;
            switch (lm)
            {
                case LoaiMenu.None:
                    btnItems_Click(sender, e);
                    break;

                case LoaiMenu.Nhom:
                    break;

                case LoaiMenu.Mon:
                    if (PageItems > 1)
                    {
                        PageItems--;
                        LoadMon(MenuNhomIndex.MenuNhom.NhomID);
                    }
                    break;

                case LoaiMenu.KichThuocMon:
                    if (PageKichThuocMon > 1)
                    {
                        PageKichThuocMon--;
                        LoadKichThuocMon(MenuMonIndex);
                    }
                    break;

                default:
                    break;
            }
        }

        private void btnItemNext_Click(object sender, RoutedEventArgs e)
        {
            LoaiMenu lm = (LoaiMenu)btnItemNext.Tag;
            switch (lm)
            {
                case LoaiMenu.None:
                    btnItems_Click(sender, e);
                    break;

                case LoaiMenu.Nhom:
                    break;

                case LoaiMenu.Mon:
                    if (MenuNhomIndex != null)
                        if (PageItems < lsMenuMon.Count / (gridItems.Children.Count - 2) + 1)
                        {
                            PageItems++;
                            LoadMon(MenuNhomIndex.MenuNhom.NhomID);
                        }
                    break;

                case LoaiMenu.KichThuocMon:
                    if (MenuNhomIndex != null)
                        if (PageKichThuocMon < lsMenuKichThuocMon.Count / (gridItems.Children.Count - 2) + 1)
                        {
                            PageKichThuocMon++;
                            LoadKichThuocMon(MenuMonIndex);
                        }
                    break;

                default:
                    break;
            }
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            POSButtonMenu btn = (POSButtonMenu)sender;
            if (btn.Tag is Data.BOMenuMon)
            {
                MenuMonIndex = (Data.BOMenuMon)btn.Tag;
                if (_IsBanHang)
                {
                    PageKichThuocMon = 1;
                    LoadKichThuocMon(MenuMonIndex);
                }
                else
                    OnEventMenuMon(MenuMonIndex);
            }
            else if (btn.Tag is Data.BOMenuKichThuocMon)
            {
                MenuKichThuocMonIndex = (Data.BOMenuKichThuocMon)btn.Tag;
                OnEventMenuKichThuocMon(MenuKichThuocMonIndex);
            }
        }



        private void SetButtonItem(POSButtonMenu btn, Data.BOMenuMon item)
        {
            btn.Visibility = System.Windows.Visibility.Visible;
            btn.Tag = item;
            btn.IsEnabled = true;
            btn.Content = item.MenuMon.TenNgan;
            if (item.MenuMon.Hinh != null && item.MenuMon.Hinh.Length > 0)
            {
                btn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.MenuMon.Hinh);
            }
            else
            {
                var uriSource = new Uri(@"/SystemImages;component/Images/NoImages.jpg", UriKind.Relative);
                btn.Image = new BitmapImage(uriSource);
            }
        }

        private void SetItemPage(LoaiMenu loaiMenu)
        {
            btnItemBack.Tag = loaiMenu;
            btnItemNext.Tag = loaiMenu;
            switch (loaiMenu)
            {
                case LoaiMenu.None:
                    break;

                case LoaiMenu.Nhom:
                    break;

                case LoaiMenu.Mon:
                case LoaiMenu.KichThuocMon:
                    btnItemBack.Content = "Trờ Về";
                    var uriSource = new Uri(@"/SystemImages;component/Images/Back.png", UriKind.Relative);
                    btnItemBack.Background = System.Windows.Media.Brushes.White;
                    btnItemBack.Image = new BitmapImage(uriSource);
                    btnItemNext.Content = "Tiếp Theo";
                    uriSource = new Uri(@"/SystemImages;component/Images/Forward.png", UriKind.Relative);
                    btnItemNext.Background = System.Windows.Media.Brushes.White;
                    btnItemNext.Image = new BitmapImage(uriSource);

                    break;

                default:
                    break;
            }
        }
        #endregion Items

        #region Nhóm

        private Data.BOMenuNhom MenuNhomIndex = null;
        private int PageGroup = 0;

        public void LoadGroup()
        {
            lsMenuNhom = BOMenuNhom.GetAll(LoaiNhomID, _IsBanHang, mTransit).ToList();
            if (lsMenuNhom.Count > gridGroup.Children.Count)
            {
                int CountGroup = gridGroup.Children.Count - 2;
                List<Data.BOMenuNhom> lsGroupTem = lsMenuNhom.Skip((PageGroup - 1) * CountGroup).Take(CountGroup).ToList();
                for (int i = 0; i < lsGroupTem.Count; i++)
                {
                    if (i == 0)
                    {
                        MenuNhomIndex = lsGroupTem[i];
                        PageItems = 1;
                        LoadMon(lsMenuNhom[i].MenuNhom.NhomID);
                        OnEventMenuNhom(MenuNhomIndex);
                    }
                    SetButtonNhom((POSButtonMenu)gridGroup.Children[i + 1], lsGroupTem[i]);
                }
                for (int i = lsGroupTem.Count; i < CountGroup; i++)
                {
                    SetButtonEmpty((POSButtonMenu)gridGroup.Children[i + 1]);
                }
                SetGroupPage();
            }
            else
            {
                for (int i = 0; i < lsMenuNhom.Count; i++)
                {
                    if (i == 0)
                    {
                        MenuNhomIndex = lsMenuNhom[i];
                        PageItems = 1;
                        LoadMon(lsMenuNhom[i].MenuNhom.NhomID);
                        OnEventMenuNhom(MenuNhomIndex);
                    }
                    SetButtonNhom((POSButtonMenu)gridGroup.Children[i], lsMenuNhom[i]);
                }

                for (int i = lsMenuNhom.Count; i < gridGroup.Children.Count; i++)
                    SetButtonEmpty((POSButtonMenu)gridGroup.Children[i]);
            }
        }
        public void SetButtonNhom(POSButtonMenu btn, Data.BOMenuNhom item)
        {
            btn.Visibility = System.Windows.Visibility.Visible;
            btn.Tag = item;
            btn.IsEnabled = true;
            btn.Content = item.MenuNhom.TenNgan;
            if (item.MenuNhom.Hinh != null && item.MenuNhom.Hinh.Length > 0)
            {
                btn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.MenuNhom.Hinh);
            }
            else
            {
                var uriSource = new Uri(@"/SystemImages;component/Images/NoImages.jpg", UriKind.Relative);
                btn.Image = new BitmapImage(uriSource);
            }
        }

        public void SetGroupPage()
        {
            btnGroupBack.Content = "Trờ Về";
            var uriSource = new Uri(@"/SystemImages;component/Images/Back.png", UriKind.Relative);
            btnGroupBack.Image = new BitmapImage(uriSource);
            btnGroupNext.Content = "Tiếp Theo";
            uriSource = new Uri(@"/SystemImages;component/Images/Forward.png", UriKind.Relative);
            btnGroupNext.Image = new BitmapImage(uriSource);
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            POSButtonMenu btn = (POSButtonMenu)sender;
            MenuNhomIndex = (Data.BOMenuNhom)btn.Tag;
            PageItems = 1;
            LoadMon(MenuNhomIndex.MenuNhom.NhomID);
            if (!_IsBanHang)
            {
                OnEventMenuNhom(MenuNhomIndex);
            }
        }

        private void btnGroupBack_Click(object sender, RoutedEventArgs e)
        {
            if (lsMenuNhom.Count > gridGroup.Children.Count)
            {
                if (PageGroup > 1)
                {
                    PageGroup--;
                    LoadGroup();
                }
            }
            else
            {
                btnGroup_Click(sender, e);
            }
        }

        private void btnGroupNext_Click(object sender, RoutedEventArgs e)
        {
            if (lsMenuNhom.Count > gridGroup.Children.Count)
            {
                if (PageGroup < (lsMenuNhom.Count / (gridGroup.Children.Count - 2)) + 1)
                {
                    PageGroup++;
                    LoadGroup();
                }
            }
            else
            {
                btnGroup_Click(sender, e);
            }
        }

        private void OnEventMenuNhom(Data.BOMenuNhom item)
        {
            if (_OnEventMenuNhom != null)
                _OnEventMenuNhom(item);
        }

        #endregion Nhóm

        #region Loại Nhóm

        private int LoaiNhomID = 0;

        private void btnNuoc_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhomID = 1;
            LoadGroup();
        }

        private void btnTatCa_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhomID = 0;
            LoadGroup();
        }

        private void btnThucAn_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhomID = 2;
            LoadGroup();
        }
        #endregion Loại Nhóm

        #region Kích thước món

        private Data.BOMenuKichThuocMon MenuKichThuocMonIndex = null;
        private int PageKichThuocMon = 1;

        private void OnEventMenuKichThuocMon(Data.BOMenuKichThuocMon item)
        {
            if (_OnEventMenuKichThuocMon != null)
            {
                if (_IsDanhSachKhuyenMai)
                {
                    BOMenuKhuyenMai.GetAll(item, mTransit);
                }
                _OnEventMenuKichThuocMon(item);
            }
        }

        public void LoadKichThuocMon(Data.BOMenuMon mon)
        {
            lsMenuKichThuocMon = BOMenuKichThuocMon.GetAll(mon.MenuMon.MonID, _IsDinhLuong, _IsThoiGian, mTransit).ToList();
            if (lsMenuKichThuocMon.Count == 1)
            {
                OnEventMenuKichThuocMon(lsMenuKichThuocMon[0]);
            }
            else if (lsMenuKichThuocMon.Count > gridItems.Children.Count)
            {
                int SoLuongKichThuocMon = gridItems.Children.Count - 2;
                List<Data.BOMenuKichThuocMon> lsKichThuocMonTem = lsMenuKichThuocMon.Skip((PageKichThuocMon - 1) * SoLuongKichThuocMon).Take(SoLuongKichThuocMon).ToList();
                bool Chay = true;
                int j = 0;
                for (int i = 0; i < lsKichThuocMonTem.Count; i++, j++)
                {
                    if (i == 0)
                    {
                        if (IsRefershMenu)
                            OnEventMenuKichThuocMon(lsMenuKichThuocMon[i]);
                    }
                    Chay = true;
                    while (Chay)
                    {
                        Chay = false;
                        if (Grid.GetRow(gridItems.Children[j]) != gridItems.RowDefinitions.Count - 1)
                            SetButtonKichThuocMon((POSButtonMenu)gridItems.Children[j], lsKichThuocMonTem[i]);
                        else if (Grid.GetColumn(gridItems.Children[j]) > 0 && Grid.GetColumn(gridItems.Children[j]) < gridItems.ColumnDefinitions.Count - 1)
                            SetButtonKichThuocMon((POSButtonMenu)gridItems.Children[j], lsKichThuocMonTem[i]);
                        else
                        {
                            Chay = true;
                            j++;
                        }
                    }
                }
                if (lsKichThuocMonTem.Count > gridItems.Children.Count - gridItems.ColumnDefinitions.Count)
                    j++;
                Chay = true;
                for (; j < gridItems.Children.Count; j++)
                {
                    Chay = true;
                    while (Chay)
                    {
                        Chay = false;
                        if (Grid.GetRow(gridItems.Children[j]) != gridItems.RowDefinitions.Count - 1)
                            SetButtonEmpty((POSButtonMenu)gridItems.Children[j]);
                        else if (Grid.GetColumn(gridItems.Children[j]) > 0 && Grid.GetColumn(gridItems.Children[j]) < gridItems.ColumnDefinitions.Count - 1)
                            SetButtonEmpty((POSButtonMenu)gridItems.Children[j]);
                        else
                        {
                            Chay = true;
                            j++;
                            if (j > gridItems.Children.Count - 1)
                                Chay = false;
                        }
                    }
                }
                SetItemPage(LoaiMenu.KichThuocMon);
            }
            else
            {
                for (int i = 0; i < lsMenuKichThuocMon.Count; i++)
                {
                    if (i == 0)
                    {
                        if (IsRefershMenu)
                            OnEventMenuKichThuocMon(lsMenuKichThuocMon[i]);
                    }
                    SetButtonKichThuocMon((POSButtonMenu)gridItems.Children[i], lsMenuKichThuocMon[i]);
                }
                for (int i = lsMenuKichThuocMon.Count; i < gridItems.Children.Count; i++)
                {
                    SetButtonEmpty((POSButtonMenu)gridItems.Children[i]);
                }
                SetItemPage(LoaiMenu.None);
            }
        }

        private void SetButtonKichThuocMon(POSButtonMenu btn, Data.BOMenuKichThuocMon item)
        {
            btn.Visibility = System.Windows.Visibility.Visible;
            btn.Tag = item;
            btn.IsEnabled = true;
            btn.Content = item.MenuKichThuocMon.TenLoaiBan;
            if (item.MenuMon.Hinh != null && item.MenuMon.Hinh.Length > 0)
            {
                btn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.MenuMon.Hinh);
            }
            else
            {
                var uriSource = new Uri(@"/SystemImages;component/Images/NoImages.jpg", UriKind.Relative);
                btn.Image = new BitmapImage(uriSource);
            }
        }

        private void SetKichThuocMonPage()
        {
            btnItemBack.Content = "Trở về";
            btnItemNext.Content = "Kế tiếp";
        }
        #endregion Kích thước món

        public void RefershMenu(bool IsNhom)
        {
            IsRefershMenu = true;
            if (IsNhom)
                LoadGroup();
            else
                LoadMon(MenuNhomIndex.MenuNhom.NhomID);
            IsRefershMenu = false;
        }

        public void SetButtonEmpty(POSButtonMenu btn)
        {
            btn.Visibility = System.Windows.Visibility.Hidden;
            btn.Content = "";
            btn.Image = null;
            btn.Background = System.Windows.Media.Brushes.Gray;
            btn.IsEnabled = false;
        }
        private void SetImageSizetItems()
        {
            int col = gridItems.ColumnDefinitions.Count;
            int row = gridItems.RowDefinitions.Count;
            ImageWidthItems = (gridItems.ActualWidth / col - 4) - 10;
            ImageHeightItems = ((gridItems.ActualHeight - 10) / row - 4) - 30;
            foreach (POSButtonMenu item in gridItems.Children)
            {
                item.ImageWidth = ImageWidthItems;
                item.ImageHeight = ImageHeightItems;
            }
            foreach (POSButtonMenu item in gridLoaiGroup.Children)
            {
                item.ImageWidth = ImageWidthItems;
                item.ImageHeight = ImageHeightItems - 20;
            }

            foreach (POSButtonMenu item in gridGroup.Children)
            {
                item.ImageWidth = ImageWidthItems;
                item.ImageHeight = ImageHeightItems;
            }
        }
    }
}