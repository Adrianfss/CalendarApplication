using CalendarApplication.DAL;
using CalendarApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplicationTest
{
    static class HelperClass
    {
        /// <summary>
        ///  Helper method for creating inMemoryDb
        /// </summary>
        /// <param name="items">database items </param>
        /// <param name="dbName"> if not provided it will generate a ranom database-name </param>
        public static CalendarContext CreateInMemoryDB(List<CalendarEntrie> items, string dbName = null)
        {
            dbName = (dbName != null ? dbName : Guid.NewGuid().ToString());

            var options = new DbContextOptionsBuilder<CalendarContext>()
               .UseInMemoryDatabase(databaseName: dbName)
               .Options;

            var context = new CalendarContext(options);
            items.ForEach(n => context.calendarEntries.Add(n));
            context.SaveChanges();
            return context;
        }
        public static List<CalendarEntrie> CreateItems()
        {
            return new List<CalendarEntrie>
            {
                new CalendarEntrie{Id = new Guid(),Title ="Title1",Description = "Description1"},
                new CalendarEntrie{Id = new Guid(),Title ="Title2",Description = "Description2"},
                new CalendarEntrie{Id = new Guid(),Title ="Title3",Description = "Description3"}
            };
        }
    }
}
