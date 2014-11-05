using System;
using System.Windows;
using System.Windows.Controls;

namespace ControlLibrary
{
    /// <summary>
    /// Interaction logic for UCTimePicker.xaml
    /// </summary>
    public partial class UCTimePicker : UserControl
    {
        public UCTimePicker()
        {
            InitializeComponent();
        }

        public TimeSpan TimeCurent
        {
            get
            {
                return new TimeSpan(Convert.ToInt32(txtHours.Text), Convert.ToInt32(txtMinutes.Text), Convert.ToInt32(txtSeconds.Text));
            }
            set
            {
                if (value != null)
                {
                    txtHours.Text = value.Hours.ToString("00");
                    txtMinutes.Text = value.Minutes.ToString("00");
                    txtSeconds.Text = value.Seconds.ToString("00");
                }
                else
                {
                    txtHours.Text = "00";
                    txtMinutes.Text = "00";
                    txtSeconds.Text = "00";
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}