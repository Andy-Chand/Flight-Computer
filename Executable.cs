/****************************************************************************
ScriptName: Lab06-FlightComp > Executable
Coder: Andy Chand 
Student ID: 0983026

Date: 2020-11-02
vers     Date                    Coder       Issue
1.0      2020-11-18              Andy        Initial
1.1      2020-11-23              Andy        Updated coding for FindAirport and 
                                             printing routes

*********************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GeoEngine_Workspace;

namespace Lab06_FlightComp
{
    public class AirportListModify
    {
        //Finds the airport from the Master list and adds it to temp lists as necessary and the final route list
        public static List<RouteFlight> FindAirport(List<Location> MasterAirportList, String AirportCode, List<RouteFlight> Route, List<Location> TempRoute, List<GeoEngine_Workspace.Location> TempGeoBearing)
        {
            //Brings in GeoEngine functionality
            GeoEngine_Workspace.GeoEngine G = new GeoEngine_Workspace.GeoEngine();
            //Tests to verify if the user input matches the master list and if it has already been entered/Returns true or false
            bool ContainsAirport = MasterAirportList.Any(a => a.IATACode.Contains(AirportCode));
            bool ContainedInTemp = TempRoute.Any(x => x.IATACode.Contains(AirportCode));
            //Default bearing angle and distance to throw into the lists as needed
            double GetBearing = 0;
            double Distance = 0;

            //Check to verify the user input is in the master list and hasn't been entered already
            if (ContainsAirport && ContainedInTemp != true)
            {
                //Finds the user input in the master list and adds the whole row to TempRoute
                TempRoute.Add(MasterAirportList.Find(x => x.IATACode.Contains(AirportCode))); 
                //Adds the specific needed values from TempRoute to Route list
                Route.Add(new RouteFlight(TempRoute[TempRoute.Count-1].IATACode, TempRoute[TempRoute.Count - 1].Latitude, TempRoute[TempRoute.Count - 1].Longitude, GetBearing, Distance));
                //Adds the found data from the master list and formats it to be used in GeoEngine when finding bearing
                TempGeoBearing.Add(new GeoEngine_Workspace.Location(TempRoute[TempRoute.Count - 1].Latitude, TempRoute[TempRoute.Count - 1].Longitude, TempRoute[TempRoute.Count - 1].Elevation));

                //Continues if theres at least 2 entries in the TempRoute list
                if (TempRoute.Count > 1)
                {
                    //Solved the Bearing and Distance with the help of GeoEngine
                    GetBearing = G.BearingTo(TempGeoBearing[TempGeoBearing.Count - 2], TempGeoBearing[TempGeoBearing.Count - 1]);
                    Distance = G.Distance(TempGeoBearing[TempGeoBearing.Count - 2], TempGeoBearing[TempGeoBearing.Count - 1], 'm'); ;

                    //Updates previous entries in the Route list with the new bearing and distances
                    Route[Route.Count - 2].BearingTo = Math.Round(GetBearing, 2);
                    Route[Route.Count - 2].Distance = Math.Round(Distance, 2);
                }
                //Returns the final Route list
                return Route;
            }
            //If the user input isn't in the Master list, it will repeat from the beginning
            else
            {
                ContainsAirport = false;
            }
            //Returns the final Route list
            return Route;
        }
        //Method that outputs the intro/updated list for the program
        public static void PrintRoute(List<RouteFlight> Route)
        {
            Console.WriteLine("Andy's Flight Computer Console Edition\tStudent Number: 0983026\n");
            Console.WriteLine("'A' to Add Airport to Route\t'R' to Remove last Airport from Route\n'P' to Print Route to File\t'Q' to Quit\n\n");
            Console.WriteLine("Airport Code\tLatitude\tLongitude\tBearing to Next Airport\t\tDistance to Next Airport");
            int x = 0;
            //Writes each entry of the list in Route
            foreach (RouteFlight RF in Route)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}\t\t\t\t{4}", Route[x].ICAOCode, Route[x].Latitude, Route[x].Longitude, Route[x].BearingTo, Route[x].Distance);
                x++;
            }
            //Double to solve for the total distance in the route so far
            double DistanceTotal = Route.Sum(m => m.Distance);
            Console.WriteLine("\nNumber of Airports: {0}\nLength of Route: {1}\n\n", Route.Count, Math.Round(DistanceTotal, 2));
        }
    }
    class Executable
    {
        static void Main(string[] args)
        {
            //A true/false statement to be used in the while loop to keep the programming looping if the user wants
            bool KeepLoop = true;
            //Brings in the Master List of all the airports from DataAccess
            DataAccess d = new DataAccess();
            List<Location> MasterAirportList = new List<Location>();
            MasterAirportList = d.GetAllLocations();
            List<RouteFlight> Route = new List<RouteFlight>();
            List<Location> TempRoute = new List<Location>();
            List<GeoEngine_Workspace.Location> TempGeoBearing = new List<GeoEngine_Workspace.Location>();

            //Keeps the program running until the user quits
            while (KeepLoop)
            {
                //Calls the method to print the program
                Lab06_FlightComp.AirportListModify.PrintRoute(Route);
                Console.WriteLine("Make a selection:");
                var UserInput = Console.ReadLine();

                //Allows the user to add airports
                if (UserInput.ToUpper() == "A")
                {
                    Console.WriteLine("Please enter a 3 letter Airport Code");
                    String AirportCode = (Console.ReadLine()).ToUpper();
                    //Throws all the necessary variables into the method FindAirport
                    Route = AirportListModify.FindAirport(MasterAirportList, AirportCode, Route, TempRoute, TempGeoBearing);
                    //Clears the console to reprint everything after updating, instead of constant scrolling
                    Console.Clear();
                }
                //Allows the user to remove the last airport
                else if (UserInput.ToUpper() == "R")
                {
                    //Checks to make sure there's at least 1 entry in the Route list
                    if (Route.Count >= 1)
                    {
                        //Removes last entry
                        Route.RemoveAt(Route.Count - 1);
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        continue;
                    }
                }
                //Allows the user to print the list
                else if (UserInput.ToUpper() == "P")
                {
                    using (TextWriter tw = new StreamWriter("SavedList.txt"))
                    {
                        int count = 0;

                        foreach (RouteFlight RF in Route)
                        {
                            tw.WriteLine(Route[count].ICAOCode + " " + Route[count].Latitude + " " + Route[count].Longitude + " " + Route[count].BearingTo + " " + Route[count].Distance);
                            count++;
                        }
                    }
                    Route.Clear();
                    Console.Clear();
                }
                //Allows the user to quit the program
                else if (UserInput.ToUpper() == "Q")
                {
                    KeepLoop = false;
                }
                //An else statement that outputs when the user enters a negative number, a 0, any number more than 52, or any non-int value
                else
                {
                    Console.WriteLine("Entry is not valid, try again.\nHit enter to continue.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}
