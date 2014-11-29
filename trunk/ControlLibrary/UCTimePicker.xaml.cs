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
                if (txtHours.Text == "")
                    txtHours.Text = "0";
                if (txtMinutes.Text == "")
                    txtMinutes.Text = "0";
                if (txtSeconds.Text == "")
                    txtSeconds.Text = "0";
                return new TimeSpan(Convert.ToInt32(txtHours.Text), Convert.ToInt32(txtMinutes.Text), Convert.ToInt32(txtSeconds.Text));
            }
            set
            {
                if (value != null)
                {
                    txtHours.Text = value.Hours.ToString();
                    txtMinutes.Text = value.Minutes.ToString();
                    txtSeconds.Text = value.Seconds.ToString();
                }
                else
                {
                    txtHours.Text = "0";
                    txtMinutes.Text = "0";
                    txtSeconds.Text = "0";
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsNumber(e.Text, e.Text.Length - 1))
            {
                TextBox txt = (TextBox)sender;
                int max = Convert.ToInt32(txt.Tag);
                if (max < Convert.ToInt32(txt.Text + e.Text))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            else
                e.Handled = true;
        }
    }
}