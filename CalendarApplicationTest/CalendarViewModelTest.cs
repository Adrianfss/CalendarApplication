using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using CalendarApplication.Models;
using CalendarApplication.ViewModels;
using CalendarApplication.DAL.Repositorys;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CalendarApplicationTest
{
    public class CalendarViewModelTest
    {
        [Fact]
        public void Selected_Item_Is_First_Item()
        {
            var dbContext = HelperClass.CreateInMemoryDB(HelperClass.CreateItems());
            CalendarRepository calendarRepository = new CalendarRepository(dbContext);
            CalendarViewModel viewModel = new CalendarViewModel(calendarRepository,null);


            var item = viewModel.SelectedEntrie;
            Assert.Equal("Title1", item.Title);
        }
        [Fact]
        public async Task Update_List_On_OnCreateAsync()
        {
            var dbContext = HelperClass.CreateInMemoryDB(HelperClass.CreateItems());
            CalendarRepository calendarRepository = new CalendarRepository(dbContext);
            CalendarViewModel viewModel = new CalendarViewModel(calendarRepository, null);


            var items = viewModel.CalendarEntries;
            Assert.Equal(3, items.Count);

            await viewModel.OnCreateAsync(new CalendarEntrie { Id = new Guid(), Title = "Title4", Description = "Description4" });
            items = viewModel.CalendarEntries;
            Assert.Equal(4, items.Count);
        }
        [Fact]
        public async Task Update_List_On_OnUpdateAsync()
        {
            var dbContext = HelperClass.CreateInMemoryDB(HelperClass.CreateItems());
            CalendarRepository calendarRepository = new CalendarRepository(dbContext);
            CalendarViewModel viewModel = new CalendarViewModel(calendarRepository, null);

            var items = viewModel.SelectedEntrie;
            items.Description = "Changed";

            await viewModel.OnChangeAsync(items);
            var entries = viewModel.CalendarEntries;
            Assert.NotNull(entries.FirstOrDefault(x => x.Description.Equals("Changed")));
        }
        [Fact]
        public async Task Update_List_On_OnDeleteAsync()
        {
            var dbContext = HelperClass.CreateInMemoryDB(HelperClass.CreateItems());
            CalendarRepository calendarRepository = new CalendarRepository(dbContext);
            CalendarViewModel viewModel = new CalendarViewModel(calendarRepository, null);


            var items = viewModel.SelectedEntrie;
            Assert.Equal(3, viewModel.CalendarEntries.Count);
            await viewModel.DeleteEntrieAsync();
            Assert.Equal(2, viewModel.CalendarEntries.Count);
        }
    }

}
