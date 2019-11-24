using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public static class GuidGenerator
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString().Replace("-","").Substring(0,6);
        }
    }
}
