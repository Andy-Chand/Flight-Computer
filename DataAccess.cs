/****************************************************************************
ScriptName: Lab06-FlightComp > DataAccess
Coder: Andy Chand 
Student ID: 0983026

Date: 2020-11-02
vers     Date                    Coder       Issue
1.0      2020-11-18              Andy        Initial

*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoEngine_Workspace;

namespace Lab06_FlightComp
{
    public class DataAccess
    {
        public DataAccess()
        {

        }
        private String GetConnectionString()
        {
            return SERVER CONNECTION;
        }
        //Method to get and store all locations
        public List<Location> GetAllLocations()
        {
            //Lists the TempLocationList which will store all the information from the SQL Database
            List<Location> MasterAirportList = new List<Location>();

            try
            {
                //Accesses the connection and executes the stored procedure to get all airports
                System.Data.SqlClient.SqlDataReader t = PDM.Data.SqlHelper.ExecuteReader(GetConnectionString(), "sp_GetAllAirports");
                //If the database still has rows, it will continue
                if (t.HasRows)
                {
                    //While rows are present C# will continue to read and add the SQL database to the C# list
                    while (t.Read())
                    {
                        //Grabs the variables from Location and parses them appropriately 
                        Location s = new Location(int.Parse(t[0].ToString()), t[1].ToString(), t[2].ToString(), t[3].ToString(),
                            double.Parse(t[4].ToString()), double.Parse(t[5].ToString()), int.Parse(t[6].ToString()), t[7].ToString());
                        //Adds the row to the list
                        MasterAirportList.Add(s);
                    }
                }
            }
            //This is the catch block
            catch (SqlException SQLE)
            {
                //No Implementation yet...
                throw SQLE;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

            }
            //Returns the filled list
            return MasterAirportList;
        }
    }
}
