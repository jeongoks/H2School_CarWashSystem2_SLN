using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H1School_CarWashSystem1
{
    public class Processes
    {
        public List<WashingType> WashTypes { get; set; }
        public bool Rinsing { get; set; }
        public bool Washing { get; set; }
        public bool Waxing { get; set; }
        public bool UndercarriageRinse { get; set; }
        public bool Drying { get; set; }

        public Processes()
        {
            WashTypes = new List<WashingType>();
        }
    }
}