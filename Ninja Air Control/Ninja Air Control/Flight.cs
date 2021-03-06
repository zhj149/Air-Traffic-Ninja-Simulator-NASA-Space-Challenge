﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaAirControl.Data;
using NinjaAirControl.Utils;

namespace NinjaAirControl
{
    /// <summary>
    /// Describes a single flight.
    /// Could be active, if aircraft is in flight or inactive if aircraft is on ground.
    /// </summary>
    public class Flight
    {
        private DateTime lastUpdated; 

        public Aircraft Aircraft { get; private set; }

        public AirTrafficController TrafficController { get; private set; } 

        public Person Pilot { get; private set; }

        public FlightPlan FlightPlan { get; private set; }

        public string Squack { get; set; }

        public bool IsActive { get; private set; }       
       
        public Position3D CurrentPosition { get; private set; }

        public int CurrentSpeed { get; private set; }

        public int CurrentAltitude { get; set; }

        public int CurrentHeadingInDegrees { get; private set; }     

        public Flight(Aircraft aircraft, Person pilot, FlightPlan flightPlan, string squack)
        {
            this.Aircraft = aircraft;
            this.Pilot = pilot;
            this.FlightPlan = flightPlan;
            this.Squack = squack;
            this.IsActive = true;
            this.CurrentPosition = flightPlan.DepartureAirport.Coordinates; //set first position to be equal to the departure airport
            this.CurrentSpeed = flightPlan.PreplannedSpeed;
            this.lastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Checks status of flight and sets the IsActive property
        /// </summary>
        public void CheckFlightStatus() 
        {
            if (CurrentPosition.CompareTo(FlightPlan.ArrivalAirport.Coordinates) == 0)
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// Method responsible for updating the position of aircraft at a certain time.
        /// </summary>
        public void UpdatePosition()
        {
            this.CurrentHeadingInDegrees = CalculateCurrentHeading(FlightPlan.PreplannedRoute.GetNextFix().Coordinates);
            double currentHeadingInRadians = MeasureConverter.ConvertDegreeToRadian(CurrentHeadingInDegrees);
            decimal newLongitude = CurrentPosition.Longitude;
            decimal newLatitude = CurrentPosition.Latitude;
            DateTime currentDateTime = DateTime.Now;
            double secondsElapsed = (currentDateTime - lastUpdated).Seconds;
            double distanceElapsed = secondsElapsed * CurrentSpeed / 3600; // to convert NM/h to NM/s
            newLongitude += (decimal)(distanceElapsed * Math.Sin(currentHeadingInRadians));
            newLatitude += (decimal)(distanceElapsed * Math.Cos(currentHeadingInRadians));
            this.CurrentPosition = new Position3D(newLongitude, newLatitude, CurrentPosition.Altitude);
            lastUpdated = currentDateTime;
        }

        public int CalculateCurrentHeading(Position3D nextPosition)
        {
            int updatedHeading=0;
            if (CurrentPosition.Longitude<nextPosition.Longitude)
            {
                double longitudeDifference=(CurrentPosition.LongitudeInNauticalMiles-nextPosition.LongitudeInNauticalMiles);
                double latitudeDifference = (CurrentPosition.LatitudeInNauticalMiles - nextPosition.LatitudeInNauticalMiles);
                double distanceBetweenPoints = Math.Sqrt(longitudeDifference*longitudeDifference+latitudeDifference*latitudeDifference);
                updatedHeading = (int)MeasureConverter.ConvertRadiansToDegrees((
                        Math.Asin(Math.Abs(longitudeDifference) / distanceBetweenPoints)));
                if (longitudeDifference>0)
                {
                    updatedHeading=updatedHeading + 180;
                }
            }

            return updatedHeading;
        }
    }
}
