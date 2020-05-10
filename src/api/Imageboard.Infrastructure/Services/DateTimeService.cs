using Imageboard.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;

        public DateTime Today => DateTime.UtcNow.Date;
    }
}
