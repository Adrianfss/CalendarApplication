using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CalendarApplication.CallbackInterface;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CalendarApplication.Models;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// Interaction logic for CreateCalendarViewModel.xaml
    /// </summary>
    public partial class CreateCalendarViewModel : Window 
    {
        private CalendarEntrie calendarEntrie;
        private IOnCreateCallback _onCreateCallback;
        public CreateCalendarViewModel(IOnCreateCallback onCreateCallback)
        {
            calendarEntrie = new CalendarEntrie { StartTime = DateTime.Now, EndTime = DateTime.Now };
            DataContext = calendarEntrie;
            _onCreateCallback = onCreateCallback;

            InitializeComponent();
        }

        private void Save_Changes_Click(object sender, RoutedEventArgs e)
        {

            _onCreateCallback.OnCreate(calendarEntrie);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
