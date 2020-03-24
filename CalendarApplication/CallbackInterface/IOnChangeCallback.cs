using CalendarApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CallbackInterface
{
    /// <summary>
    /// CallbackInterface 
    /// Use: call back to main Window
    /// </summary>
    public interface IOnChangeCallback
    {
        Task OnChangeAsync(CalendarEntrie calendarEntrie);  
    }
}
