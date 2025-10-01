using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.ValueObjects
{
    public sealed class Address : IEquatable<Address>
    {
        public string HouseNumber { get; }
        public string Street { get; }
        public string City { get; }
        public string District { get; }
        public string Region { get; }
        public string? PostalIndex { get; }
        public string? Country { get; }

        private Address(string houseNumber, string street, string city, string district, string region, string? postalIndex, string? country)
        {
            HouseNumber = houseNumber;
            Street = street;
            City = city;
            District = district;
            Region = region;
            PostalIndex = postalIndex;
            Country = country;
        }
        public static Address Create(string houseNumber, string street, string city, string district, string region, string postalIndex, string country)
        {
            if (string.IsNullOrWhiteSpace(houseNumber)) { throw new ArgumentNullException(nameof(houseNumber), "HouseNumber cannot be empty"); }
            if (string.IsNullOrWhiteSpace(street)) { throw new ArgumentNullException(nameof(street), "Street cannot be empty"); }
            if (string.IsNullOrWhiteSpace(city)) { throw new ArgumentNullException(nameof(city), "City cannot be empty"); }
            if (string.IsNullOrWhiteSpace(district)) { throw new ArgumentNullException(nameof(district), "District cannot be empty"); }
            if (string.IsNullOrWhiteSpace(region)) { throw new ArgumentNullException(nameof(region), "Region cannot be empty"); }
            return new Address(houseNumber.Trim(), street.Trim(), city.Trim(), district.Trim(), region.Trim(), string.IsNullOrWhiteSpace(postalIndex) ? null :postalIndex.Trim(), string.IsNullOrWhiteSpace(country) ? null : country.Trim());
        }

        public bool Equals(Address? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other))  return true;
            return this.Street == other.Street && this.HouseNumber == other.HouseNumber && this.City == other.City && this.District == other.District && this.Region == other.Region;
        }
        public override bool Equals(object? obj) => Equals(obj as Address);
        public override int GetHashCode() => HashCode.Combine(HouseNumber, City, District, Region);
        public static bool operator ==(Address a, Address b) => a.Equals(b);
        public static bool operator !=(Address a, Address b) => !(a == b); 
        public override string ToString() => $"{HouseNumber}, {Street}, {City}, {District}, {Region}, {PostalIndex}, {Country}";

    }
}
