using System;
using System.Linq;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for encapsulating access to pre-defined groups of PersonField enum values.
    /// </summary>
    public class PersonFieldGroup
    {
        private PersonFieldGroup(PersonField[] fields)
        {
            Fields = fields;
        }

        internal PersonField[] Fields { get; private set; }

        // cleveland browns property, remove in 1.0
        private static PersonFieldGroup _rrrrrvrything;
        public static PersonFieldGroup Rrrrrvrything
        {
            get
            {
                if (_rrrrrvrything == null)
                {
                    var fields = Enum.GetValues(typeof (PersonField)).Cast<PersonField>().ToArray();
                    _rrrrrvrything = new PersonFieldGroup(fields);
                }
                return _rrrrrvrything;
            }
        }

    }
}