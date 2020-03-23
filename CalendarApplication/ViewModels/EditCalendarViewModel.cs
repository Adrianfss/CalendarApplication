using CalendarApplication.CallbackInterface;
using CalendarApplication.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplication.ViewModels
{
    class EditCalendarViewModel : Screen
    {
        private readonly IOnChangeCallback _onChangeCallback;
        private CalendarEntrie _selectedEntrie;
        public EditCalendarViewModel(CalendarEntrie entrie, IOnChangeCallback callback)
        {
            _onChangeCallback = callback;
            SelectedEntrie = entrie;
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
        public void Save()
        {
            _onChangeCallback.OnChange(_selectedEntrie);
            CloseWindow();
        }
        public void CloseWindow()
        {
            TryClose();
        }
        public void Test()
        {

        }
    }
}
