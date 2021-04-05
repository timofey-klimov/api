using System;

namespace Domain.Models
{
    public abstract class BaseEntity<T> : BaseNotifyEntity
        where T : IEquatable<T>
    {
        public T Id { get; private set; }

        protected BaseEntity()
            :base()
        {

        }

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity<T>;

            if (GetType() != obj.GetType())
                return false;

            if (ReferenceEquals(obj, null))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public static bool operator ==(BaseEntity<T> firstEntity, BaseEntity<T> secondEntity)
        {
            if (ReferenceEquals(firstEntity, null) && ReferenceEquals(secondEntity, null))
                return true;

            if (ReferenceEquals(firstEntity, null) || ReferenceEquals(secondEntity, null))
                return false;

            return firstEntity.Equals(secondEntity);
        }

        public static bool operator !=(BaseEntity<T> firstEntity, BaseEntity<T> secondEntity)
        {
            return !(firstEntity == secondEntity);
        }
    }
}
