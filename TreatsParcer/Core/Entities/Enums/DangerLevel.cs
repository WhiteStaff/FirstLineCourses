using System.ComponentModel;
using TreatsParser.Core.Helpers;

namespace ThreatsParser.Entities.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum DangerLevel
    {
        [Description("Низкая")]
        Low = 0,

        [Description("Средняя")]
        Medium = 1,

        [Description("Высокая")]
        High = 2,

        [Description("Очень высокая")]
        VeryHigh = 3
    }
}