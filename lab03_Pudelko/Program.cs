using System;
using PudelkoLibrary;
using System.Numerics;
using System.Collections;
using System.Globalization;

//Pudelko p = new Pudelko(0,1,1, PudelkoLibrary.Enums.UnitOfMeasure.milimeter);

//Console.WriteLine(p.ToString());

//P(2.5, 9.321, 0.1) == P.Parse("2.500 m × 9.321 m × 0.100 m")
CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
Console.WriteLine("Culture is {0}", CultureInfo.CurrentCulture.Name);

string input = "2.500 m × 9.321 m × 0.100 m";
string[] values = input.Split(" ");

//double a, b, c;
/*string str = "2.500";
double d;
bool success = double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
Console.WriteLine(success);*/

//Convert.ToDouble(values[0], out a);
double.TryParse(values[0], out double b);
Console.WriteLine(b);
//double.TryParse(values[6], out c);

/*Console.WriteLine(b);

foreach (var x in values)
    Console.WriteLine(x);*/