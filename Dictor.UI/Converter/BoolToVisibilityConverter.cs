using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dictor.UI.Converter
{
    class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private Visibility NotShowVisibility { get; set; }

        private Visibility ShowVisibility { get; set; }


        public BoolToVisibilityConverter()
        {
            NotShowVisibility = Visibility.Collapsed;
            ShowVisibility = Visibility.Visible;
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return (bool)value ? ShowVisibility : NotShowVisibility;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

    }
}
