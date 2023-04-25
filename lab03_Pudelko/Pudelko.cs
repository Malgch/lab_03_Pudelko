using PudelkoLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            get { return a; }
            set { a = ConvertToMeters(value, Unit); }
        }

        // public double a { get; init; }
        public double B
        {
            get { return b; }
            set { b = ConvertToMeters(value, Unit); }
        }
        public double C
        {
            get { return c; }
            set { c = ConvertToMeters(value, Unit); }
        }

        #region Constructor
        public Pudelko(double? a = null, double? b = null , double? c = null , UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            Unit = unit; 
            if (a is null || b is null || c is null) //sets default values for null parameters depending on unit of measurement
            {
                switch (unit)
                {
                    case UnitOfMeasure.meter:
                        {
                            this.a = a ?? 0.1;
                            this.b = b ?? 0.1;
                            this.c = c ?? 0.1;
                            break;
                        }
                    case UnitOfMeasure.centimeter:
                        {

                            this.a = a ?? 10;
                            this.b = b ?? 10;
                            this.c = c ?? 10;
                            break;
                        }
                    case UnitOfMeasure.milimeter:
                        {
                            this.a = a ?? 100;
                            this.b = b ?? 100;
                            this.c = c ?? 100;
                            break;
                        }
                }
            }
            else
            {
                this.a = (double)a;
                this.b = (double)b;
                this.c = (double)c;
            }

            //conversion
            double aMeters = ConvertToMeters(this.a, Unit);
            double bMeters = ConvertToMeters(this.b, Unit);
            double cMeters = ConvertToMeters(this.c, Unit);

            //exceptions
            if ((aMeters <= 0) || (bMeters <= 0) || (cMeters <= 0))
            {
                throw new ArgumentOutOfRangeException("The sizes of the box can not have negative values!");
            }
            else if ((aMeters >= 10) || (bMeters >= 10) || (cMeters >= 10))
            {
                throw new ArgumentOutOfRangeException("The size of box can not be greater than 10 meters!");
            }

            //assignment of converted-to-meters value
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
        #endregion


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

        public override string ToString() //meters
        {
            string defaultEnumMeters = "m";
            return $"{A.ToString("0.000")} {defaultEnumMeters} \u00d7 {B.ToString("0.000")} {defaultEnumMeters} \u00d7 {C.ToString("0.000")} {defaultEnumMeters}";
        }

        public string ToString(string format)
        {
            if (string.IsNullOrEmpty(format)) format = "m";

            if (format.ToLower() == "cm")
            {
                return $"{A * 100:0.0} cm \u00d7 {B * 100:0.0} cm \u00d7 {C * 100:0.0} cm";
            }
            else if (format.ToLower() == "m")
            {
                return $"{A.ToString("0.000")} m \u00d7 {B.ToString("0.000")} m \u00d7 {C.ToString("0.000")} m";
            }
            else if (format.ToLower() == "mm")
            {
                return $"{A * 1000:0} mm \u00d7 {B * 1000:0} mm \u00d7 {C * 1000:0} mm";
            }
            else
                throw new FormatException("wrong format");
        }


        public double Volume
        {
            get { return Math.Round(A * B * C, 9); }
        }

    }
}
