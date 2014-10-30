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
                return new TimeSpan(Convert.ToInt32(cbbHours.Text), Convert.ToInt32(cbbMinutes.Text), Convert.ToInt32(cbbSeconds.Text));
            }
            set
            {
                cbbHours.Text = value.Hours.ToString();
                cbbMinutes.Text = value.Minutes.ToString();
                cbbSeconds.Text = value.Seconds.ToString();
            }
        }

        public TimeSpan TimeMax { get; set; }

        public TimeSpan TimeMin { get; set; }

        private void LoadHours()
        {
            cbbHours.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                cbbHours.Items.Add((i).ToString());
            }
            cbbHours.SelectedIndex = 0;
        }

        private void LoadMinutes()
        {
            cbbMinutes.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                cbbMinutes.Items.Add((i * 5).ToString());
            }
            cbbMinutes.SelectedIndex = 0;
        }

        private void LoadSeconds()
        {
            cbbSeconds.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                cbbSeconds.Items.Add((i * 5).ToString());
            }
            cbbSeconds.SelectedIndex = 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHours();
            LoadMinutes();
            LoadSeconds();
            
        }
    }
}