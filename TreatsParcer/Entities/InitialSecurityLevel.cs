using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Entities
{
    public class InitialSecurityLevel
    {
        public LocationCharacteristic TerritorialLocation { get; set; }

        public NetworkCharacteristic NetworkCharacteristic { get; set; }

        public PersonalDataActionCharacteristics PersonalDataActionCharacteristics { get; set; }

        public PersonalDataPermissionSplit PersonalDataPermissionSplit { get; set; }

        public OtherDBConnections OtherDbConnections { get; set; }

        public AnonymityLevel AnonymityLevel { get; set; }

        public PersonalDataSharingLevel PersonalDataSharingLevel { get; set; }
    }
}