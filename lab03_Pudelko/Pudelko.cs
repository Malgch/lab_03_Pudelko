﻿using PudelkoLibrary.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLibrary
{
    public class Pudelko : IEnumerable<double>
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
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
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

        public double Area
        {
            get { return Math.Round(2 * A + 2 * B + 2 * C, 6); }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Pudelko) return false;

            Pudelko OtherPudelko = (Pudelko)obj;
            double firstBox = Math.Max(Math.Max(A, B), C);
            double otherBox = Math.Max(Math.Max(OtherPudelko.A, OtherPudelko.B), OtherPudelko.C);
            return firstBox == otherBox;
        }
        public override int GetHashCode() => HashCode.Combine(A, B, C);

        public static bool operator ==(Pudelko? boxA, Pudelko? boxB)
        {
            if (boxA is null && boxB is null) return true;
            if (boxA is null) return false;

            return boxA.Equals(boxB);
        }

        public static bool operator !=(Pudelko? boxA, Pudelko? boxB) => !(boxA == boxB);

        public static Pudelko operator +(Pudelko boxA, Pudelko boxB)
        {
            double[] box1Dimensions = { boxA.A, boxA.B, boxA.C };
            double[] box2Dimensions = { boxB.A, boxB.B, boxB.C };

            Array.Sort(box1Dimensions);
            Array.Sort(box2Dimensions);

            double height = Math.Max(box1Dimensions[2], box2Dimensions[2]);
            double width = box1Dimensions[1] + box2Dimensions[1];
            double lenght = box1Dimensions[0] + box2Dimensions[0];
            return new Pudelko(height, width, lenght);
        }

        public static explicit operator double[](Pudelko box) //converter to double array
        {
            double[] converted = { (double)box.A, (double)box.B, (double)box.C };
            return converted;
        }

        public static implicit operator Pudelko((int a, int b, int c) dimensions) //to be tested
        {
            return new Pudelko { A = ((double)dimensions.a / 1000), B = (double)dimensions.b / 1000, C = (double)dimensions.c / 1000, Unit = UnitOfMeasure.milimeter };
        }

        public double this[int index]
        {
            get {
                switch (index)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }


        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
}
