using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest
{
    public enum TransactionTypes { withdraw = 1, deposit = 2, transfer = 3 }
    public enum Currency
    {
        [Description("Canadian dollars")]
        CAD = 1,
        [Description("US dollars")]
        USD = 2,
        [Description("Mexican Pesos")]
        MXN = 3
    }

}
