using CalendarApplication.DAL.Repositorys;
using CalendarApplication.Models;
using CalendarApplication.CallbackInterface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using System;
using CalendarApplication.Views;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// Interaction logic for CalendarViewModel.xaml
    /// </summary>
    public partial class CalendarViewModel : Window, IOnChangeCallback, IOnCreateCallback
    {
        private CalendarEntrie selected;
        private readonly ICalendarRepository _calendarRepository;
        private ObservableCollection<CalendarEntrie> calendarEntriesList;
        private List<CalendarEntrie> AllCalendarEntries;

        public CalendarViewModel(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
            AllCalendarEntries = _calendarRepository.GetCalendarEntries().ToList();
            InitializeComponent();
            UpdateEntrieList();
            lvCalendarList.ItemsSource = calendarEntriesList;
        }

        private void CalenderListClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                selected = (CalendarEntrie)item.Content;
                FellInfoText(selected);
            }
        }

        private void Add_Entrie(object sender, RoutedEventArgs e)
        {
            CreateCalendarViewModel createCalendarViewModel = new CreateCalendarViewModel(this);
            createCalendarViewModel.Show();
        }
        private void Edit_Entrie(object sender, RoutedEventArgs e)
        {
            EditCalendarViewModel createCalendarViewModel = new EditCalendarViewModel(selected,this);
            createCalendarViewModel.Show();
        }
        private void Delete_Entrie(object sender, RoutedEventArgs e)
        {
            if(selected == null)
            {
                ErrorView ev = new ErrorView("Nothing is Selected");
                ev.Show();
                return;
            }
            _calendarRepository.RemoveEntrieAsync(selected);
        }

        public async Task OnChange(CalendarEntrie calendarEntrie)
        {
            var returnValue = await _calendarRepository.UpdateEntrieAsync(calendarEntrie);
           // AllCalendarEntries.up = returnValue;
        }

        public async Task OnCreate(CalendarEntrie calendarEntrie)
        {
            var returnValue = await _calendarRepository.AddEntrieAsync(calendarEntrie);
            AllCalendarEntries.Add(returnValue);
            calendarEntriesList.Add(returnValue);
        }

        private void UpdateEntrieList()
        {
            calendarEntriesList = new ObservableCollection<CalendarEntrie>(AllCalendarEntries);
        }
        private void FellInfoText(CalendarEntrie entrie)
        {

            lblTitle.Content = entrie.Title;
            tbDescription.Text = entrie.Description;
            lblStartTime.Content = entrie.StartTime.ToLongDateString();
            lblEndTime.Content = entrie.EndTime.ToLongDateString();
        }
    }
}