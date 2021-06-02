using System;

namespace Utils.Extensions
{
    public static class CastExtensions
    {
        public static T To<T>(this object obj)
        {
             return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
