Legacy Code: Run FlightComputer.exe to see a running example without the SQL database and stored procedures.

This program is about taking data from an existing database (DB_Airports) and using the stored procedures to pull all airports. 

FlightComputer will:
•	Pull data from DB_Airports using sp_GetAllAirports using the SITG Data Access Library
•	Create a List of Airport Objects from that data. The Airport Object captures all the data that comes from sp_GetAllAirports.
•	has a menu system, calculates distance between waypoints, bearings and other data that is displayed to the user.
•	Allow the user to add and remove waypoints.
•	FlightComputer will print a flight datafile that is capable of being read by the Uber Toolset provided. 
