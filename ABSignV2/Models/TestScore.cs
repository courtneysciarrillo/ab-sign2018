using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABSignV2
{
    public partial class TestScore
    {
        public static int? GetPercent(int? score)
        {
            int? percentage = score * 10;
            return percentage;
        }
    }
}