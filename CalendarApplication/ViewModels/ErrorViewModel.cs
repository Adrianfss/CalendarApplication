using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplication.ViewModels
{ 
    /// <summary>
    /// Simple ErrorView
    /// </summary>
    class ErrorViewModel : Screen
    {
        public string Message { get; set; }
        public ErrorViewModel(string msg)
        {
            Message = msg;
        }
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
