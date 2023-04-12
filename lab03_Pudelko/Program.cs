using PudelkoLibrary;
using System.Numerics;

//Pudelko p = new Pudelko(0,1,1, PudelkoLibrary.Enums.UnitOfMeasure.milimeter);

//Console.WriteLine(p.ToString());

double a = 1234.6442; //mm

Console.WriteLine((Math.Truncate(a)/1000)); //na m

double b = Math.Truncate(a / 1000); //mm na m
Console.WriteLine(b);
