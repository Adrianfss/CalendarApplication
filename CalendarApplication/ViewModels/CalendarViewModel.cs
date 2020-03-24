using CalendarApplication.CallbackInterface;
using CalendarApplication.DAL.Repositorys;
using CalendarApplication.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// Main Window for this application IwindowManager and ICalendarRepository 
    /// is Injected in the bootstrap class
    /// </summary>
    public class CalendarViewModel : Screen, IOnChangeCallback, IOnCreateCallback
    {
        private readonly IWindowManager _windowManager;
        private List<CalendarEntrie> _fullEntrieList;
        private readonly ICalendarRepository _calendarRepository;
        private BindableCollection<CalendarEntrie> _calendarEntries = new BindableCollection<CalendarEntrie>();
        private CalendarEntrie _selectedEntrie;
        private DateTime _startTime = DateTime.Now;
        private DateTime _endTime = DateTime.Now;

        public CalendarViewModel(ICalendarRepository calendarRepository, IWindowManager windowManager)
        {
            _calendarRepository = calendarRepository;
            _windowManager = windowManager;
            _fullEntrieList = _calendarRepository.GetCalendarEntries().ToList();
            CalendarEntries.AddRange(_fullEntrieList);
            SelectedEntrie = _calendarEntries.FirstOrDefault();
        }

        public BindableCollection<CalendarEntrie> CalendarEntries
        {
            get { return _calendarEntries; }
            set { _calendarEntries = value; }
        }

        public CalendarEntrie SelectedEntrie
        {
            get { return _selectedEntrie; }
            set
            {
                _selectedEntrie = value;
                NotifyOfPropertyChange(() => SelectedEntrie);
            }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
            }
        }
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
            }
        }
        /// <summary>
        /// uses calendarRepository for getting the newest list from server
        /// </summary>
        public async void ShowAll()
        {
            CalendarEntries.Clear();
            _fullEntrieList = (await _calendarRepository.GetCalendarEntriesAsync()).ToList();
            CalendarEntries.AddRange(_fullEntrieList);
            NotifyOfPropertyChange(() => CalendarEntries);
        }
        /// <summary>
        /// Search for CalendarEntrie within StartTime and EndTime
        /// </summary>
        public void Search()
        {
            var list = _fullEntrieList.Where(i => i.StartTime >= StartTime && i.EndTime <= EndTime.AddDays(1));
            CalendarEntries.Clear();
            CalendarEntries.AddRange(list);
        }
        public void EditEntrie()
        {
            if (SelectedEntrie == null) return;

            _windowManager.ShowWindow(new EditCalendarViewModel (entrie: SelectedEntrie,windowManager:_windowManager, callback:this), null, null);
        }

        public async Task DeleteEntrieAsync()
        {
            if (SelectedEntrie == null) return;

            await _calendarRepository.RemoveEntrieAsync(SelectedEntrie);
            CalendarEntries.Remove(SelectedEntrie);
            SelectedEntrie = CalendarEntries.FirstOrDefault();
        }

        public void AddEntrie()
        {
            _windowManager.ShowWindow(new CreateCalendarViewModel(windowManager: _windowManager,onCreateCallback :this), null, null);
        }
        /// <summary>
        /// implementing of callbackInterfaces
        /// </summary>
        public async Task OnCreateAsync(CalendarEntrie calendarEntrie)
        {
            var item = await _calendarRepository.AddEntrieAsync(calendarEntrie);
            CalendarEntries.Add(item);
        }

        public async Task OnChangeAsync(CalendarEntrie calendarEntrie)
        {
            var item = await _calendarRepository.UpdateEntrieAsync(calendarEntrie);

            var oldValue = CalendarEntries.FirstOrDefault(x => x.Id == calendarEntrie.Id);
            if (oldValue != null) oldValue = item;

            NotifyOfPropertyChange(() => CalendarEntries);
        }
    }
}