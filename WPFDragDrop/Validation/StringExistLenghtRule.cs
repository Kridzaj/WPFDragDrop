using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DomainModelEditor.Validation
{
    [ContentProperty("ExistingValues")]
    public class StringExistLenghtRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public ExistingValues ExistingValues { get; set; }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if ((value.ToString().Length < Min) || ((value.ToString().Length > Max)))
            {
                return new ValidationResult(false,
                  $"Input text must be between {Min} and {Max}");
            }
            if (ExistingValues.Value.Contains(value.ToString()))
            {
                return new ValidationResult(false,
                 $"Input value already exists");
            }
            return new ValidationResult(true, null);
        }
    }

    public class ExistingValues : DependencyObject
    {
        public string[] Value
        {
            get 
            {
                return (string[])GetValue(ValueProperty);
            }
            set 
            { 
                SetValue(ValueProperty, value); 
            }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string[]),
            typeof(ExistingValues),
            new PropertyMetadata(default(string[])));
    }
}
