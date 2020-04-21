using System.ComponentModel;
using TreatsParser.Core.Helpers;

namespace ThreatsParser.Entities.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum RiskProbabilities
    {
        [Description("Маловероятно")]
        Unlikely = 0,

        [Description("Низкая")]
        Low = 2,

        [Description("Средняя")]
        Average = 5,

        [Description("Высокая")]
        High = 10,
    }
}