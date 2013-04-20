﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaAirControl
{
    public class AirspaceFix
    {
        public AirspaceFix(Position3D coordinates, bool isRadioNavigationalPoint, bool isMandatoryToReport)
        {
            this.Coordinates = coordinates;
            this.IsMandatoryToReport = isMandatoryToReport;
            this.IsRadioNavigationalPoint = isRadioNavigationalPoint;
        }

        public bool IsRadioNavigationalPoint { get; private set; }

        public bool IsMandatoryToReport { get; private set; }

        public Position3D Coordinates { get; private set; }
        // has:
        //-Position3D
        //-isRadioNav (bool) - is it a radio navigational point or not
        //-isMandatoryToReport (bool) - does the aircraft have to report to AirTrafficController
    }
}