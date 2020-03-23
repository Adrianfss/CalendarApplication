using CalendarApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApplication.DAL
{
    public class CalendarContext :DbContext
    {
        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
        {
        }
        public DbSet<CalendarEntrie> calendarEntries { get; set; }
    }
}
