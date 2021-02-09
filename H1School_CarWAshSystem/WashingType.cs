using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1School_CarWashSystem1
{
    public class WashingType
    {
        public enum WashType
        {
            Gold,
            Silver,
            Standard
        }

        public enum Price
        {
            GoldPrice = 150,
            SilverPrice = 100,
            StandardPrice = 50
        }

        public List<Processes> Processes { get; set; }
        public List<WashingHall> WashingHalls { get; set; }
        public WashType Types { get; set; }
        public Price Prices { get; set; }
        public int Id { get; set; }

        public WashingType()
        {
            WashingHalls = new List<WashingHall>();
            Processes = new List<Processes>();
        }
    }
}