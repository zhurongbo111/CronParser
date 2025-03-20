using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronParser
{
    public class CronFormatException : FormatException
    {
        public CronFormatException(string message) : base(message)
        {
            
        }
    }
}
