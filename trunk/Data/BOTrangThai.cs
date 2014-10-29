using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BOTrangThai
    {

    }

    public enum EnumTrangThai
    {
        /// <summary>
        /// Không có lấy order
        /// </summary>
        BanDong,
        /// <summary>
        /// Cho lấy order
        /// </summary>
        BanMo,
        /// <summary>
        /// Sau khi bán xong
        /// </summary>
        BanHoanTat,
        /// <summary>
        ///Bàn đã in hóa đơn
        /// </summary>
        BanInHoaDon,
        /// <summary>
        /// Bàn bị hủy
        /// </summary>
        BanHuy
    }
}
