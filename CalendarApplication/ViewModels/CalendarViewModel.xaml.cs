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
            selected = AllCalendarEntries.FirstOrDefault();
            FillInfoField(selected);
        }

        private void CalenderListClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                selected = (CalendarEntrie)item.Content;
                FillInfoField(selected);
            }
        }

        private void Add_Entrie(object sender, RoutedEventArgs e)
        {
            CreateCalendarViewModel createCalendarViewModel = new CreateCalendarViewModel(this);
            createCalendarViewModel.Show();
        }
        private void Edit_Entrie(object sender, RoutedEventArgs e)
        {
            if(!ValidateSelected())
            {
                return;
            }
            EditCalendarViewModel createCalendarViewModel = new EditCalendarViewModel(selected,this);
            createCalendarViewModel.Show();
        }



        private void Calendar_SelectedDatesChanged(object sender,
        SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                DateTime date = calendar.SelectedDate.Value;
                var list = AllCalendarEntries.Where(i => i.StartTime >= date && i.EndTime.Date <= date.AddDays(1)).ToList();
                calendarEntriesList = new ObservableCollection<CalendarEntrie>(list);
                lvCalendarList.ItemsSource = calendarEntriesList;
            }
        }
        private async void Delete_Entrie(object sender, RoutedEventArgs e)
        {
            if (!ValidateSelected())
            {
                return;
            }
            await _calendarRepository.RemoveEntrieAsync(selected);
            AllCalendarEntries.Remove(selected);
            selected = AllCalendarEntries.FirstOrDefault();
            FillInfoField(selected);
            UpdateEntrieList();
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            DateTime? from = dpFrom.SelectedDate;
            DateTime? to = dpTo.SelectedDate;

            if(from == null || to == null)
            {
                new ErrorView("Date is null").Show();
                return;
            }
            var list = AllCalendarEntries.Where(i => i.StartTime >= from && i.EndTime <= to);
            calendarEntriesList = new ObservableCollection<CalendarEntrie>(list);
            lvCalendarList.ItemsSource = calendarEntriesList;
        }

        public async Task OnChange(CalendarEntrie calendarEntrie)
        {
            var returnValue = await _calendarRepository.UpdateEntrieAsync(calendarEntrie);
            var oldValue = AllCalendarEntries.FirstOrDefault(x => x.Id == calendarEntrie.Id);
            if (oldValue != null) oldValue = returnValue;

            UpdateEntrieList();
        }
        public async Task OnCreate(CalendarEntrie calendarEntrie)
        {
            var returnValue = await _calendarRepository.AddEntrieAsync(calendarEntrie);
            AllCalendarEntries.Add(returnValue);
            UpdateEntrieList();
        }

        private void UpdateEntrieList()
        {
            calendarEntriesList = new ObservableCollection<CalendarEntrie>(AllCalendarEntries);
            lvCalendarList.ItemsSource = calendarEntriesList;
        }
        private void FillInfoField(CalendarEntrie entrie)
        {
            if (entrie == null) return;

            lblTitle.Content = entrie.Title;
            tbDescription.Text = entrie.Description;
            lblStartTime.Content = entrie.StartTime.ToLongDateString();
            lblEndTime.Content = entrie.EndTime.ToLongDateString();
        }

        private async void Refresh(object sender, RoutedEventArgs e)
        {
            AllCalendarEntries = (await _calendarRepository.GetCalendarEntriesAsync()).ToList();
            UpdateEntrieList();
        }
        private bool ValidateSelected()
        {
            if(selected == null)
            {
                new ErrorView("Nothing is Selected").Show();
                return false;
            }

           return true;
        }
    }
}