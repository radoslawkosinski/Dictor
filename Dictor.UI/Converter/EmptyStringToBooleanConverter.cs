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
    class EmptyStringToBooleanConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Disable UI element when string is empty
        /// </summary>
        private Boolean EmptyVisibility { get; set; }

        /// <summary>
        /// Enable UI element when string is empty
        /// </summary>
        private Boolean NotEmptyVisibility { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmptyStringToBooleanConverter" />.
        /// </summary>
        public EmptyStringToBooleanConverter()
        {
            EmptyVisibility = false;
            NotEmptyVisibility = true;
        }


        /// <summary>
        /// converts string to bool (enable/disable UI element)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string res = (string)value;

            var NonNullItem = !string.IsNullOrEmpty(res);

            return NonNullItem ? NotEmptyVisibility  : EmptyVisibility;

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
