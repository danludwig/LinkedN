using System;
namespace LinkedN.MvcDemo.Models
{
    public class ProfileFieldCheckBox
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public PersonField EnumValue
        {
            get { return (PersonField)Enum.Parse(typeof (PersonField), Value); }
        }
        public bool IsChecked { get; set; }
    }
}