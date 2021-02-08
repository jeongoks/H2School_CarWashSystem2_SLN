using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H1School_CarWashSystem1
{
    public class WashingHall
    {
        public List<Vehicle> Vehicle { get; set; }

        public List<WashingType> WashType { get; set; }

        public WashingHall(List<WashingType> types)
        {
            WashType = new List<WashingType>();
            WashType = types;
        }
    }
}