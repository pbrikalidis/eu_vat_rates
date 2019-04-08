using System;
using System.Collections.Generic;

namespace EU_VAT_Rates
{
    public class Rates
    {
        public double super_reduced { get; set; }
        public double reduced { get; set; }
        public double standard { get; set; }
        public double? reduced1 { get; set; }
        public double? reduced2 { get; set; }
        public double? parking { get; set; }
    }

    public class Period
    {
        public string effective_from { get; set; }
        public Rates rates { get; set; }
    }

    public class Rate
    {
        public string name { get; set; }
        public string code { get; set; }
        public string country_code { get; set; }
        public List<Period> periods { get; set; }
    }

    public class RootObject
    {
        public string details { get; set; }
        public object version { get; set; }
        public List<Rate> rates { get; set; }
    }

  
}