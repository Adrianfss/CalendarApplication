using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using CalendarApplication.CallbackInterface;
using CalendarApplication.Models;
using System.Threading.Tasks;
using CalendarApplication.ViewModels;

namespace CalendarApplicationTest
{
    public class CreateCalendarViewModelTest
    {
        [Fact]
        public void Entrie_Is_Saved_when_Created()
        {
            Mock<IOnCreateCallback> mock = new Mock<IOnCreateCallback>();
            mock.Setup(x => x.OnCreateAsync(It.IsAny<CalendarEntrie>()))
                .Returns(Task.FromResult(new CalendarEntrie()));

            CreateCalendarViewModel viewModel = new CreateCalendarViewModel(null,mock.Object);

            viewModel.SelectedEntrie = new CalendarEntrie { Title = "new Title", Description = "new Description"};

            viewModel.Save();
        }
    }
}
