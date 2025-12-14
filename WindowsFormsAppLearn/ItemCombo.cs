using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppLearn
{
    class ItemCombo
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public string UnitPrice { get; set; }
        public override string ToString() => $"{Code} - {Name}"; // Ensures only the name is shown

    }
}
