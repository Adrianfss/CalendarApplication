using CalendarApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApplication.DAL.Repositorys
{
    /// <summary>
    /// Basic Repository for CalendarDatabase
    /// </summary>
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
            return (await _dbContext.calendarEntries.ToListAsync()).OrderBy(x => x.StartTime);
        }
        public IEnumerable<CalendarEntrie> GetCalendarEntries()
        {
            return _dbContext.calendarEntries.ToList().OrderBy(x => x.StartTime);
        }

        public async Task<CalendarEntrie> AddEntrieAsync(CalendarEntrie entrie)
        {
            var entityEntry = await _dbContext.calendarEntries.AddAsync(entrie);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task RemoveEntrieAsync(CalendarEntrie entrie)
        {
            _dbContext.Remove(entrie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CalendarEntrie> UpdateEntrieAsync(CalendarEntrie entrie)
        {
            var entityEntry = _dbContext.Update(entrie);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}