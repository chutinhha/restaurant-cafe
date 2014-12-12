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
using System.Windows.Shapes;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowMenuExport.xaml
    /// </summary>
    public partial class WindowMenuExport : Window
    {
        private Data.Transit mTranSit;
        private ExportImport.ImportExportProcess mImportExportProcess;
        public WindowMenuExport(Data.Transit transit)
        {
            mTranSit = transit;
            mImportExportProcess = new ExportImport.ImportExportProcess();
            mImportExportProcess._OnImportExport += new ExportImport.ImportExportProcess.EventImportExport(mImportExportProcess__OnImportExport);
            InitializeComponent();
        }

        void mImportExportProcess__OnImportExport(string log, bool isError)
        {
            Paragraph para = new Paragraph();
            if (isError)
                para.Foreground = Brushes.Red;
            para.Inlines.Add(new Run(log));                        
            richTextBox1.Document.Blocks.Add(para);
            richTextBox1.ScrollToEnd();
        }
        

        private void btnXuat_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.InitialDirectory = mTranSit.DuongDanHinh;
            dlg.Filter = "Excel Files | *.xlsx";
            if (dlg.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                //ExportImport.ImportExportProcess.Export(dlg.FileName);
                mImportExportProcess.Export(dlg.FileName);                
            }
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = mTranSit.DuongDanHinh;
            dlg.Filter = "Excel Files | *.xlsx";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mImportExportProcess.Import(dlg.FileName);                
            }
        }
    }
}
