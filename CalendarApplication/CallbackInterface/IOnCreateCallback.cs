using CalendarApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CallbackInterface
{
    public interface IOnCreateCallback
    {
        Task OnCreateAsync(CalendarEntrie calendarEntrie);
    }
}
