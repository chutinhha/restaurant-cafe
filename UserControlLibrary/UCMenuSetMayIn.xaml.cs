using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Linq;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenuSetMayIn.xaml
    /// </summary>
    public partial class UCMenuSetMayIn : UserControl
    {
        private Data.Transit mTransit = null;

        public UCMenuSetMayIn()
        {
            InitializeComponent();
        }

        public delegate void OnExit();

        public event OnExit OnEventExit;

        public Data.BOMenuMon _Mon { get; set; }

        private Data.BOMenuItemMayIn BOMenuItemMayIn = null;

        public void Init(Data.Transit transit)
        {
            mTransit = transit;
            BOMenuItemMayIn = new Data.BOMenuItemMayIn(mTransit);
            if (OnEventExit == null)
                btnHuy.Visibility = System.Windows.Visibility.Hidden;
        }

        public void SetValues(Data.BOMenuMon mon)
        {
            _Mon = mon;
            if (_Mon != null)
            {
                txtTenMon.Text = _Mon.MenuMon.TenDai;
                LoadDanhSach();
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            if (OnEventExit != null)
            {
                OnEventExit();
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            List<Data.BOMenuItemMayIn> lsArray = new List<Data.BOMenuItemMayIn>();
            List<Data.BOMenuItemMayIn> lsArrayDeleted = new List<Data.BOMenuItemMayIn>();
            foreach (ShowData item in lvData.Items)
            {
                if (item.Values == true)
                {
                    if (item.MenuItemMayIn.MenuItemMayIn.MayInID == 0 || item.MenuItemMayIn.MenuItemMayIn.MayInID == null)
                    {
                        item.MenuItemMayIn.MenuItemMayIn.MayInID = item.MenuItemMayIn.MayIn.MayInID;
                        item.MenuItemMayIn.MenuItemMayIn.MonID = _Mon.MenuMon.MonID;
                        item.MenuItemMayIn.MenuItemMayIn.Deleted = false;
                        item.MenuItemMayIn.MenuItemMayIn.Visual = true;
                        lsArray.Add(item.MenuItemMayIn);
                    }
                }
                else
                {
                    if (item.MenuItemMayIn.MenuItemMayIn.MayInID > 0)
                    {
                        lsArrayDeleted.Add(item.MenuItemMayIn);
                    }
                }
            }
            BOMenuItemMayIn.Luu(lsArray, lsArrayDeleted, mTransit);
            LoadDanhSach();
            UserControlLibrary.WindowMessageBox messageBox = new UserControlLibrary.WindowMessageBox(mTransit.StringButton.LuuThanhCong);
            messageBox.ShowDialog();
        }

        private void LoadDanhSach()
        {
            IQueryable<Data.MAYIN> lsMayIn = Data.BOMayIn.GetAllNoTracking(mTransit, false);
            IQueryable<Data.BOMenuItemMayIn> lsMonMayIn = BOMenuItemMayIn.GetAll(_Mon.MenuMon.MonID, mTransit);
            List<ShowData> lsShowData = new List<ShowData>();
            foreach (Data.MAYIN mi in lsMayIn)
            {
                ShowData item = null;
                if (lsMonMayIn.Where(s => s.MayIn.MayInID == mi.MayInID).Count() > 0)
                {
                    item = new ShowData(lsMonMayIn.Where(s => s.MayIn.MayInID == mi.MayInID).FirstOrDefault(), true);
                }
                else
                {
                    item = new ShowData();
                    item.MenuItemMayIn.MayIn = mi;
                }
                lsShowData.Add(item);
            }
            lvData.ItemsSource = lsShowData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetValues(_Mon);
        }

        private class ShowData
        {
            public Data.BOMenuItemMayIn MenuItemMayIn { get; set; }
            public bool Values { get; set; }

            public ShowData()
            {
                MenuItemMayIn = new Data.BOMenuItemMayIn();
                Values = false;
            }

            public ShowData(Data.BOMenuItemMayIn menuItemMayIn, bool value)
            {
                MenuItemMayIn = menuItemMayIn;
                Values = value;
            }
        }        
    }
}