using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Entities
{
    public class ModelLine
    {
        private bool _isActual;

        public int Id { get; set; }

        public string ThreatNumber { get; set; }

        public int Y2 { get; set; }

        public double Y { get; set; }

        public DangerLevel DangerLevel { get; set; }

        public string isActual
        {
            get => _isActual ? "Актуальная" : "Неактуальная";
            set => _isActual = value == "True";
        }
    }
}