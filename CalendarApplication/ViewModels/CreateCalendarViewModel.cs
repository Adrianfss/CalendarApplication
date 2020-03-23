using CalendarApplication.CallbackInterface;
using CalendarApplication.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplication.ViewModels
{
    class CreateCalendarViewModel : Screen
    {
        IOnCreateCallback _onCreateCallBack;
        private CalendarEntrie _selectedEntrie;
        public CreateCalendarViewModel(IOnCreateCallback onCreateCallback) 
        {
            _onCreateCallBack = onCreateCallback;
            SelectedEntrie = new CalendarEntrie { StartTime = DateTime.Now, EndTime = DateTime.Now};
        }
        public CalendarEntrie SelectedEntrie
        {
            get { return _selectedEntrie; }
            set
            {
                _selectedEntrie = value;
                NotifyOfPropertyChange(() => SelectedEntrie);
            }
        }
        public bool CanSave() => SelectedEntrie.EndTime != null && SelectedEntrie.StartTime != null;  

        public void Save()
        {
            _onCreateCallBack.OnCreate(SelectedEntrie);
            CloseWindow();
        }
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
