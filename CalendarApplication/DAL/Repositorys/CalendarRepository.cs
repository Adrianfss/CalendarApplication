using CalendarApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApplication.DAL.Repositorys
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly CalendarContext _dbContext;

        public CalendarRepository(CalendarContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CalendarEntrie>> GetCalendarEntriesAsync()
        {
            return await _dbContext.calendarEntries.ToListAsync();
        }
        public IEnumerable<CalendarEntrie> GetCalendarEntries()
        {
            return _dbContext.calendarEntries.ToList();
        }

        public async Task AddEntrieAsync(CalendarEntrie entrie)
        {
            await _dbContext.calendarEntries.AddAsync(entrie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveEntrieAsync(CalendarEntrie entrie)
        {
            _dbContext.Remove(entrie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEntrie(CalendarEntrie entrie)
        {
            _dbContext.Remove(entrie);
            await _dbContext.SaveChangesAsync();
        }
    }
}