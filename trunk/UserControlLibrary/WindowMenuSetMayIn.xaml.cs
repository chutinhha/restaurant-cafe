using System.Windows;

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for WindowMenuSetMayIn.xaml
    /// </summary>
    public partial class WindowMenuSetMayIn : Window
    {
        private Data.Transit mTransit = null;

        public WindowMenuSetMayIn(Data.BOMenuMon mon, Data.Transit transit)
        {
            InitializeComponent();
            mTransit = transit;
            uCMenuSetMayIn.OnEventExit += new UCMenuSetMayIn.OnExit(uCMenuSetMayIn_OnEventExit);
            uCMenuSetMayIn.Init(mTransit);
            uCMenuSetMayIn.SetValues(mon);
        }

        private void uCMenuSetMayIn_OnEventExit()
        {
            this.Close();
        }        
    }
}