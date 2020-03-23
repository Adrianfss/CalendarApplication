using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using CalendarApplication.DAL.Repositorys;

namespace CalendarApplication.ViewModels
{
    class ShellViewModel : Screen
    {
        ICalendarRepository _calendarRepository;
        public ShellViewModel(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
            _calendarRepository.GetCalendarEntries();
        }
    }
}
