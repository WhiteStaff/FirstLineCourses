﻿using ThreatsParser.Entities.Enums;

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

        #region Coefficients Getters

        public int TerritorialLocationCoef
        {
            get
            {
                switch (TerritorialLocation)
                {
                    case LocationCharacteristic.Government:
                        return 10;
                    case LocationCharacteristic.City:
                        return 10;
                    case LocationCharacteristic.Cooperative:
                        return 5;
                    case LocationCharacteristic.SeveralLocal:
                        return 5;
                    default:
                        return 0;
                }
            }
        }

        public int NetworkCharacteristicCoef
        {
            get
            {
                switch (NetworkCharacteristic)
                {
                    case NetworkCharacteristic.MultiPointPublic:
                        return 10;
                    case NetworkCharacteristic.OnePointPublic:
                        return 5;
                    default:
                        return 0;
                }
            }
        }

        public int PersonalDataActionCharacteristicsCoef
        {
            get
            {
                switch (PersonalDataActionCharacteristics)
                {
                    case PersonalDataActionCharacteristics.ReadWrite:
                        return 0;
                    case PersonalDataActionCharacteristics.WriteDeleteSort:
                        return 5;
                    default:
                        return 10;
                }
            }
        }

        public int PersonalDataPermissionSplitCoef =>
            PersonalDataPermissionSplit == PersonalDataPermissionSplit.SomeStaff ? 5 : 10;

        public int OtherDbConnectionsCoef => OtherDbConnections == OtherDBConnections.OtherDb ? 10 : 0;

        public int AnonymityLevelCoef
        {
            get
            {
                switch (AnonymityLevel)
                {
                    case AnonymityLevel.FullyAnonymous:
                        return 0;
                    case AnonymityLevel.OnlyInside:
                        return 5;
                    default:
                        return 10;
                }
            }
        }

        public int PersonalDataSharingLevelCoef
        {
            get
            {
                switch (PersonalDataSharingLevel)
                {
                    case PersonalDataSharingLevel.Full:
                        return 10;
                    case PersonalDataSharingLevel.Partly:
                        return 5;
                    default:
                        return 0;
                }
            }
        }

        #endregion
    }
}