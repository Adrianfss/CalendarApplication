using CalendarApplication.DAL.Repositorys;
using CalendarApplication.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// Interaction logic for CalendarViewModel.xaml
    /// </summary>
    public partial class CalendarViewModel : Window
    {
        private CalendarEntrie selected;
        private readonly ICalendarRepository _calendarRepository;
        private ObservableCollection<CalendarEntrie> calendarEntries;
        private readonly List<CalendarEntrie> calendarEntriesList;

        public CalendarViewModel(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
            calendarEntriesList = _calendarRepository.GetCalendarEntries().ToList();
            InitializeComponent();
            calendarEntries = new ObservableCollection<CalendarEntrie>(calendarEntriesList);
            lvCalendarList.ItemsSource = calendarEntries;
        }

        private void CalenderListClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                selected = (CalendarEntrie)item.Content;

                lblTitle.Content = selected.Title;
                lblDescription.Content = selected.Description;
                lblStartTime.Content = selected.StartTime.ToLongDateString();
                lblEndTime.Content = selected.EndTime.ToLongTimeString();
            }
        }

        private void Add_Entrie(object sender, RoutedEventArgs e)
        {
            CreateCalendarViewModel createCalendarViewModel = new CreateCalendarViewModel();
            createCalendarViewModel.Show();
        }
    }
}