using CalendarApplication.CallbackInterface;
using CalendarApplication.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// window for creating new CalenderEntries
    /// uses OnCreatedCallBack for callback to mainWindow
    /// </summary>
     public class CreateCalendarViewModel : Screen
    {
        IOnCreateCallback _onCreateCallBack;
        IWindowManager _windowManager;

        private CalendarEntrie _selectedEntrie;
        public CreateCalendarViewModel(IWindowManager windowManager,IOnCreateCallback onCreateCallback) 
        {
            _onCreateCallBack = onCreateCallback;
            _windowManager = windowManager;
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
        public void Save()
        {
            if(String.IsNullOrEmpty(SelectedEntrie.Description) || String.IsNullOrEmpty(SelectedEntrie.Title))
            {
                _windowManager.ShowWindow(new ErrorViewModel("Description or Title is Empty"), null, null);
                return;
            }
            _onCreateCallBack.OnCreateAsync(SelectedEntrie);
            CloseWindow();
        }
        public void CloseWindow()
        {
            TryClose();
        }
    }
}
