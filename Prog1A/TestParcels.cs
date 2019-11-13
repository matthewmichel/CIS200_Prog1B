// Program 1B
// CIS 200-01
// Fall 2019
// Due: 2/10/2019
// By: M1752

// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy.
// It creates several different Parcels and prints them.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Prog1
{
    class TestParcels
    {
        // Precondition:  None
        // Postcondition: Parcels have been created and displayed
        static void Main(string[] args)
        {
            // Test Data - Magic Numbers OK
            Address a1 = new Address("John Smith", "123 Any St.", "Apt. 45",
                "Louisville", "KY", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.",
                "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321",
                "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7",
                "Portland", "ME", 04101); // Test Address 4
            Address a5 = new Address("George Washington", "7068 Lawrence St.", "Apt. 22",
                "Asheville", "NC", 28803); // Test Address 1
            Address a6 = new Address("John Adams", "264 Monroe Ave.",
                "Macungie", "PA", 18062); // Test Address 2
            Address a7 = new Address("James Madison", "318 Westport Street", "Suite 55",
                "North Miami Beach", "FL", 33160); // Test Address 3
            Address a8 = new Address("Andrew Jackson", "5 Edgemont Ave.", "Apt. 31",
                "Lockport", "NY", 14094); // Test Address 4

            Letter letter1 = new Letter(a1, a2, 3.95M);
            Letter letter2 = new Letter(a5, a7, 5.1M);
            Letter letter3 = new Letter(a2, a4, 1.5M);  // Letter test object
            GroundPackage gp1 = new GroundPackage(a3, a4, 14, 10, 5, 12.5);        // Ground test object
            GroundPackage gp2 = new GroundPackage(a8, a5, 17, 6, 6, 42.5);
            GroundPackage gp3 = new GroundPackage(a3, a6, 12, 15, 12, 36.2);
            NextDayAirPackage ndap1 = new NextDayAirPackage(a1, a3, 25, 15, 15, 85, 7.50M);
            NextDayAirPackage ndap2 = new NextDayAirPackage(a2, a4, 40, 17, 22, 102, 8.25M);
            NextDayAirPackage ndap3 = new NextDayAirPackage(a7, a8, 12, 5, 4, 23, 4.75M);
            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a4, a1, 46.5, 39.5, 28.0, 80.5, TwoDayAirPackage.Delivery.Saver);
            TwoDayAirPackage tdap2 = new TwoDayAirPackage(a5, a2, 22.7, 12.2, 17.1, 54.2, TwoDayAirPackage.Delivery.Saver);
            TwoDayAirPackage tdap3 = new TwoDayAirPackage(a6, a7, 34.9, 17.8, 15.0, 68.6, TwoDayAirPackage.Delivery.Early);

            List<Parcel> parcels;      // List of test parcels

            parcels = new List<Parcel>();

            parcels.Add(letter1); // Populate list
            parcels.Add(letter2);
            parcels.Add(letter3);
            parcels.Add(gp1);
            parcels.Add(gp2);
            parcels.Add(gp3);
            parcels.Add(ndap1);
            parcels.Add(ndap2);
            parcels.Add(ndap3);
            parcels.Add(tdap1);
            parcels.Add(tdap2);
            parcels.Add(tdap3);

            WriteLine("Original List:");
            WriteLine("====================");
            foreach (Parcel p in parcels)
            {
                WriteLine(p);
                WriteLine("====================");
            }

            // FIRST LINQ Query

            var parcelListQuery = // LINQ Query that selects all parcels sorted by the destination address' zip code
                from parcel in parcels
                orderby parcel.DestinationAddress.Zip descending
                select parcel;

            Console.WriteLine("==================================================================");
            Console.WriteLine(" List of all parcels sorted by destination zip code (descending):");
            Console.WriteLine("==================================================================\n");
            for (int i = 0; i<parcelListQuery.Count(); i++) // Loops over each parcel in the list created by the LINQ query
            {
                Console.WriteLine($"----- Parcel with position: {i}: -----\n");
                Console.WriteLine(parcelListQuery.ElementAt(i));
                Console.WriteLine($"----- End of parcel {i} --------------\n");
            }

            // SECOND LINQ Query

            var parcelByCostQuery = // LINQ Query that selects all parcels sorted by the the parcel's CalcCost() function
                from parcel in parcels
                orderby parcel.CalcCost() descending
                select parcel;

            Console.WriteLine("==================================================================");
            Console.WriteLine(" List of all parcels sorted by cost (descending):");
            Console.WriteLine("==================================================================\n");
            for (int i = 0; i < parcelByCostQuery.Count(); i++) // Loops over each parcel in the list created by the LINQ query
            {
                Console.WriteLine($"----- Parcel with position: {i}: -----\n");
                Console.WriteLine(parcelByCostQuery.ElementAt(i));
                Console.WriteLine($"----- End of parcel {i} --------------\n");
            }

            // THIRD LINQ Query

            var parcelByTypeAndCost = // LINQ Query that selects all parcels sorted first by the parcel's type and then by the parcel's CalcCost() function
                from parcel in parcels
                orderby parcel.GetType().ToString(), parcel.CalcCost() descending
                select parcel;

            Console.WriteLine("=======================================================================");
            Console.WriteLine(" List of all parcels sorted by type (ascending) and cost (descending):");
            Console.WriteLine("=======================================================================\n");
            for (int i = 0; i < parcelByTypeAndCost.Count(); i++) // Loops over each parcel in the list created by the LINQ query
            {
                Console.WriteLine($"----- Parcel with position: {i}: -----\n");
                Console.WriteLine(parcelByTypeAndCost.ElementAt(i));
                Console.WriteLine($"----- End of parcel {i} --------------\n");
            }

            // FOURTH LINQ Query

            var heavyAirPackagesByWeight = // LINQ Query that selects only heavy AirPackages sorted by the package's weight
                from parcel in parcels
                let airParcel = parcel as AirPackage // airParcel represents the downcasted representation of the selected parcel. If airParcel is null then the parcel object was not an AirParcel.
                where (airParcel != null) && airParcel.IsHeavy()
                orderby airParcel.Weight
                select airParcel;


            Console.WriteLine("=======================================================================");
            Console.WriteLine(" List of all AirPackages sorted by weight:");
            Console.WriteLine("=======================================================================\n");
            for (int i = 0; i < heavyAirPackagesByWeight.Count(); i++) // Loops over each parcel in the list created by the LINQ query
            {
                Console.WriteLine($"----- Parcel with position: {i}: -----\n");
                Console.WriteLine(heavyAirPackagesByWeight.ElementAt(i));
                Console.WriteLine($"----- End of parcel {i} --------------\n");
            }

            Pause();


       }

        // Precondition:  None
        // Postcondition: Pauses program execution until user presses Enter and
        //                then clears the screen
        public static void Pause()
        {
            WriteLine("Press Enter to Continue...");
            ReadLine();

            Console.Clear(); // Clear screen
        }
    }
}
