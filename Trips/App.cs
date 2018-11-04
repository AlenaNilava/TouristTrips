using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;

namespace Trips
{
    class App
    {       
        static void Main(string[] args)
        {
			//TODO: add exteranal source for trips
			//Test trips, in order to get more restuls, trips can be loaded from outside file (database or txt, xml)
			SeaTrip seaTrip = new SeaTrip("0", "sea", "Italy", "03-22-2018", 11, 250.4, "plain", "AI", 200.34, "private");
			ExcursionTrip tourTrip = new ExcursionTrip("1", "excursion", "Czech Republic", "06-12-2017", 3, 76.5, "bus", "BB", "Explore historial places", "Visit church", 25.7);
			ShoppingTrip shopTrip = new ShoppingTrip("2", "shopping", "Poland", "05-28-2018", 5, 30.6, "bus", "BC", "Arkadia Shopping Mall");
			HealthTrip healthTrip = new HealthTrip("3", "health", "Israel", "05-10-2018", 2, 315.75, "train", "BB", "Massage, Inhalation, Mineral Baths");

			//Add trips to catalog
			List<Trip> catalog = new List<Trip>() { seaTrip, tourTrip, shopTrip, healthTrip };

			//Trip Info from user
			UserService.InitilizeUserInfo();

			//Filter the records
			List<Trip> filteredTrips = Utility.filterByPurpose(UserService.purposeParameter, catalog);
            filteredTrips = Utility.filterByTransport(UserService.transportParameter, filteredTrips);
            filteredTrips = Utility.filterByFoodType(UserService.foodTypeParameter, filteredTrips);
            filteredTrips = Utility.filterByDuration(UserService.durationParameter, filteredTrips);

			//Output the records
			Utility.sortingByPrice(filteredTrips);
            Utility.displayOrders(filteredTrips);

			//read, write from txt file
			string fileNameText = "TripsCatalog.txt";
			List<Trip> tripsFromTextFile = new List<Trip>();
			Utility.writeToText(fileNameText, catalog);
            ArrayList tripsArray = Utility.readFromText(fileNameText);
            for (int i = 0; i < tripsArray.Count; i++ )
            {   
                try
                {
                    tripsFromTextFile.Add(Utility.stringParser((string)tripsArray[i]));
                }
                catch (TripRecordIsNotReadException e)
                {
                    Console.WriteLine("Trip record is not read." + "\nException details:" + "\n" + e.Message + "\nObject = " + e.Source + "\nMethod - " + e.TargetSite + "\nCall stack - " + e.StackTrace);
                }                
            }
            Console.WriteLine("Trips from txt file:");
            Utility.displayOrders(tripsFromTextFile);


			//read, write from binary file
			string fileNameBin = "TripsCatalog.dat";
			List<Trip> tripsFromBinFile = new List<Trip>();
			Utility.writeToBinary(fileNameBin, catalog);
            tripsFromBinFile = Utility.readFromBinary(fileNameBin);
            Console.WriteLine("Trips from dat file:");
            Utility.displayOrders(tripsFromBinFile);

			//read, write from xml file
			string fileNameXml = "TripsCatalog.xml";
			List<Trip> tripsFromXmlFile = new List<Trip>();
			Utility.SerializeToXml(fileNameXml, catalog);
            tripsFromXmlFile = Utility.DeserializeFromXml(fileNameXml);
            Console.WriteLine("Trips from XML file:");
            Utility.displayOrders(tripsFromXmlFile);

			//work with DB
			SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.AppSettings["cnStr"];
            cn.Open();
            
            Database.readFromDB(cn);

            int numberOfAffectedRows;
            string command = "DELETE FROM dbo.Products WHERE UnitPrice = (SELECT MAX(UnitPrice) FROM dbo.Products)";
            numberOfAffectedRows = Database.executeNonQuery(cn, command);
            Console.WriteLine("Number of affected rows: {0}", numberOfAffectedRows);

            command = "UPDATE dbo.Shippers SET CompanyName = 'test' WHERE ShipperID = 1";
            numberOfAffectedRows = Database.executeNonQuery(cn, command);
            Console.WriteLine("Number of affected rows: {0}", numberOfAffectedRows);

            command = "INSERT INTO dbo.Region ( RegionID, RegionDescription ) VALUES ( 6, N'RegionFromProgram')";
            numberOfAffectedRows = Database.executeNonQuery(cn, command);
            Console.WriteLine("Number of affected rows: {0}", numberOfAffectedRows);

            string categoryName = "Beverages";
            string ordYear = "1996";
            Database.executeSalesByCategoryProc(cn, categoryName, ordYear);
        }
    }
}
