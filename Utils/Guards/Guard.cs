using System;

namespace Utils.Guards
{
    public static class Guard
    {
        public static void GuardAgainstNull<T>(T value, string name)
            where T:class
        {
            if (value == null)
                throw new ArgumentNullException($"{name} of type {value.GetType()} can't be null");
        }
    }
}
