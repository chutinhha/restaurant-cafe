using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {
        private List<Data.MENUMON> lsMenuMon = new List<Data.MENUMON>();
        private List<Data.MENUNHOM> lsMenuNhom = new List<Data.MENUNHOM>();
        private Data.Transit mTransit = null;
        public bool _IsBanHang { get; set; }

        public delegate void EventMenu(object ob);
        public event EventMenu OnEventMenu;

        public UCMenu()
        {
            InitializeComponent();
            _IsBanHang = false;
        }

        public void Init(Data.Transit transit)
        {
            SetImageSizetItems();
            mTransit = transit;
            LoadData();
        }

        public void LoadData()
        {
            PageGroup = 1;
            LoadGroup();
        }

        #region Items

        private int PageItems = 1;

        private void SetItemPage()
        {
            btnItemBack.Content = "Trở về";
            btnItemNext.Content = "Kế tiếp";
        }
        public void LoadItem(int GroupID)
        {
            lsMenuMon = Data.BOMenuMon.GetAll(GroupID, mTransit);
            if (lsMenuMon.Count > gridItems.Children.Count)
            {
                int CountItems = gridItems.Children.Count - 2;
                List<Data.MENUMON> lsItemsTem = lsMenuMon.Skip((PageItems - 1) * CountItems).Take(CountItems).ToList();
                bool Chay = true;
                int j = 0;
                for (int i = 0; i < lsItemsTem.Count; i++, j++)
                {
                    if (i == 0)
                    {
                        if (IsRefershMenu)
                            OnEventMenu(lsItemsTem[i]);
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
                SetItemPage();
            }
            else
            {
                for (int i = 0; i < lsMenuMon.Count; i++)
                {
                    if (i == 0)
                    {
                        if (IsRefershMenu)
                            OnEventMenu(lsMenuMon[i]);
                    }
                    SetButtonItem((POSButtonMenu)gridItems.Children[i], lsMenuMon[i]);
                }
                for (int i = lsMenuMon.Count; i < gridItems.Children.Count; i++)
                {
                    SetButtonEmpty((POSButtonMenu)gridItems.Children[i]);
                }
            }
        }

        private void btnItemBack_Click(object sender, RoutedEventArgs e)
        {
            if (MenuNhomIndex != null)

                if (PageItems > 1)
                {
                    PageItems--;
                    LoadItem(MenuNhomIndex.NhomID);
                }
        }

        private void btnItemNext_Click(object sender, RoutedEventArgs e)
        {

            if (lsMenuMon.Count > gridItems.Children.Count)
            {
                if (MenuNhomIndex != null)
                    if (PageItems < lsMenuMon.Count / (gridItems.Children.Count - 2) + 1)
                    {
                        PageItems++;
                        LoadItem(MenuNhomIndex.NhomID);
                    }
            }
        }

        private Data.MENUMON MenuMonIndex = null;
        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            POSButtonMenu btn = (POSButtonMenu)sender;
            MenuMonIndex = (Data.MENUMON)btn.Tag;
            PageItems = 1;
            OnEventMenu(MenuMonIndex);
        }

        private void SetButtonItem(POSButtonMenu btn, Data.MENUMON item)
        {
            btn.Tag = item;
            btn.IsEnabled = true;
            btn.Content = item.TenNgan;
            if (item.Hinh != null && item.Hinh.Length > 0)
            {
                btn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.Hinh);
            }
            else
            {
                var uriSource = new Uri(@"/ControlLibrary;component/Images/NoImages.jpg", UriKind.Relative);
                btn.Image = new BitmapImage(uriSource);
            }
        }

        #endregion Items

        #region Nhóm

        private int PageGroup = 0;

        public void LoadGroup()
        {

            if (LoaiNhomID == 0)
                lsMenuNhom = Data.BOMenuNhom.GetAll(-1, mTransit);
            else
                lsMenuNhom = Data.BOMenuNhom.GetAll(LoaiNhomID, mTransit);

            if (lsMenuNhom.Count > gridGroup.Children.Count)
            {
                int CountGroup = gridGroup.Children.Count - 2;
                List<Data.MENUNHOM> lsGroupTem = lsMenuNhom.Skip((PageGroup - 1) * CountGroup).Take(CountGroup).ToList();
                for (int i = 0; i < lsGroupTem.Count; i++)
                {
                    if (i == 0)
                    {
                        MenuNhomIndex = lsGroupTem[i];
                        PageItems = 1;
                        LoadItem(lsMenuNhom[i].NhomID);
                        OnEventMenu(MenuNhomIndex);

                    }
                    SetButtonGroup((POSButtonMenu)gridGroup.Children[i + 1], lsGroupTem[i]);
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
                        LoadItem(lsMenuNhom[i].NhomID);
                        OnEventMenu(MenuNhomIndex);
                    }
                    SetButtonGroup((POSButtonMenu)gridGroup.Children[i], lsMenuNhom[i]);
                }

                for (int i = lsMenuNhom.Count; i < gridGroup.Children.Count; i++)
                    SetButtonEmpty((POSButtonMenu)gridGroup.Children[i]);
            }
        }

        private Data.MENUNHOM MenuNhomIndex = null;

        public void SetButtonGroup(POSButtonMenu btn, Data.MENUNHOM item)
        {
            btn.Tag = item;
            btn.IsEnabled = true;
            btn.Content = item.TenNgan;
            if (item.Hinh != null && item.Hinh.Length > 0)
            {
                btn.Image = Utilities.ImageHandler.BitmapImageFromByteArray(item.Hinh);
            }
            else
            {
                var uriSource = new Uri(@"/ControlLibrary;component/Images/NoImages.jpg", UriKind.Relative);
                btn.Image = new BitmapImage(uriSource);
            }
        }

        public void SetGroupPage()
        {
            btnGroupBack.Content = "Trờ Về";
            var uriSource = new Uri(@"/ControlLibrary;component/Images/Back.png", UriKind.Relative);
            btnGroupBack.Image = new BitmapImage(uriSource);
            btnGroupNext.Content = "Tiếp Theo";
            uriSource = new Uri(@"/ControlLibrary;component/Images/Forward.png", UriKind.Relative);
            btnGroupNext.Image = new BitmapImage(uriSource);
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            POSButtonMenu btn = (POSButtonMenu)sender;
            MenuNhomIndex = (Data.MENUNHOM)btn.Tag;
            PageItems = 1;
            LoadItem(MenuNhomIndex.NhomID);
            if (!_IsBanHang)
            {
                OnEventMenu(MenuNhomIndex);
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

        #endregion Nhóm

        #region Loại Nhóm

        private void btnNuoc_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhomID = 1;
            LoadGroup();
        }

        private void btnThucAn_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhomID = 2;
            LoadGroup();
        }

        private int LoaiNhomID = 0;

        private void btnTatCa_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhomID = 0;
            LoadGroup();
        }

        #endregion Loại Nhóm

        public void SetButtonEmpty(POSButtonMenu btn)
        {
            btn.Content = "";
            btn.IsEnabled = false;
        }


        private bool IsRefershMenu = false;
        public void RefershMenu(bool IsNhom)
        {
            IsRefershMenu = true;
            if (IsNhom)
                LoadGroup();
            else
                LoadItem(MenuNhomIndex.NhomID);
            IsRefershMenu = false;
        }

        private double ImageWidthItems = 0;
        private double ImageHeightItems = 0;
        private void SetImageSizetItems()
        {
            int col = gridItems.ColumnDefinitions.Count;
            int row = gridItems.RowDefinitions.Count;
            ImageWidthItems = gridItems.ActualWidth / col - (col * 4);
            ImageHeightItems = gridItems.ActualHeight / row - (row * 24);
            foreach (POSButtonMenu item in gridItems.Children)
            {
                item.ImageWidth = ImageWidthItems;
                item.ImageHeight = ImageHeightItems;
            }
            foreach (POSButtonMenu item in gridLoaiGroup.Children)
            {
                item.ImageWidth = ImageWidthItems;
                item.ImageHeight = ImageHeightItems;
            }

            foreach (POSButtonMenu item in gridGroup.Children)
            {
                item.ImageWidth = ImageWidthItems;
                item.ImageHeight = ImageHeightItems;
            }
        }

        private void btnGioKaraoke_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}