using System;

namespace Domain.Models.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string Value { get; private set; }

        private Email()
        {
        }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            Value = email;
        }


        protected override bool EqualsCore(Email email)
        {
            return email.Value.Equals(Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode() ^ 17;
        }
    }
}
