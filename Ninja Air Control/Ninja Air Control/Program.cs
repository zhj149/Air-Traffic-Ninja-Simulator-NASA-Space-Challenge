﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaAirControl
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("TEst");
            //FlightRoute test = new FlightRoute();
            //test.AddFix(new AirspaceFix(new Position3D(1,2,3), true,true));
            //Console.WriteLine(test.Route[0].IsMandatoryToReport);
            AircraftIdData testData = new AircraftIdData();
            AirTrafficController testController = new AirTrafficController(TrafficControllerType.Approach);
            Pilot testPilot = new Pilot("ivan");
            Airport testDeparturePort = new Airport("testAirport", new Position3D(0,0,1), new Dispatcher("disp"));
            Airport testArrivalPort = new Airport("testAirport2", new Position3D(10,10,1), new Dispatcher("disp"));
            FlightRoute testRoute = new FlightRoute();
            testRoute.AddFix(new AirspaceFix(new Position3D(2, 2, 2), false, false));
            FlightPlan testPlan = new FlightPlan(testDeparturePort, testArrivalPort, new DateTime(), new DateTime(), 10, testRoute, 100);
            Aircraft testAircraft = new Aircraft(testData, testController, testPilot, testPlan, 10, 45);
            Console.WriteLine(testAircraft.CurrentPosition);
            testAircraft.UpdatePosition();
            Console.WriteLine(testAircraft.CurrentPosition);
            //AirControlEngine engine = new AirControlEngine();
            //engine.Start();

        }
    }
}
