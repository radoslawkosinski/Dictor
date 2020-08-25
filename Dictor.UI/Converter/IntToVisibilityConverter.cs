using Dictor.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dictor.UI.Converter
{
    public class IntToVisibilityConverter : MarkupExtension, IValueConverter
    {

        private Visibility NotShowVisibility { get; set; }

        private Visibility ShowVisibility { get; set; }


        public IntToVisibilityConverter()
        {
            NotShowVisibility = Visibility.Collapsed;
            ShowVisibility = Visibility.Visible;
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var count = (int)value;



            return count > 0 ? ShowVisibility : NotShowVisibility;

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
