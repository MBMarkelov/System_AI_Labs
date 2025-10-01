using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.Domain.Enums;

namespace SAI.Domain.ValueObjects
{
    public sealed class Email : IEquatable<Email>
    {
        public string NameEmail { get; }
        public string DomainEmail { get; }
        public Email(string nameEmail, string domainEmail)
        {
            NameEmail = nameEmail;
            DomainEmail = domainEmail;
        }
        public static Email Create(string nameEmail, string domainEmail)
        {
            if (nameEmail == null) throw new ArgumentNullException(nameof(nameEmail), "Email cannot be empty");
            if( domainEmail == null) throw new ArgumentNullException(nameof(domainEmail), "domain cannot be empty");
            if (!IsValideDomain(domainEmail)) throw new ArgumentException(nameof(domainEmail), "invalid domain address");

            return new Email(nameEmail, domainEmail);
        }
        public bool Equals(Email other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.NameEmail == other.NameEmail && this.DomainEmail == other.DomainEmail;
        }
        public override bool Equals(object? obj) => Equals(obj as Email);
        public override int GetHashCode() => HashCode.Combine(NameEmail, DomainEmail);
        public override string ToString() => $"{NameEmail}@{DomainEmail}";
        private static bool IsValideDomain(string domain)
        {
            domain = domain.ToLower().Trim();
            foreach(string domainI in Enum.GetValues(typeof(EmailDomain)))
            {
                string enumDomain = domainI.ToLower().Trim().Replace("_", ".");
                if(enumDomain == domain) return true;
            }
            return false;
        }
    }
}
