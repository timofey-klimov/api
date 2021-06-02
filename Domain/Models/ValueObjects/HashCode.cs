using System;
using System.Linq;

namespace Domain.Models.ValueObjects
{
    public class HashCode : ValueObject<HashCode>
    {
        public byte[] Value { get; private set; }

        private HashCode()
        {

        }

        public HashCode(byte[] password)
        {
            if (password == null || !password.Any())
                throw new ArgumentNullException(nameof(password));
            Value = password;
        }

        public void Update(byte[] password)
        {
            if (password != null && !password.Any())
                throw new ArgumentNullException(nameof(password));

            Value = password;
        }

        protected override bool EqualsCore(HashCode password)
        {
            return Enumerable.SequenceEqual(password.Value, Value);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                if (Value == null)
                {
                    return 0;
                }
                int hash = 17;
                foreach (var element in Value)
                {
                    hash = hash * 31 + element.GetHashCode();
                }
                return hash;
            }
        }
    }
}
