﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VWorkFlow
    {
        public int WfrId { get; set; }
        public int RouteId { get; set; }
        public int Sequence { get; set; }
        public string RouteName { get; set; }
        public int isActive { get; set; }

    }
}
