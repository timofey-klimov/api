using System;

namespace Domain.Models.ValueObjects
{
    public class Login : ValueObject<Login>
    {
        public string Value { get; private set; }

        private Login()
        {

        }

        public Login(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException(nameof(login));
            Value = login;
        }
        protected override bool EqualsCore(Login login)
        {
            return login.Value.Equals(Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode() * 31;
        }
    }
}
