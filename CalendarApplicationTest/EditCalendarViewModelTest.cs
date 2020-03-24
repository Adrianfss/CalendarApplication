using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CalendarApplication.ViewModels;
using CalendarApplication.CallbackInterface;
using CalendarApplication.Models;
using System.Threading.Tasks;

namespace CalendarApplicationTest
{
    public class EditCalendarViewModelTest
    {
        [Fact]
        public void Entrie_Is_Saved_when_Created()
        {
            Mock<IOnChangeCallback> mock = new Mock<IOnChangeCallback>();
            mock.Setup(x => x.OnChangeAsync(It.IsAny<CalendarEntrie>()))
                .Returns(Task.FromResult(new CalendarEntrie()));

            var entrie = new CalendarEntrie { Title = "Title", Description = "Description"};

            EditCalendarViewModel viewModel = new EditCalendarViewModel(entrie,null,mock.Object);

            viewModel.Save();
        }
    }
}
