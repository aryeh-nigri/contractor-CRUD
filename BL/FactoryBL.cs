﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class FactoryBL
    {
        public static IBL getBL()
        {
            return BL_IMP.GetInstance();
        }
    }
}
