using System;
using PudelkoLibrary;
using System.Numerics;
using System.Collections;
using System.Globalization;
using PudelkoLibrary.Enums;
using System.Runtime.CompilerServices;

namespace PudelkoLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // List of pudelko objects
            List<Pudelko> pudelkoList = new()
            {
                new Pudelko(2.5, 4, 4, UnitOfMeasure.centimeter),
                new Pudelko(2.5, 4, 4, UnitOfMeasure.meter),
                new Pudelko(100, 250, 350, UnitOfMeasure.centimeter),
                new Pudelko(100, 250, 350, UnitOfMeasure.centimeter),
                new Pudelko(null, 250, null, UnitOfMeasure.centimeter),
                new Pudelko(null, null, null, UnitOfMeasure.milimeter),
                Pudelko.Parse("2000 mm × 9321 mm × 100 mm"),
                Pudelko.Parse("2.123 m × 4.56 m × 9.12 m")

             };

            Console.WriteLine("List of Pudelko objects: ");
            foreach( Pudelko p in pudelkoList )
                Console.WriteLine(p.ToString());

            pudelkoList.Sort(Comparision);

            Console.WriteLine("\nList of sorted Pudelko objects: ");
            foreach(Pudelko p in pudelkoList)
                Console.WriteLine(p.ToString());

            static int Comparision(Pudelko p1, Pudelko p2)
            {
                if (p1 is null || p2 is null)
                    throw new ArgumentOutOfRangeException("Pudelko can not be null");
                if (p1.Volume.CompareTo(p2.Volume) != 0)
                    return p1.Volume.CompareTo(p2.Volume);
                else if (p1.Area.CompareTo(p2.Area) != 0)
                    return p1.Area.CompareTo(p2.Area);
                else
                    return (p1.A + p1.B + p1.C).CompareTo(p2.A + p2.B + p2.C);
            }

            Console.WriteLine($"\nSprawdzenie metody Equals. Czy pudełko\n{pudelkoList[0]}\njest równe pudełkom\n{pudelkoList[1]} \n{pudelkoList[2]}");
            Console.WriteLine($"{pudelkoList[0].Equals(pudelkoList[1])}\n{pudelkoList[1].Equals(pudelkoList[2])}");

            var newInstance = new Pudelko(5, 8, 2, UnitOfMeasure.meter);
            Console.WriteLine($"\nSprawdzenie metody Kompresuj. Zamiana pudełka\n{newInstance}, o objętości {newInstance.Volume} m3 na pudełko sześcienne");
            var compressedPudelko = newInstance.Kompresuj();

            Console.WriteLine($"Nowe pudełko sześcienne ma dlugości boków {compressedPudelko.ToString()}");


        }
    }
}

