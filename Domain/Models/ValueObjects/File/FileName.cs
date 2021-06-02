using System;

namespace Domain.Models.ValueObjects
{
    public class FileName : ValueObject<FileName>
    {
        public string Value { get; private set; }

        private FileName()
        {

        }

        public FileName(string value)
            :base()
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(FileName));
            Value = value;
        }

        protected override bool EqualsCore(FileName value)
        {
            return value.Value.Equals(Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode() * 31;
        }
    }
}
