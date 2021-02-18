using System;
using System.Collections.Generic;

namespace ThreeTermController
{
    public class UnsignedDouble : IEquatable<UnsignedDouble>, IComparable<UnsignedDouble>
    {
        private readonly double value;

        public UnsignedDouble(double value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            this.value = value;
        }
        
        public static implicit operator double(UnsignedDouble ud) => ud.value;
        public static explicit operator UnsignedDouble(double d) => new UnsignedDouble(d);
        
        public static double operator +(UnsignedDouble left, double right) => left.value + right;
        public static double operator +(double left, UnsignedDouble right) => left + right.value;
        
        public static double operator -(UnsignedDouble left, double right) => left.value - right;
        public static double operator -(double left, UnsignedDouble right) => left - right.value;
        
        public static double operator *(UnsignedDouble left, double right) => left.value * right;
        public static double operator *(double left, UnsignedDouble right) => left * right.value;

        public bool Equals(UnsignedDouble other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UnsignedDouble) obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(UnsignedDouble left, UnsignedDouble right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnsignedDouble left, UnsignedDouble right)
        {
            return !Equals(left, right);
        }

        public int CompareTo(UnsignedDouble other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return value.CompareTo(other.value);
        }

        public static bool operator <(UnsignedDouble left, UnsignedDouble right)
        {
            return Comparer<UnsignedDouble>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(UnsignedDouble left, UnsignedDouble right)
        {
            return Comparer<UnsignedDouble>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(UnsignedDouble left, UnsignedDouble right)
        {
            return Comparer<UnsignedDouble>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(UnsignedDouble left, UnsignedDouble right)
        {
            return Comparer<UnsignedDouble>.Default.Compare(left, right) >= 0;
        }
    }
}