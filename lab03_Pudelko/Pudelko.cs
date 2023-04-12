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
            get => ConvertToMeters(a, Unit);
            set { a = value; }
        }

        // public double a { get; init; }
        public double B
        {
            get { return ConvertToMeters(b, Unit); }
            set { a = value; }
        }
        public double C
        {
            get { return ConvertToMeters(c, Unit); }
            set { a = value; }
        }


        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            Unit = unit;

            //conversion
            double aMeters = ConvertToMeters(a, Unit);
            double bMeters = ConvertToMeters(b, Unit);
            double cMeters = ConvertToMeters(c, Unit);


            if ((aMeters <= 0) || (bMeters <= 0) || (cMeters <= 0))
            {
                throw new ArgumentOutOfRangeException("The sizes of the box can not have negative values!");
            }
            else if ((aMeters >= 10) || (bMeters >= 10) || (cMeters >= 10))
            {
                throw new ArgumentOutOfRangeException("The size of box can not be greater than 10 meters!");
            }

            switch (unit)
            {
                case UnitOfMeasure.centimeter:
                case UnitOfMeasure.milimeter:
                    {
                        this.a = aMeters;
                        this.b = bMeters;
                        this.c = cMeters;
                        return;
                    }
            }
        }


        private double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    return Math.Truncate(value * 1000.0) / 1000.0;
                case UnitOfMeasure.centimeter:
                    return Math.Truncate(value / 100 * 1000) / 1000;
                case UnitOfMeasure.milimeter:
                    return Math.Truncate(value) / 1000;
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
