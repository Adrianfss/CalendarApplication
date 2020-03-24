using CalendarApplication.ViewModels;
using Caliburn.Micro;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using CalendarApplication.DAL;
using CalendarApplication.DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using CalendarApplication.HelperClasses;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CalendarApplication
{
    /// <summary>
    /// using Caliburn.Micro for MVVM structural design pattern
    /// and SimpleContainer for dependency injection
    /// </summary>
    class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Configure() seting up Singletons and Instance
        /// 
        /// Server connectionString can be found in appsettings.json
        /// </summary>
        protected override void Configure()
        {
            string connectionString = JObject.Parse(File.ReadAllText(@"appsettings.json"))["ConnectionStrings"]["DefaultConnection"].ToString();
            var optionBuilder = new DbContextOptionsBuilder<CalendarContext>();
            optionBuilder.UseSqlServer(connectionString);

            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .RegisterInstance(typeof(DbContextOptions<CalendarContext>),
                typeof(DbContextOptions<CalendarContext>).ToString(),
                optionBuilder.Options);

             GetType().Assembly.GetTypes()
                 .Where(type => type.IsClass)
                 .Where(type => type.Name.EndsWith("ViewModel"))
                 .ToList()
                 .ForEach(viewModelType => _container.RegisterPerRequest(
                     viewModelType, viewModelType.ToString(), viewModelType));
                     
 
            _container.PerRequest<CalendarContext, CalendarContext>();
            _container.PerRequest<ICalendarRepository, CalendarRepository>();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            InitializeSeed();
            DisplayRootViewFor<CalendarViewModel>();
        }

        /// <summary>
        /// Run Seed if database is empty
        /// </summary>
        private void InitializeSeed()
        {
            var option = (DbContextOptions<CalendarContext>)_container.GetInstance(typeof(DbContextOptions<CalendarContext>), typeof(DbContextOptions<CalendarContext>).ToString());
            var calendarContext = new CalendarContext(option);
            var seed = new Seed(calendarContext);
            seed.InitializeDB();
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
