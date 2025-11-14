using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1_Malckov
{
    public class DrugRecord
    {
        public int Year { get; set; }
        public double Estimate { get; set; }
        public string Sex { get; set; } = "";
        public string RaceGroup { get; set; } = "";
        public string StubLabel { get; set; } = "";

        public double[] Features => new double[]
        {
            Year,
            Estimate,
            Sex == "Male" ? 1.0 : 0.0
        };

        public string Label => RaceGroup;
    }
}
