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

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {
        List<string> lsGroup = new List<string>();
        public UCMenu()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                lsGroup.Add("Group " + (i + 1));
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

        public void LoadItem()
        {

        }

        #region Group
        private int PageGroup = 0;

        public void LoadGroup()
        {
            if (lsGroup.Count > gridGroup.Children.Count)
            {
                int CountGroup = gridGroup.Children.Count - 2;
                List<string> lsGroupTem = lsGroup.Skip((PageGroup - 1) * CountGroup).Take(CountGroup).ToList();
                for (int i = 0; i < lsGroupTem.Count; i++)
                {
                    SetButtonGroup((Button)gridGroup.Children[i], lsGroupTem[i + 1]);
                }
                for (int i = lsGroupTem.Count; i < CountGroup; i++)
                {
                    SetEmptyButton((Button)gridGroup.Children[i + 1]);
                }
                SetGroupPage();
            }
            else
            {
                for (int i = 0; i < lsGroup.Count; i++)
                    SetButtonGroup((Button)gridGroup.Children[i], lsGroup[i]);

                for (int i = lsGroup.Count; i < gridGroup.Children.Count; i++)
                    SetEmptyButton((Button)gridGroup.Children[i]);
            }
        }
        private void btnGroupFirst_Click(object sender, RoutedEventArgs e)
        {
            if (lsGroup.Count > gridGroup.Children.Count)
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

        private void btnGroupLast_Click(object sender, RoutedEventArgs e)
        {
            if (lsGroup.Count > gridGroup.Children.Count)
            {
                if (PageGroup < (lsGroup.Count / (gridGroup.Children.Count - 2)) + 1)
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

        public void SetButtonGroup(Button btn, string item)
        {
            btn.IsEnabled = true;
            btn.Content = item;
        }

        public void SetGroupPage()
        {
            btnGroupFirst.Content = "Back";
            btnGroupLast.Content = "Next";
        }

        #endregion

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {

        }
        public void SetEmptyButton(Button btn)
        {
            btn.Content = "";
            btn.IsEnabled = false;

        }


    }
}
