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

            //conversion
            double aMeters = ConvertToMeters(a, Unit);
            double bMeters = ConvertToMeters(b, Unit);
            double cMeters = ConvertToMeters(c, Unit);


            if ((a <= 0) || (b <= 0) || (c <= 0))
            {
                throw new ArgumentOutOfRangeException("The sizes of the box can not have negative values!");
            }
            else if ((aMeters > 10) || (bMeters > 10) || (cMeters > 10))
            {
                throw new ArgumentOutOfRangeException("The size of box can not be greater than 10 meters!");
            }

            switch (unit)
            {
                case UnitOfMeasure.meter:
                    {
                        if ((a > 10) || (b > 10) || (c > 10))
                        {
                            
                        }
                        break;
                    }
                case UnitOfMeasure.centimeter:
                    {
                        if ((a > 100) || (b > 100) || (c > 10))
                        {
                            throw new ArgumentOutOfRangeException("The size of box can not be greater than 10 meters!");
                        }
                        break;

                    }
                case UnitOfMeasure.milimeter:
                    {
                        if ((a > 1000) || (b > 1000) || (c > 1000))
                        {
                            throw new ArgumentOutOfRangeException("The size of box can not be greater than 10 meters!");
                        }
                        break;

                    }
            } 
        }


        private double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            switch(unit)
            {
                case UnitOfMeasure.meter:
                    return value;
                case UnitOfMeasure.centimeter:
                    return Math.Round(value / 100, 1);
                case UnitOfMeasure.milimeter:
                    return Math.Round(value / 1000, 0);
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
