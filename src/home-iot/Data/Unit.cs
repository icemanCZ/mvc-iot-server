using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public enum Unit : byte
    {
        DegreeCelsius = 0,
        Unknown = byte.MaxValue,
    }

    public static class UnitExtensions
    {
        public static string GetText(this Unit t)
        {
            switch (t)
            {
                case Unit.DegreeCelsius:
                    return "°C";
                case Unit.Unknown:
                    return "";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
