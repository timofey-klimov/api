using System;

namespace Domain.Models.ValueObjects
{
    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        public string Value { get; private set; }

        private PhoneNumber()
        {

        }

        public PhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            Value = phoneNumber;
        }

        protected override bool EqualsCore(PhoneNumber phoneNumber)
        {
            return phoneNumber.Equals(Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode() ^ 17;
        }
    }
}
