using CalendarApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.DAL.Repositorys
{
    public interface ICalendarRepository
    {
        Task<IEnumerable<CalendarEntrie>> GetCalendarEntriesAsync();
        IEnumerable<CalendarEntrie> GetCalendarEntries();
        Task AddEntrieAsync(CalendarEntrie entrie);
        Task RemoveEntrieAsync(CalendarEntrie entrie);
        Task UpdateEntrie(CalendarEntrie entrie);
    }
}
