using PudelkoLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLibrary
{

    public class Pudelko
    {
        public double a;
        public double b;
        public double c;
        public UnitOfMeasure Unit { get; init; }
        public double A
        {
            get { return Math.Round(ConvertToMeters(a, Unit), 3); }
            set { a = value; }
        }

        // public double a { get; init; }
        public double B
        {
            get { return Math.Round(ConvertToMeters(b, Unit), 3); }
            set { a = value; }
        }
        public double C
        {
            get { return Math.Round(ConvertToMeters(c, Unit), 3); }
            set { a = value; }
        }



        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.Unit = unit;


            if ((a > 10 ) || ( b > 10) || (c > 10))
            {
                throw new ArgumentOutOfRangeException("The size of box can not be greater than 10 meters!");
            }
            else if ((a <= 0) || (b <= 0) || (c <= 0))
            {
                throw new ArgumentOutOfRangeException("The sizes of the box can not have negative values!");
            }    
        }

        public Pudelko() : this(10, 10, 10, UnitOfMeasure.centimeter) { }

        private double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            switch(unit)
            {
                case UnitOfMeasure.meter:
                    return value;
                case UnitOfMeasure.centimeter:
                    return value * 100;
                case UnitOfMeasure.milimeter:
                    return value * 1000;
                default:
                    throw new ArgumentException("Please provide correct unit of measure");
            }
        }

        public override string ToString()
        {
            return $"{A} {Unit} \u00d7  {B} {Unit} \u00d7 {C} {Unit} ";
        }

        public string ToString(string format)
        {
            return format;
        }



    }
}
