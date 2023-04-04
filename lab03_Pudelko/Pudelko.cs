using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLibrary
{

    public class Pudelko
    {
        public double A { get; init; }
        public double B { get; init; }
        public double C { get; init; }

        public Pudelko(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;

            if (a || b || c > 10)
            {
                throw new ArgumentException("");
            }
        }

        public Pudelko() : this(10, 10, 10) { }




    }
}
