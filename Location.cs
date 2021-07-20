/****************************************************************************
ScriptName: Lab06-FlightComp > Location
Coder: Andy Chand 
Student ID: 0983026

Date: 2020-11-02
vers     Date                    Coder       Issue
1.0      2020-11-18              Andy        Initial

*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using GeoEngine_Workspace;

namespace Lab06_FlightComp
{
    public class Location
    {
        //Declares all the variables present in the Airport Database 
        //Declared as public, so it is accessible
        public int AirportID { get; set; }
        public string IATACode { get; set; }
        public string AirportName { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Elevation { get; set; }
        public string CountryName { get; set; }

        //Public method "Location" that is pulled to format the list in "DataAccess"
        public Location(int AirportID, string IATACode, string AirportName, string City, double Latitude, double Longitude, int Elevation, String CountryName)
        {
            this.AirportID = AirportID;
            this.IATACode = IATACode;
            this.AirportName = AirportName;
            this.City = City;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Elevation = Elevation;
            this.CountryName = CountryName;
        }
    }

    public class RouteFlight
    {
        //Declares all the variables present in the User Input/Route List 
        //Declared as public, so it is accessible
        public string ICAOCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double BearingTo { get; set; }
        public double Distance { get; set; }

        //Public method "Location" that is pulled to format the list in "DataAccess"
        public RouteFlight(String ICAOCode, double Latitude, double Longitude, double BearingTo, double Distance)
        {
            this.ICAOCode = ICAOCode;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.BearingTo = BearingTo;
            this.Distance = Distance;
        }
    }
}
