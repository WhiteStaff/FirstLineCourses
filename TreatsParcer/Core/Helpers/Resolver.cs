using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using ThreatsParser.Core.Entities.Enums.DbEnums;
using ThreatsParser.Entities.Enums;

namespace TreatsParser.Core.Helpers
{
    public static class Resolver
    {
        public static DangerLevel ResolveDanger(double coef)
        {
            if (coef <= 0.3) return DangerLevel.Low;
            if (coef <= 0.6) return DangerLevel.Medium;
            if (coef <= 0.8) return DangerLevel.High;
            return DangerLevel.High;
        }

        public static bool ResolveActual(DangerLevel dangerLevel, RiskProbabilities risk)
        {
            if (risk == RiskProbabilities.Unlikely) return false;
            if ((dangerLevel == DangerLevel.Low || dangerLevel == DangerLevel.Medium) &&
                risk == RiskProbabilities.Low) return false;
            if (dangerLevel == DangerLevel.Low && risk == RiskProbabilities.Medium) return false;

            return true;
        }

        public static List<string> ResolveSourceType(string value)
        {
            var result = new List<string>();
            value.Split(',').ToList().ForEach(x => result.Add(UppercaseFirst(x.Trim() == "" ? "Отсутствует" : x.Trim())));

            return result;
        }

        public static List<string> ResolveTargetsType(string value)
        {
            var result = new List<string>();
            string[] splitted = null;
            if (value.ToLower().Contains("микропрограммное, системное и прикладное программное обеспечение"))
            {
                result.Add("Микропрограммное, системное и прикладное программное обеспечение");
                splitted = value.Split(new string[] { "микропрограммное, системное и прикладное программное обеспечение", ","}, StringSplitOptions.RemoveEmptyEntries);
            }
            if (value.ToLower().Contains("виртуальные устройства хранения, обработки и передачи данных"))
            {
                result.Add("Виртуальные устройства хранения, обработки и передачи данных");
                splitted = value.Split(new string[] { "Виртуальные устройства хранения, обработки и передачи данных", "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (value.ToLower().Contains("информационная система, иммигрированная в облако"))
            {
                result.Add("Информационная система, иммигрированная в облако");
                splitted = value.Split(new string[] { "Виртуальные устройства хранения, обработки и передачи данных", ","}, StringSplitOptions.RemoveEmptyEntries);
            }

            splitted = value.Contains('(') ? new []{value} : value.Split(',');
            splitted.ToList().ForEach(x => result.Add(UppercaseFirst(x.Trim())));

            return result;
        }

        private static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }
}