using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Base
{
    public abstract class Equatable<T> : IEquatable<T> where T : class, IEquatable<T>
    {
        public abstract override int GetHashCode();

        public bool Equals(T other)
        {
            if ((object)other == null)
            {
                return false;
            }
            return GetHashCode() == other.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public static bool operator ==(Equatable<T> lhs, Equatable<T> rhs)
        {
            if ((object)lhs != null && (object)rhs != null)
            {
                return object.Equals(lhs, rhs);
            }
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Equatable<T> lhs, Equatable<T> rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
