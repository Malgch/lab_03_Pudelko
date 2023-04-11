using PudelkoLibrary;
using System.Numerics;

Pudelko p = new Pudelko();

Console.WriteLine(p.ToString());

double a = 100.1; //centymetry 

//Console.WriteLine(Math.Truncate(a *100) / 10000); //na m

double b = Math.Truncate(a / 1000 * 10000) / 1000;
Console.WriteLine(b);
