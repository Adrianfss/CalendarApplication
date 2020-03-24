using CalendarApplication.CallbackInterface;
using CalendarApplication.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplication.ViewModels
{
    /// <summary>
    /// window for Updating new CalenderEntries
    /// uses OnUpdateCallBack for callback to mainWindow
    /// </summary>
    public class EditCalendarViewModel : Screen
    {
        private readonly IOnChangeCallback _onChangeCallback;
        private CalendarEntrie _selectedEntrie;
        private readonly IWindowManager _windowManager;
        public EditCalendarViewModel(CalendarEntrie entrie,IWindowManager windowManager, IOnChangeCallback callback)
        {
            _windowManager = windowManager;
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
            if (String.IsNullOrEmpty(SelectedEntrie.Description) || String.IsNullOrEmpty(SelectedEntrie.Title))
            {
                _windowManager.ShowWindow(new ErrorViewModel("Description or Title is Empty"), null, null);
                return;
            }
            _onChangeCallback.OnChangeAsync(_selectedEntrie);
            CloseWindow();
        }
        public void CloseWindow()
        {
            TryClose();
        }

    }
}
