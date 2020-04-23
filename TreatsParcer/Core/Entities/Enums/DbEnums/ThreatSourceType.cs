using System.ComponentModel;
using TreatsParser.Core.Helpers;

namespace ThreatsParser.Core.Entities.Enums.DbEnums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ThreatSourceType
    {
        [Description("Внешний нарушитель с высоким потенциалом")]
        OutsideHigh = 0,

        [Description("Внешний нарушитель со средним потенциалом")]
        OutsideMedium = 1,

        [Description("Внешний нарушитель с низким потенциалом")]
        OutsideLow = 2,

        [Description("Внутренний нарушитель с высоким потенциалом")]
        InsideHigh = 3,

        [Description("Внутренний нарушитель со средним потенциалом")]
        InsideMedium = 4,

        [Description("Внутренний нарушитель с низким потенциалом")]
        InsideLow = 5,

        [Description("Отсутствует")]
        Empty = 6
    }
}