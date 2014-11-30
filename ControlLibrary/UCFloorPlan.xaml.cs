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
    /// Interaction logic for UCFloorPlan.xaml
    /// </summary>
    public partial class UCFloorPlan : UserControl
    {
        public delegate void EventFloorPlan(POSButtonTable tbl);
        public event EventFloorPlan _OnEventFloorPlan;
        public bool _IsEdit { get; set; }
        private Data.Transit mTransit;        
        public Data.KHU _Khu { get; set; }
        public Data.CAIDATBAN _CAIDATBAN 
        {
            get { return mBOBan._CAIDATBAN; }
        }        
        private Data.BOBan mBOBan;      
  
        public UCFloorPlan()
        {
            InitializeComponent();
        }

        public void Init(Data.Transit tran)
        {            
            mTransit = tran;
            mBOBan = new Data.BOBan(mTransit);            
        }       
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void LoadAlllStatus()
        {
            if (!_IsEdit)
            {
                var list = Data.BOTableStatus.GetAll(mTransit).ToList();
                foreach (POSButtonTable item in gridFloorPlan.Children)
                {
                    //if (list.Count==0)
                    //{
                    //    break;
                    //}
                    bool check = true;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (item._Ban.BanID==list[i].TableID)
                        {
                            item._ButtonTableStatusColor = (POSButtonTable.POSButtonTableStatusColor)list[i].Status;
                            list.RemoveAt(i);
                            check = false;
                            //i--;
                            break;
                        }
                    }
                    if (check)
                    {
                        item._ButtonTableStatusColor = POSButtonTable.POSButtonTableStatusColor.None;
                    }
                }
            }
        }
        public void SaveChange()
        {
            //for (int i = 0; i < this.gridFloorPlan.Children.Count; i++)                        
            //{
            //    POSButtonTable tbl = (POSButtonTable)this.gridFloorPlan.Children[i];
            //    if (tbl._Ban.BanID == 0)
            //    {                    
            //        mBOBan.Them(tbl._Ban);
            //        tbl._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.None;
            //    }
            //    else
            //    {
            //        switch (tbl._ButtonTableStatus)
            //        {
            //            case POSButtonTable.POSButtonTableStatus.Edit:                            
            //                mBOBan.Sua(tbl._Ban);
            //                tbl._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.None;
            //                break;
            //            case POSButtonTable.POSButtonTableStatus.Delete:                            
            //                mBOBan.Xoa(tbl._Ban);
            //                gridFloorPlan.Children.RemoveAt(i);
            //                i--;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
            mBOBan.Commit();
        }
        public void LoadTable()
        {
            if (_Khu != null)
            {                                
                gridFloorPlan.Children.Clear();
                IQueryable<Data.BAN> list;
                if (_IsEdit)
                {
                    list = mBOBan.GetAllTablePerArea(_Khu);
                }
                else
                {
                    list = mBOBan.GetVisualTablePerArea(_Khu);
                }
                foreach (var ban in list)
                {
                    AddTable(ban);
                }
                LoadAlllStatus();
                imgBackground.Source = Utilities.ImageHandler.BitmapImageFromByteArray(_Khu.Hinh);
            }
        }
        public void DrawTable(POSButtonTable tbl)
        {
            tbl.TableDraw(mBOBan._CAIDATBAN);
        }
        public void LoadBackgroundImage(BitmapImage img)
        {
            imgBackground.Source = img;
        }
        public void LoadTable(Data.KHU khu)
        {
            _Khu = khu;
            LoadTable();            
        }
        public void AddTable(Data.BAN ban)
        {
            POSButtonTable tbl = new POSButtonTable(mBOBan, ban,gridFloorPlan); ;
            tbl.IsEnabled = true;                 
            tbl._IsEdit = this._IsEdit;
            tbl.TableDraw(mBOBan._CAIDATBAN);
            //tbl._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.None;            
            tbl.Click += new RoutedEventHandler(tbl_Click);
            mBOBan.Them(ban);
            gridFloorPlan.Children.Add(tbl);
        }
        public void RemoveAllTable()
        {

            foreach (POSButtonTable ban in gridFloorPlan.Children)
            {                
                if (ban._Ban.BanID > 0)
                {
                    mBOBan.Xoa(ban._Ban);
                }                 
            }
            gridFloorPlan.Children.Clear();
        }
        public void RemoveTable(POSButtonTable ban)
        {
            if (ban._Ban.BanID > 0)
            {
                mBOBan.Xoa(ban._Ban);
            }
            gridFloorPlan.Children.Remove(ban);            
        }
        public void Edit(POSButtonTable ban)
        {
            mBOBan.Sua(ban._Ban);
        }
        void tbl_Click(object sender, RoutedEventArgs e)
        {
            POSButtonTable tbl = (POSButtonTable)sender;
            if (_OnEventFloorPlan != null)
            {
                _OnEventFloorPlan(tbl);
            }
        }        
    }
}
