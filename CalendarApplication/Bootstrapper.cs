using CalendarApplication.ViewModels;
using Caliburn.Micro;
using System.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CalendarApplication.DAL;
using CalendarApplication.DAL.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace CalendarApplication
{
    class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }
        protected override void Configure()
        {
            string connectionString = "Server = (localdb)\\mssqllocaldb; Database = CalendarDB; Trusted_Connection = True; MultipleActiveResultSets = true";
            var optionBuilder = new DbContextOptionsBuilder<CalendarContext>();
            optionBuilder.UseSqlServer(connectionString);

            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .RegisterInstance(typeof(DbContextOptions<CalendarContext>),
                typeof(DbContextOptions<CalendarContext>).ToString(),
                optionBuilder.Options);

            /* GetType().Assembly.GetTypes()
                 .Where(type => type.IsClass)
                 .Where(type => type.Name.EndsWith("ViewModel"))
                 .ToList()
                 .ForEach(viewModelType => _container.RegisterPerRequest(
                     viewModelType, viewModelType.ToString(), viewModelType));
                     */
            _container
                .RegisterPerRequest(typeof(ShellViewModel), typeof(ShellViewModel).ToString(), typeof(ShellViewModel));
            
            _container.PerRequest<CalendarContext, CalendarContext>();
            _container.PerRequest<ICalendarRepository, CalendarRepository>();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
