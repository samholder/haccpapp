using System;

namespace Hacapp.Core
{
    public class Optional<T>
    {
        private T value;

        public Optional()
        {
            HasValue = false;
        }

        public Optional(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value",
                    "You cannot create an Optional<T> with a null value for the T.  " +
                    "If you want the optional value to be unspecified please use the default constructor.");
            }

            HasValue = true;
            Value = value;
        }

        public T Value
        {
            get
            {
                if (HasValue)
                {
                    return value;
                }

                throw new InvalidOperationException(
                    "You attempted to access the value of an Optional<T> when it has no value.  " +
                    "Please check the value of HasValue before accessing the Value property and " +
                    "only attempt access if this property returns True");
            }
            private set { this.value = value; }
        }

        public bool HasValue { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is Optional<T>)
            {
                return Equals((Optional<T>) obj);
            }
            return false;
        }

        public bool Equals(Optional<T> other)
        {
            if (HasValue && other.HasValue)
            {
                return Equals(value, other.value);
            }
            return HasValue == other.HasValue;
        }

        public override int GetHashCode()
        {
            return HasValue ? Value.GetHashCode() : base.GetHashCode();
        }
    }
}