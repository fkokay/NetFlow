using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Where(x => x != null)
                .Aggregate(0, (hash, obj) => HashCode.Combine(hash, obj));
        }
    }

}
