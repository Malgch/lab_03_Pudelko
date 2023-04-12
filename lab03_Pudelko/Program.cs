using PudelkoLibrary;
using System.Numerics;

Pudelko p = new Pudelko();

Console.WriteLine(p.ToString());

double a = 1; //mm

//Console.WriteLine(Math.Truncate(a *100) / 10000); //na m

double b = Math.Truncate(a / 1000); //mm na m
Console.WriteLine(b);
