using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Core
{
    public static class GuidGenerator
    {
        public static string NewGuid(int lettersCount)
        {
            return Guid.NewGuid().ToString().Replace("-","").Substring(lettersCount);
        }
    }
}
