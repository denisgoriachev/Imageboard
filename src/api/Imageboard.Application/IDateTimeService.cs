using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Application
{
    public interface IDateTimeService
    {
        DateTime Now { get; }

        DateTime Today { get; }
    }
}
