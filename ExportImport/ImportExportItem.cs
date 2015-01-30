using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportImport
{
    public class ImportExportItem
    {
        public string TenNhom { get; set; }
        public string TenMon { get; set; }        
        public string ChiTietMon { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongBanMacDinh { get; set; }

        
        public static ImportExportItem GetProductData(IList<string> rowData, IList<string> columnNames)
        {
            ImportExportItem entity = (ImportExportItem)Activator.CreateInstance(typeof(ImportExportItem));
            GetValue(entity, rowData, columnNames);
            return entity;
        }
        public static void GetValue(ImportExportItem data, IList<string> rowData, IList<string> columnNames)
        {
            foreach (var item in typeof(ImportExportItem).GetProperties())
            {
                item.SetValue(data, ConvertType(item.PropertyType, rowData[columnNames.IndexOf(item.Name.ToLower())]), null);
            }
        }
        private static object ConvertType(Type type, string value)
        {
            if (type == typeof(int))
            {
                return ExcelReader.ToInt32(value);
            }
            if (type == typeof(int?))
            {
                return ExcelReader.ToInt32Nullable(value);
            }
            if (type == typeof(decimal))
            {
                return ExcelReader.ToDecimal(value);
            }
            if (type == typeof(decimal?))
            {
                return ExcelReader.ToDecimalNullable(value);
            }
            if (type == typeof(bool))
            {
                return ExcelReader.ToBoolean(value);
            }
            if (type == typeof(bool?))
            {
                return ExcelReader.ToBooleanNullable(value);
            }
            if (type == typeof(DateTime))
            {
                return ExcelReader.ToDateTime(value);
            }
            if (type == typeof(DateTime?))
            {
                return ExcelReader.ToDateTimeNullable(value);
            }
            return value;
        }      
    }
}
