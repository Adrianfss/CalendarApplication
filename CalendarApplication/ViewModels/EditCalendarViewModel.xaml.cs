using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CalendarApplication.Models;
using CalendarApplication.CallbackInterface;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// Interaction logic for EditCalendarViewModel.xaml
    /// </summary>
    public partial class EditCalendarViewModel : Window
    {
        private readonly IOnChangeCallback _onChangeCallback;
        public EditCalendarViewModel(CalendarEntrie entrie, IOnChangeCallback onChangeCallback)
        {
            _onChangeCallback = onChangeCallback;
            DataContext = entrie;
            InitializeComponent();
        }

        private void Save_Entrie(object sender, RoutedEventArgs e)
        {
            _onChangeCallback.OnChange((CalendarEntrie)DataContext);
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
