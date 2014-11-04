using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.ProcessOrder
{
    public class BanHang
    {
        public ChiTietBanHang ChiTietBanHangs { get; set; }
        public BanHang()
        {
            ChiTietBanHangs = new ChiTietBanHang();
        }        
    }
}
