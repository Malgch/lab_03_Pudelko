using PudelkoLibrary.Enums;
using PudelkoLibrary;
using System.Runtime.CompilerServices;

namespace PudelkoLibrary
{

    public static class ExtensionMethod
    {
        public static Pudelko Kompresuj(this Pudelko input)
        {
            var pudelkoVolume = input.Volume;

            if (input.a == input.b && input.a == input.c) return new Pudelko(input.a, input.a, input.c, UnitOfMeasure.meter);

            double newPudelkoEdge = Math.Round(Math.Pow(pudelkoVolume, 1.0 / 3 ), 9);

            return new Pudelko(newPudelkoEdge, newPudelkoEdge, newPudelkoEdge, UnitOfMeasure.meter);

        }


    }
}