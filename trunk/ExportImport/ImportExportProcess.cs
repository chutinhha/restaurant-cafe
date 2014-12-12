using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportImport
{
    public class ImportExportProcess
    {
        public delegate void EventImportExport(string log,bool isError);
        public event EventImportExport _OnImportExport;
        Data.KaraokeEntities mKaraokeEntities;

        private static string LOAINHOM = "Loại Nhóm";
        private static string MAYIN = "Máy In";
        private static string LOAIGIA = "Loại Giá";
        private static string DONVI = "Đơn Vị";
        private static string LOAIDONVIBAN = "Loại Đơn Vị Bán";
        private static string NHOM = "Nhóm";
        private static string MON = "Món";
        private static string CHITIETMON = "Chi Tiết Món";
        private static string CHITIETGIA = "Chi Tiết Giá";
        private static string MONMAYIN = "Món Máy In";
        public ImportExportProcess()
        {
            mKaraokeEntities = new Data.KaraokeEntities();
        }
        
        public void Export(string url)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();

                _OnImportExport(String.Format("Đọc {0}...",LOAINHOM), false);
                ds.Tables.Add(ObjectConvert<Data.MENULOAINHOM>.GetTableData(mKaraokeEntities.MENULOAINHOMs, LOAINHOM));

                _OnImportExport(String.Format("Đọc {0}...", MAYIN), false);                                
                ds.Tables.Add(ObjectConvert<Data.MAYIN>.GetTableData(mKaraokeEntities.MAYINs, MAYIN));

                _OnImportExport(String.Format("Đọc {0}...", LOAIGIA), false);                
                ds.Tables.Add(ObjectConvert<Data.MENULOAIGIA>.GetTableData(mKaraokeEntities.MENULOAIGIAs, LOAIGIA));

                _OnImportExport(String.Format("Đọc {0}...", DONVI), false);                
                ds.Tables.Add(ObjectConvert<Data.DONVI>.GetTableData(mKaraokeEntities.DONVIs, DONVI));

                _OnImportExport(String.Format("Đọc {0}...", LOAIDONVIBAN), false);                
                ds.Tables.Add(ObjectConvert<Data.LOAIBAN>.GetTableData(mKaraokeEntities.LOAIBANs, LOAIDONVIBAN));

                _OnImportExport(String.Format("Đọc {0}...", NHOM), false);                
                ds.Tables.Add(ObjectConvert<Data.MENUNHOM>.GetTableData(mKaraokeEntities.MENUNHOMs, NHOM));

                _OnImportExport(String.Format("Đọc {0}...", MON), false);                
                ds.Tables.Add(ObjectConvert<Data.MENUMON>.GetTableData(mKaraokeEntities.MENUMONs, MON));

                _OnImportExport(String.Format("Đọc {0}...",CHITIETMON), false);
                ds.Tables.Add(ObjectConvert<Data.MENUKICHTHUOCMON>.GetTableData(mKaraokeEntities.MENUKICHTHUOCMONs, CHITIETMON));

                _OnImportExport(String.Format("Đọc {0}...", CHITIETGIA), false);
                ds.Tables.Add(ObjectConvert<Data.MENUGIA>.GetTableData(mKaraokeEntities.MENUGIAs, CHITIETGIA));

                _OnImportExport(String.Format("Đọc {0}...", MONMAYIN), false);
                ds.Tables.Add(ObjectConvert<Data.MENUITEMMAYIN>.GetTableData(mKaraokeEntities.MENUITEMMAYINs, MONMAYIN));

                _OnImportExport("Lưu tập tin..." + url, false);
                CreateExcelFile.CreateExcelDocument(ds, url);                                

                _OnImportExport("Xuất dữ liệu thành công...", false);
            }
            catch (Exception ex)
            {
                _OnImportExport("Lỗi..." + ex.Message, true);
            }
        }
        
        public void Import(string url)
        {
            try
            {
                _OnImportExport("----------------------------", false);
                _OnImportExport(String.Format("Đọc {0}...", LOAINHOM), false);
                var listLoaiNhom = ExcelReader.GetDataToList(url,LOAINHOM, ObjectConvert<Data.MENULOAINHOM>.GetProductData);
                foreach (var item in listLoaiNhom)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.LoaiNhomID, item.TenLoaiNhom), false);
                    var query=from x in mKaraokeEntities.MENULOAINHOMs
                                                  where x.LoaiNhomID == item.LoaiNhomID
                                                  select x;
                    string resuilt = ObjectConvert<Data.MENULOAINHOM>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENULOAINHOMs, item);
                    if (resuilt!="")
                    {
                        _OnImportExport(resuilt, true);        
                    }
                }
                _OnImportExport("----------------------------", false);
                _OnImportExport(String.Format("Đọc {0}...", MAYIN), false);
                var listMayIn = ExcelReader.GetDataToList(url, MAYIN, ObjectConvert<Data.MAYIN>.GetProductData);
                foreach (var item in listMayIn)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.MayInID, item.TenMayIn), false);
                    var query = from x in mKaraokeEntities.MAYINs
                                where x.MayInID == item.MayInID
                                select x;
                    string resuilt = ObjectConvert<Data.MAYIN>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MAYINs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }


                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc loại giá...", false);
                var listLoaiGia = ExcelReader.GetDataToList(url, LOAIGIA, ObjectConvert<Data.MENULOAIGIA>.GetProductData);
                foreach (var item in listLoaiGia)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.LoaiGiaID, item.Ten), false);
                    var query = from x in mKaraokeEntities.MENULOAIGIAs
                                where x.LoaiGiaID == item.LoaiGiaID
                                select x;
                    string resuilt = ObjectConvert<Data.MENULOAIGIA>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENULOAIGIAs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc đơn vị...", false);
                var listDonVi = ExcelReader.GetDataToList(url, DONVI, ObjectConvert<Data.DONVI>.GetProductData);
                foreach (var item in listDonVi)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.DonViID, item.TenDonVi), false);
                    var query = from x in mKaraokeEntities.DONVIs
                                where x.DonViID == item.DonViID
                                select x;
                    string resuilt = ObjectConvert<Data.DONVI>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.DONVIs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc loại đơn vị...", false);                
                var listLoaiDonVi = ExcelReader.GetDataToList(url, LOAIDONVIBAN, ObjectConvert<Data.LOAIBAN>.GetProductData);
                foreach (var item in listLoaiDonVi)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.LoaiBanID, item.TenLoaiBan), false);
                    var query = from x in mKaraokeEntities.LOAIBANs
                                where x.LoaiBanID == item.LoaiBanID
                                select x;
                    string resuilt = ObjectConvert<Data.LOAIBAN>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.LOAIBANs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc nhóm...", false);
                var listNhom = ExcelReader.GetDataToList(url, NHOM, ObjectConvert<Data.MENUNHOM>.GetProductData);
                foreach (var item in listNhom)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.NhomID, item.TenNgan), false);
                    var query = from x in mKaraokeEntities.MENUNHOMs
                                where x.NhomID == item.NhomID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUNHOM>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUNHOMs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc món...", false);
                var listMon = ExcelReader.GetDataToList(url, MON, ObjectConvert<Data.MENUMON>.GetProductData);
                foreach (var item in listMon)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.MonID, item.TenNgan), false);
                    var query = from x in mKaraokeEntities.MENUMONs
                                where x.MonID == item.MonID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUMON>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUMONs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc chi tiết món...", false);
                var listChiTietMon = ExcelReader.GetDataToList(url, CHITIETMON, ObjectConvert<Data.MENUKICHTHUOCMON>.GetProductData);
                foreach (var item in listChiTietMon)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.KichThuocMonID, item.TenLoaiBan), false);
                    var query = from x in mKaraokeEntities.MENUKICHTHUOCMONs
                                where x.KichThuocMonID == item.KichThuocMonID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUKICHTHUOCMON>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUKICHTHUOCMONs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }


                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc chi tiết giá...", false);
                var listChiTietGia = ExcelReader.GetDataToList(url, CHITIETGIA, ObjectConvert<Data.MENUGIA>.GetProductData);
                foreach (var item in listChiTietGia)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.GiaID, item.Gia), false);
                    var query = from x in mKaraokeEntities.MENUGIAs
                                where x.GiaID == item.GiaID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUGIA>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUGIAs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("----------------------------", false);
                _OnImportExport("Đọc món máy in...", false);
                var listMonMayIn = ExcelReader.GetDataToList(url, MONMAYIN, ObjectConvert<Data.MENUITEMMAYIN>.GetProductData);
                foreach (var item in listMonMayIn)
                {
                    _OnImportExport(String.Format("Cập nhật...{0} - {1}", item.MayInID, item.MonID), false);
                    var query = from x in mKaraokeEntities.MENUITEMMAYINs
                                where x.MayInID == item.MayInID && x.MonID==item.MonID
                                select x;
                    string resuilt = ObjectConvert<Data.MENUITEMMAYIN>.UpdateData(mKaraokeEntities, query, mKaraokeEntities.MENUITEMMAYINs, item);
                    if (resuilt != "")
                    {
                        _OnImportExport(resuilt, true);
                    }
                }

                _OnImportExport("Nhập dữ liệu thành công...", false);
                //============================
                
            }
            catch (Exception ex)
            {
                _OnImportExport("Lỗi..."+ex.Message, false);
            }            

        }

        private void OnImport(string log,bool isError)
        {
            if (_OnImportExport!=null)
	        {
	            _OnImportExport(log,isError);
	        }
        }

        public static Data.MENULOAINHOM GetMenuLoaiNhom(IList<string> rowData, IList<string> columnNames)
        {
            var data = new Data.MENULOAINHOM();
            ObjectConvert<Data.MENULOAINHOM>.GetValue(data, rowData, columnNames);
            return data;
        }
        
    }
}
