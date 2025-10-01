using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.ValueObjects
{
    public sealed class EventDateRange : IEquatable<EventDateRange>
    {
        public DateTime From {  get; }
        public DateTime? To { get; }

        public EventDateRange(DateTime start, DateTime? end) 
        {
            From = start;
            To = end;
        }
        public static EventDateRange Create(DateTime start, DateTime? end = null)
        {
            if (end.HasValue && end.Value < start) throw new Exception("end time early start time");
            return new EventDateRange(start, end);
        }
        public bool Contains(DateTime date)
        {
            if (To.HasValue) return date >= From && date <= To.Value;
            if (!To.HasValue) return date >= From;
            return false;
        }
        public bool OverlapWith(EventDateRange other)
        {
            DateTime EndTimeOther = other.To ?? DateTime.MaxValue; 
            DateTime EndTimeSelf = To ?? DateTime.MaxValue;

            return To <= EndTimeOther && EndTimeSelf >= other.To; 
        }
        public bool Equals(EventDateRange other) 
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.From == other.From && other.To == this.To;
        }
        public override bool Equals(object? obj) => Equals(obj as EventDateRange);

        public override int GetHashCode() => HashCode.Combine(this.From, this.To);
        public override string ToString() => To.HasValue ? $"{this.From} - {this.To}" : $"{this.From}";
    }
}
