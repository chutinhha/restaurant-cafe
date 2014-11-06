using System.Windows.Controls;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UCKhuyenMai.xaml
    /// </summary>
    public partial class UCKhuyenMai : UserControl
    {
        private Data.Transit mTransit = null;

        public UCKhuyenMai(Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
        }
    }
}