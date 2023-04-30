using PudelkoLibrary.Enums;
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
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        public double a;
        public double b;
        public double c;
        public UnitOfMeasure Unit { get; init; }
        public double A
        {
            get { return a; }
            init { a = ConvertToMeters(value, Unit); }
        }
        public double B
        {
            get { return b; }
            init { b = ConvertToMeters(value, Unit); }
        }
        public double C
        {
            get { return c; }
            init { c = ConvertToMeters(value, Unit); }
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
            else if ((aMeters > 10) || (bMeters > 10) || (cMeters > 10))
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
        #region ToString method
        public override string ToString() //meters
        {
            string defaultEnumMeters = "m";
            return $"{A.ToString("0.000")} {defaultEnumMeters} \u00d7 {B.ToString("0.000")} {defaultEnumMeters} \u00d7 {C.ToString("0.000")} {defaultEnumMeters}";
        }

        public string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format)) format = "m";
            if (provider is null) provider = CultureInfo.CurrentCulture;

            if (format.ToLower() == "cm")
               return $"{A * 100:0.0} cm \u00d7 {B * 100:0.0} cm \u00d7 {C * 100:0.0} cm";
            
            else if (format.ToLower() == "m")
                return $"{A.ToString("0.000")} m \u00d7 {B.ToString("0.000")} m \u00d7 {C.ToString("0.000")} m";
           
            else if (format.ToLower() == "mm")           
                return $"{A * 1000:0} mm \u00d7 {B * 1000:0} mm \u00d7 {C * 1000:0} mm";
           
            else
                throw new FormatException("wrong format");
        }
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }
        #endregion

        public double Volume { get { return Math.Round(A * B * C, 9); }
        }

        public double Area {  get { return Math.Round(2 * A + 2 * B + 2 * C, 6); }
        }

        #region Equals and operators
        public bool Equals(Pudelko? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            Pudelko OtherPudelko = (Pudelko)other;
            double firstBox = Math.Max(Math.Max(A, B), C);
            double otherBox = Math.Max(Math.Max(OtherPudelko.A, OtherPudelko.B), OtherPudelko.C);
            return firstBox == otherBox;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Pudelko) return false;

            return Equals(obj as Pudelko);
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
        #endregion

        #region Conversios explicit implicit
        public static explicit operator double[](Pudelko box) //converter to double array
        {
            double[] converted = { (double)box.A, (double)box.B, (double)box.C };
            return converted;
        }

        public static implicit operator Pudelko((int a, int b, int c) dimensions)
        {
            return new Pudelko { A = ((double)dimensions.a / 1000), B = (double)dimensions.b / 1000, C = (double)dimensions.c / 1000, Unit = UnitOfMeasure.milimeter };
        }
        #endregion

        #region Indexer
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
        #endregion

        #region IEnumerator interface
        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        #region Parsing string to object method
        public static Pudelko Parse(string input)
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("The input string cannot be empty.");

            string[] values = input.Split(" ");
            if (values.Length != 8) throw new ArgumentException("Input string is not in correct format");
            if (values[1] != values[4] || values[1] != values[7]) throw new FormatException("Units are not correct!");
            if (values[2] != "×" || values[5] != values[2]) throw new FormatException("Fromat is not correct");

            double a, b, c;
            double.TryParse(values[0].Trim(), out a);
            double.TryParse(values[3].Trim(), out b);
            double.TryParse(values[6].Trim(), out c);

            UnitOfMeasure unit;
            if (values[1].Trim() == "m")
                unit = UnitOfMeasure.meter;            
            else if (values[1].Trim() == "cm")
                unit = UnitOfMeasure.centimeter;
            else if (values[1].Trim() == "mm")
                unit = UnitOfMeasure.milimeter;
            else
                throw new ArgumentException("The input string is not correct format");

            return new Pudelko(a, b, c, unit);           
        }
        #endregion 
    }
}
