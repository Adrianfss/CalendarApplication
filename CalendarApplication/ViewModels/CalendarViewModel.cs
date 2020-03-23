using CalendarApplication.DAL.Repositorys;
using CalendarApplication.Models;
using CalendarApplication.Views;
using System.Linq;
using Caliburn.Micro;
using CalendarApplication.CallbackInterface;
using System.Threading.Tasks;

namespace CalendarApplication.ViewModels
{
    internal class CalendarViewModel :Screen, IOnChangeCallback, IOnCreateCallback
    {
        private readonly IWindowManager _windowManager;
        private readonly ICalendarRepository _calendarRepository;
        private BindableCollection<CalendarEntrie> _calendarEntries = new BindableCollection<CalendarEntrie>();
        private CalendarEntrie _selectedEntrie;

        public CalendarViewModel(ICalendarRepository calendarRepository, IWindowManager windowManager)
        {
            _calendarRepository = calendarRepository;
            _windowManager = windowManager;
            CalendarEntries.AddRange(_calendarRepository.GetCalendarEntries());
            SelectedEntrie = _calendarEntries.FirstOrDefault();
        }


        public BindableCollection<CalendarEntrie> CalendarEntries
        {
            get {  return _calendarEntries;}
            set { _calendarEntries = value;}
        }
        public CalendarEntrie SelectedEntrie
        {
            get{ return _selectedEntrie; }
            set
            { 
                _selectedEntrie = value;
            }
        }
        public void ShowAll()
        {
            CalendarEntries.AddRange(_calendarRepository.GetCalendarEntries());
            NotifyOfPropertyChange(() => CalendarEntries);
        }
        public void Search()
        {

        }
        public bool CanEditEntrie(CalendarEntrie selectedEntrie) => SelectedEntrie != null;
        public void EditEntrie(CalendarEntrie selectedEntrie) 
        {
            _windowManager.ShowWindow(new EditCalendarViewModel(_selectedEntrie,this), null, null);
        }
        public bool CanDeleteEntrie(CalendarEntrie selectedEntrie) => SelectedEntrie != null;
     
        public void DeleteEntrie(CalendarEntrie selectedEntrie)
        {

        }
        public void AddEntrie()
        {
            _windowManager.ShowWindow(new CreateCalendarViewModel(this), null, null);   
        }

        public async Task OnCreate(CalendarEntrie calendarEntrie)
        {
            var item = await _calendarRepository.AddEntrieAsync(calendarEntrie);
            CalendarEntries.Add(item);
        }

        public async Task OnChange(CalendarEntrie calendarEntrie)
        {
           var item = await _calendarRepository.UpdateEntrieAsync(calendarEntrie);

           var oldValue = CalendarEntries.FirstOrDefault(x => x.Id == calendarEntrie.Id);
           if (oldValue != null) oldValue = item;
            
           NotifyOfPropertyChange(() => CalendarEntries);
        }
        public void Test()
        {

        }
    }
}