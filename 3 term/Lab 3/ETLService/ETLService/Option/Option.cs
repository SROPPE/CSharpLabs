using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Option
{
    public class Option<T>
    {
        bool _valueIsPresent;
        T _value;

        public Option(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            _valueIsPresent = true;
            _value = value;
        }

        public bool IsPresent
        { get { return _valueIsPresent; } }

        public T Value
        {
            get
            {
                if (!_valueIsPresent)
                { throw new InvalidOperationException("Value is not present. Restored to default"); }
                return _value;
            }
        }
    }
}
