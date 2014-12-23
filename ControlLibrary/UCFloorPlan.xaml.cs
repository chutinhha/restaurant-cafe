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
                var list = Data.BOTableStatus.GetAll(mTransit.KaraokeEntities).ToList();
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
            foreach (POSButtonTable item in gridFloorPlan.Children)
            {
                if (item._Ban.BanID==0)
                {
                    mBOBan.Them(item._Ban);
                }
                else if(item._ButtonTableStatus==POSButtonTable.POSButtonTableStatus.Edit)
                {
                    mBOBan.Sua(item._Ban);
                }
            }
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
            POSButtonTable tbl = new POSButtonTable(ban,gridFloorPlan); ;
            tbl.IsEnabled = true;                 
            tbl._IsEdit = this._IsEdit;
            tbl.TableDraw(mBOBan._CAIDATBAN);                     
            tbl.Click += new RoutedEventHandler(tbl_Click);            
            gridFloorPlan.Children.Add(tbl);
        }
        public void AddTable(int numOfTable)
        {
            double width = this.RenderSize.Width;
            double height = this.RenderSize.Height;
            double pading = 10;
            double padingTop = height > 0 ? pading / height : 0;
            double padingLeft = width > 0 ? pading / width : 0;
            
            double x = padingLeft;
            double y = padingTop;
            double dCol = Math.Sqrt(numOfTable);
            int column = (int)dCol;
            if (dCol > column)
                column++;
            double dRow = (double)numOfTable / column;
            int row = (int)dRow;
            if (dRow > row)
                row++;
            int total = 0;
            int itemRow = 0;
            double itemWidth = column > 0 ? (1 - (column + 1) * padingLeft) / column : 0;
            double itemHeight = row > 0 ? (1 - (row + 1) * padingTop) / row : 0;
            for (int i = 0; i < row; i++)
            {
                itemRow = numOfTable - total;
                if (itemRow>column)                
                    itemRow = column;
                total += itemRow;
                x = (1-itemWidth * itemRow - padingLeft * (itemRow + 1))/2;
                x += padingLeft;
                for (int j = 0; j < itemRow; j++)
                {
                    Data.BAN ban = new Data.BAN();
                    ban.BanID = 0;
                    ban.TenBan = String.Format("{0}",i*column+j+1);
                    ban.KhuID = _Khu.KhuID;
                    ban.LocationX = (decimal)x;
                    ban.LocationY = (decimal)y;
                    ban.Width = (decimal)itemWidth;
                    ban.Height = (decimal)itemHeight;
                    ban.Visual = true;
                    ban.Deleted = false;
                    ban.Hinh = null;
                    this.AddTable(ban);

                    x += itemWidth + padingLeft;
                }
                y += itemHeight + padingTop;
            }
        }
        public void RemoveAllTable()
        {

            foreach (POSButtonTable ban in gridFloorPlan.Children)
            {
                if (ban._Ban.BanID>0)
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
            ban._ButtonTableStatus = POSButtonTable.POSButtonTableStatus.Edit;
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
