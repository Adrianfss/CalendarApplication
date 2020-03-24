using CalendarApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CallbackInterface
{
    public interface IOnChangeCallback
    {
        Task OnChangeAsync(CalendarEntrie calendarEntrie);  
    }
}
