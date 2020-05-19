using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LB_7_CSharp
{
    class RationalNumbers:IComparable<RationalNumbers>,IConvertible,IFormattable
    {
        private int numerator;
        private int denominator;
        public int Numerator {
            get { return numerator; }
            private set { numerator = value; } }
        public int Denominator
        {
            get { return denominator; }
            private set
            {
                if (value > 0f)
                    denominator = value;
                else
                    denominator = 1;
            }
        }
        public double RationalNumber { get;private set; }
        public RationalNumbers(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
            RationalNumber = this.numerator / (double)this.denominator;
        }

        public RationalNumbers Add(RationalNumbers rn)
        {
            int numerator = (this.numerator * rn.denominator) + (rn.numerator * this.denominator);
            int denominator = this.denominator * rn.denominator;

            return new RationalNumbers(numerator, denominator);
        }
        public RationalNumbers Subtract(RationalNumbers rn)
        {
            int numerator = (this.numerator * rn.denominator) - (rn.numerator * this.denominator);
            int denominator = this.denominator * rn.denominator;

            return new RationalNumbers(numerator, denominator);
        }

        public RationalNumbers Multiply(RationalNumbers rn)
        {
            int numerator = this.numerator * rn.numerator;
            int denominator = this.denominator * rn.denominator;

            return new RationalNumbers(numerator, denominator);
        }

        public RationalNumbers Divide(RationalNumbers rn)
        {
            if (rn.numerator == 0)
            {
                throw new DivideByZeroException();
            }
            int numerator = this.numerator * rn.denominator;
            int denominator = rn.numerator * this.denominator;
            return new RationalNumbers(numerator, denominator);
        }
        public override int GetHashCode()
        {
            int hashCode = -64356464;
            hashCode = hashCode * -int.MaxValue/3 + numerator.GetHashCode();
            hashCode = hashCode * -int.MaxValue/3 + denominator.GetHashCode();
            return hashCode;
        }
        public int CompareTo(RationalNumbers other)
        {
            return this.RationalNumber.CompareTo(other.RationalNumber);
        }
        public bool Equals(RationalNumbers other)
        {
            return this.RationalNumber == other.RationalNumber;
        }

        public override bool Equals(object obj)
        {
            RationalNumbers rn = obj as RationalNumbers; 
            return base.Equals(rn);
        }
        public override string ToString()
        {
           return ToString("F");
        }

        public string ToString(string format)
        {
           return ToString(format, null);
        }
        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "F";
            }
            if (format == "IF")
            {
                if (Math.Abs(numerator) > denominator && denominator != 1)
                {
                    long integral = numerator / denominator;
                    return $"{integral}({Math.Abs(numerator) % denominator}/{denominator})";
                }
                else
                {
                    format = "I";
                }
            }
            if (format == "I")
            {
                if (Math.Abs(numerator) > denominator)
                {
                    long integral = numerator / denominator;
                    return integral.ToString();
                }
                else
                {
                    format = "F";
                }
            }
            if (format == "F")
            {
                return $"{numerator}/{denominator}";
            }
            else if (format == "D")
            {
                return RationalNumber.ToString();
            }
            else if (new Regex(@"D\d*").IsMatch(format))
            {
                return RationalNumber.ToString("F" + format.Substring(1));
            }
            else
            {
                throw new FormatException($"Invalid format \"{format}\".");
            }
        }
        public static RationalNumbers Parse(string s)
        {
            if (TryParse(s, out RationalNumbers res))
            {
                return res;
            }
            else
            {
                throw new FormatException("Incorrect format.");
            }
        }
        public static bool TryParse(string s, out RationalNumbers result)
        {
            Regex pattern1 = new Regex(@"^(-?\d+)/(\d+)$");
            Regex pattern2 = new Regex(@"^(-?\d+)$");
            Regex pattern3 = new Regex(@"^(-?\d+),(\d+)$");

            if (pattern1.IsMatch(s))    
            {
                Match match = pattern1.Match(s);
                result = new RationalNumbers(int.Parse(match.Groups[1].Value),
                                            int.Parse(match.Groups[2].Value));
                return true;
            }
            if (pattern2.IsMatch(s))
            {
                Match match = pattern2.Match(s);
                result = new RationalNumbers(int.Parse(match.Groups[1].Value), 1);
                return true;
            }
            if (pattern3.IsMatch(s))
            {
                Match match = pattern3.Match(s);
                int integral = int.Parse(match.Groups[1].Value);
                int sign = integral > 0 ? 1 : -1;
                string floating = match.Groups[2].Value;
                int denominator = BinPow(10, floating.Length);
                result = new RationalNumbers((Math.Abs(integral) * denominator + int.Parse(floating))
                                        * sign,
                                        denominator);
                return true;
            }
            result = null;
            return false;

        }
        static int BinPow(int a, int n)
        {
            if (n == 0)
                return 1;
            if (n % 2 == 1)
                return a * BinPow(a, n - 1);

            int b = BinPow(a, n / 2);
            return b * b;
        }
        #region(converts)
        public static explicit operator int(RationalNumbers f) =>
           f.ToInt32(null);

        public static explicit operator long(RationalNumbers f) =>
            f.ToInt64(null);

        public static explicit operator float(RationalNumbers f) =>
            f.ToSingle(null);

        public static explicit operator double(RationalNumbers f) =>
            f.ToDouble(null);

        public static explicit operator decimal(RationalNumbers f) =>
            f.ToDecimal(null);

        public TypeCode GetTypeCode() =>
            TypeCode.Object;

        public bool ToBoolean(IFormatProvider provider) =>
            numerator != 0;

        public char ToChar(IFormatProvider provider) =>
            Convert.ToChar(RationalNumber, provider);

        public sbyte ToSByte(IFormatProvider provider) =>
            Convert.ToSByte(RationalNumber, provider);

        public byte ToByte(IFormatProvider provider) =>
            Convert.ToByte(RationalNumber, provider);

        public short ToInt16(IFormatProvider provider) =>
            Convert.ToInt16(RationalNumber, provider);

        public ushort ToUInt16(IFormatProvider provider) =>
            Convert.ToUInt16(RationalNumber, provider);

        public int ToInt32(IFormatProvider provider) =>
            Convert.ToInt32(RationalNumber, provider);

        public uint ToUInt32(IFormatProvider provider) =>
            Convert.ToUInt32(RationalNumber, provider);

        public long ToInt64(IFormatProvider provider) =>
            Convert.ToInt64(RationalNumber, provider);

        public ulong ToUInt64(IFormatProvider provider) =>
            Convert.ToUInt64(RationalNumber, provider);

        public float ToSingle(IFormatProvider provider) =>
            Convert.ToSingle(RationalNumber, provider);

        public double ToDouble(IFormatProvider provider) =>
            RationalNumber;

        public decimal ToDecimal(IFormatProvider provider) =>
            Convert.ToDecimal(RationalNumber, provider);

        public DateTime ToDateTime(IFormatProvider provider) =>
            Convert.ToDateTime(RationalNumber, provider);

        public string ToString(IFormatProvider provider) =>
            ToString("F", provider);

        public object ToType(Type conversionType, IFormatProvider provider) =>
            Convert.ChangeType(RationalNumber, conversionType);
        #endregion;
        #region(operators)
        public static RationalNumbers operator +(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.Add(num2);
        }
        public static RationalNumbers operator -(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.Subtract(num2);
        }
        public static RationalNumbers operator *(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.Multiply(num2);
        }

        public static RationalNumbers operator /(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.Divide(num2);
        }
        public static bool operator ==(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.Equals(num2);
        }

        public static bool operator !=(RationalNumbers num1, RationalNumbers num2)
        {
            return !num1.Equals(num2);
        }
        public static bool operator <(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.CompareTo(num2) < 0;
        }

        public static bool operator >(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.CompareTo(num2) > 0;
        }

        public static bool operator <=(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.CompareTo(num2) <= 0;
        }

        public static bool operator >=(RationalNumbers num1, RationalNumbers num2)
        {
            return num1.CompareTo(num2) >= 0;
        }
        #endregion;
    }
}
