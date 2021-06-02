using System;

namespace Domain.Models.ValueObjects
{
    public class FilePath : ValueObject<FilePath>
    {
        public string Value { get; private set; }

        public FilePath(string filePath)
            :base()
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(FilePath));

            Value = filePath;
        }

        protected override bool EqualsCore(FilePath value)
        {
            return value.Value.Equals(Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode() * 31;
        }
    }
}
