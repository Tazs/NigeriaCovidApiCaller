using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace covid.apicall
{
    public class Info
    {
        public string totalSamplesTested { get; set; }
        public int totalConfirmedCases { get; set; }
        public int totalActiveCases { get; set; }
        public int discharged { get; set; }
        public int death { get; set; }
        public List<subInfo> states { get; set; }
    }

    public class subInfo
    {
        public string state { get; set; }
        public string _id { get; set; }
        public int confirmedCases { get; set; }
        public int casesOnAdmission { get; set; }
        public int discharged { get; set; }
        public int death { get; set; }
    }
}
