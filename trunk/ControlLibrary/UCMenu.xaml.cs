using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {

        public class Menu
        {
            public string TenNgan { get; set; }
        }
        private List<Menu> lsMenuMon = new List<Menu>();
        private List<Menu> lsMenuNhom = new List<Menu>();


        public UCMenu()
        {
            InitializeComponent();            
            for (int i = 0; i < 10; i++)
            {                
                //lsMenuNhom.Add("Group " + (i + 1));
            }

            for (int i = 0; i < 24; i++)
            {
                //lsMenuMon.Add("Item " + (i + 1));
            }
        }

        public void Init()
        {
            LoadData();
        }

        public void LoadData()
        {
            PageGroup = 1;
            LoadGroup();
        }

        #region Items

        private int PageItems = 0;

        public void LoadItem(int GroupID)
        {
            if (GroupID != 0)
            {
                PageItems = 0;
            }
            List<Menu> lsItemsTem = lsMenuMon.Skip((PageItems - 1) * gridItems.Children.Count).Take(gridItems.Children.Count).ToList();
            for (int i = 0; i < lsItemsTem.Count; i++)
            {
                SetButtonItem((Button)gridItems.Children[i], lsItemsTem[i]);
            }
            for (int i = lsItemsTem.Count; i < gridItems.Children.Count; i++)
            {
                SetButtonEmpty((Button)gridItems.Children[i]);
            }
        }

        private void btnItemBack_Click(object sender, RoutedEventArgs e)
        {
            if (PageItems > 1)
            {
                PageItems--;
                LoadItem(0);
            }
        }

        private void btnItemNext_Click(object sender, RoutedEventArgs e)
        {
            if (PageItems < lsMenuMon.Count / gridItems.Children.Count + 1)
            {
                PageItems--;
                LoadItem(0);
            }
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SetButtonItem(Button btn, Menu item)
        {
            btn.IsEnabled = true;
            btn.Content = item.TenNgan;
        }

        #endregion Items

        #region Nhóm

        private int PageGroup = 0;

        public void LoadGroup()
        {
            if (lsMenuNhom.Count > gridGroup.Children.Count)
            {
                int CountGroup = gridGroup.Children.Count - 2;
                List<Menu> lsGroupTem = lsMenuNhom.Skip((PageGroup - 1) * CountGroup).Take(CountGroup).ToList();
                for (int i = 0; i < lsGroupTem.Count; i++)
                {
                    if (i == 0)
                        LoadItem(0);
                    SetButtonGroup((Button)gridGroup.Children[i], lsGroupTem[i + 1]);
                }
                for (int i = lsGroupTem.Count; i < CountGroup; i++)
                {
                    SetButtonEmpty((Button)gridGroup.Children[i + 1]);
                }
                SetGroupPage();
            }
            else
            {
                for (int i = 0; i < lsMenuNhom.Count; i++)
                {
                    if (i == 0)
                        LoadItem(0);
                    SetButtonGroup((Button)gridGroup.Children[i], lsMenuNhom[i]);
                }

                for (int i = lsMenuNhom.Count; i < gridGroup.Children.Count; i++)
                    SetButtonEmpty((Button)gridGroup.Children[i]);
            }
        }

        public void SetButtonGroup(Button btn, Menu item)
        {
            btn.IsEnabled = true;
            btn.Content = item.TenNgan;
        }

        public void SetGroupPage()
        {
            btnGroupBack.Content = "Trờ Về";
            btnGroupNext.Content = "Tiếp Theo";
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            LoadItem(1);
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

        }

        private void btnThucAn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTatCa_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        public void SetButtonEmpty(Button btn)
        {
            btn.Content = "";
            btn.IsEnabled = false;
        }


    }
}