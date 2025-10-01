using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.ValueObjects
{
    public sealed class PhoneNumber : IEquatable<PhoneNumber>
    {
        public string Number { get;}
        public string? CountryCode { get;}

        private PhoneNumber(string number, string? countryCode) {
            Number = NormalizationNumber(number);
            CountryCode = countryCode?.Replace("+", "").Trim();
        }
        public static PhoneNumber Create(string number, string? countryCode)
        {
            if (number == null) throw new ArgumentNullException(nameof(number), "Number cannot be empty");
            if (!System.Text.RegularExpressions.Regex.IsMatch(number, @"^[\d\s\-\(\)]+$")) throw new ArgumentException("Number have invalid charapter", nameof(number));
            return new PhoneNumber(number.Trim(), countryCode);
        }
        public bool Equals(PhoneNumber? other)
        {
            if(other is null) return false;
            if(ReferenceEquals(this, other)) return true;
            return this.Number == other.Number && this.CountryCode == other.CountryCode;
        }
        public override bool Equals(object? obj)
        {
            return base.Equals(obj as PhoneNumber);
        }
        public static bool operator ==(PhoneNumber? left, PhoneNumber? right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(PhoneNumber? left, PhoneNumber? right) => !(left == right);
        public override int GetHashCode() => HashCode.Combine(this.Number, this.CountryCode);
        public override string ToString() => this.CountryCode != null ? $"{this.CountryCode} {this.Number}" : this.Number;
        private static string NormalizationNumber(string number) => new string(number.Where(char.IsDigit).ToArray());
    }
}
