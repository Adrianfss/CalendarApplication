using CalendarApplication.DAL;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CalendarApplication.Models;
using System.Globalization;

namespace CalendarApplication.HelperClasses
{
    /// <summary>
    /// Initialize if not populated
    /// </summary>
    public class Seed
    {
        private readonly CalendarContext _calendarContext;

        public Seed(CalendarContext context)
        {
            _calendarContext = context;
        }

        public void InitializeDB()
        {
            _calendarContext.Database.EnsureCreated();

            if (_calendarContext.calendarEntries.Any()) return;


            _calendarContext.AddRange(CreateList());
            _calendarContext.SaveChanges();
        }

        private List<CalendarEntrie> CreateList()
        {
            var list = new List<CalendarEntrie>();

            list.Add(new CalendarEntrie { Title = "JUL!", Description = "Lang juleferie med masse mat og gaver", StartTime = CreateDateFromString("101220201000"), EndTime = CreateDateFromString("010120211000") });
            list.Add(new CalendarEntrie { Title = "Møte", Description = "Møte med avdelingsleder", StartTime = CreateDateFromString("260320201000"), EndTime = CreateDateFromString("260320201200") });
            list.Add(new CalendarEntrie { Title = "Dips Developer Days", Description = "Avlyst", StartTime = CreateDateFromString("050420200800"), EndTime = CreateDateFromString("080420201600") });
            list.Add(new CalendarEntrie { Title = "Helg", Description = "lang helg med masse folk", StartTime = CreateDateFromString("270320201600"), EndTime = CreateDateFromString("300320201600") });
            list.Add(new CalendarEntrie { Title = "Trening", Description = "Skal trene ben idag", StartTime = CreateDateFromString("250320201800"), EndTime = CreateDateFromString("250320201930") });
            list.Add(new CalendarEntrie { Title = "slappe av", Description = "", StartTime = CreateDateFromString("010620201000"), EndTime = CreateDateFromString("150620201000") });

            return list;
           
        }
        private DateTime CreateDateFromString(string s)
        {
            return DateTime.ParseExact(s,
                                  "ddMMyyyyHHmm",
                                   CultureInfo.InvariantCulture);
        } 
    }
}
