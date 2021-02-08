using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H1School_CarWashSystem1
{
    public class WashingType
    {
        public List<Processes> Process { get; set; }

        public WashingType(Processes process)
        {
            Process = new List<Processes>();
        }
    }
}