using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H1School_CarWashSystem1
{
    public class WashingHall
    {
        public int WashCount { get; set; }
        public bool VehicleInHall { get; set; }
        public List<WashingType> WashingTypes { get; set; }
        public int Id { get; set; }
        public bool Cycle { get; set; }

        public WashingHall(int id)
        {
            this.Id = id;
            WashingTypes = new List<WashingType>();
            VehicleInHall = false;
            WashCount = 0;
        }

        public bool CheckIfVehicleInHall()
        {
            return VehicleInHall;
        }
    }
}