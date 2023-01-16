using System;
using System.Collections.Generic;
using System.Text;

namespace Ireland2022
{
    public class CarbonIntensity
    {
        public DataEntry[] data { get; set; }
    }

    public class DataEntry
    {
        public string from { get; set; }
        public string to { get; set; }

        public Intensity intensity { get; set; }
    }

    public class Intensity
    {
        public int forecast { get; set; }
        public int actual { get; set; }
        public string index { get; set; }
    }
}
