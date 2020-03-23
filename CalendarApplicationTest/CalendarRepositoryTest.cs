using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using CalendarApplication.DAL;
using CalendarApplication.DAL.Repositorys;
using CalendarApplication.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CalendarApplicationTest
{
    public class CalendarRepositoryTest
    {
      
        public CalendarRepositoryTest()
        {
           
           
        }
        [Fact]
        public async Task AddEntrieToDb()
        {

            using (var context = CreateInMemoryDB(CreateItems()))
            {
                var calendarRepository = new CalendarRepository(context);
                Assert.Equal(3, (await calendarRepository.GetCalendarEntriesAsync()).Count());
            }

        }
        [Fact]
        public async Task RemoveEntrieFromDb()
        {

            using (var context = CreateInMemoryDB(CreateItems()))
            {
                var calendarRepository = new CalendarRepository(context);
                var entries = await calendarRepository.GetCalendarEntriesAsync();
                var entrie = entries.FirstOrDefault();
                Assert.Equal(3, (await calendarRepository.GetCalendarEntriesAsync()).Count());
                await calendarRepository.RemoveEntrieAsync(entrie);
                Assert.Equal(2, (await calendarRepository.GetCalendarEntriesAsync()).Count());
            }

        }
        [Fact]
        public async Task UpdateEntrieFromDb()
        {

            using (var context = CreateInMemoryDB(CreateItems()))
            {
                var calendarRepository = new CalendarRepository(context);
                var entries = await calendarRepository.GetCalendarEntriesAsync();
                var entrie = entries.FirstOrDefault();

                entrie.Description = "newDescription";

                await calendarRepository.UpdateEntrieAsync(entrie);
                
                Assert.NotNull((await calendarRepository.GetCalendarEntriesAsync()).FirstOrDefault(x => x.Description.Equals("newDescription")));
            }

        }

        private CalendarContext CreateInMemoryDB(List<CalendarEntrie> items,string dbName = "testDB") 
        {
            var options = new DbContextOptionsBuilder<CalendarContext>()
               .UseInMemoryDatabase(databaseName: dbName)
               .Options;

            var context = new CalendarContext(options);
            items.ForEach(n => context.calendarEntries.Add(n));
            context.SaveChanges();
            return context;
        }
        private List<CalendarEntrie> CreateItems()
        {
            return new List<CalendarEntrie>
            {
                new CalendarEntrie{Id = new Guid()},
                new CalendarEntrie{Id = new Guid()},
                new CalendarEntrie{Id = new Guid()},
            };
        }


    }
}
