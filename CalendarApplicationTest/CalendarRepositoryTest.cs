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
        public async Task Add_Entrie_ToDb()
        {

            using (var context = HelperClass.CreateInMemoryDB(HelperClass.CreateItems(), dbName: "AddDb"))
            {
                var calendarRepository = new CalendarRepository(context);
                Assert.Equal(3, (await calendarRepository.GetCalendarEntriesAsync()).Count());
            }

        }
        [Fact]
        public async Task Remove_EntrieFrom_Db()
        {
            using (var context = HelperClass.CreateInMemoryDB(HelperClass.CreateItems(), dbName: "RemoveDb"))
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
        public async Task Update_Entrie_FromDb()
        {

            using (var context = HelperClass.CreateInMemoryDB(HelperClass.CreateItems(),dbName : "UpdateDB"))
            {
                var calendarRepository = new CalendarRepository(context);
                var entries = await calendarRepository.GetCalendarEntriesAsync();
                var entrie = entries.FirstOrDefault();

                entrie.Description = "newDescription";

                await calendarRepository.UpdateEntrieAsync(entrie);
                
                Assert.NotNull((await calendarRepository.GetCalendarEntriesAsync()).FirstOrDefault(x => x.Description.Equals("newDescription")));
            }

        }
    }
}
