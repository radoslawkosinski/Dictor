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
    public class ResultEmptyOnlineExampleListToVisibilityConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// The visibility value of an empty or null <see cref="IEnumerable" />.
        /// </summary>
        private Visibility EmptyVisibility { get; set; }

        /// <summary>
        /// The visibility value of a non empty <see cref="IEnumerable" />.
        /// </summary>
        private Visibility NotEmptyVisibility { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmptyEnumerableVisibilityConverter" />.
        /// </summary>

        public ResultEmptyOnlineExampleListToVisibilityConverter()
        {
            EmptyVisibility = Visibility.Collapsed;
            NotEmptyVisibility = Visibility.Visible;
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var NonNullItem = 0;
            var res = (TranslationResult)value;


            if (res != null)
            NonNullItem = res.Results.Where(x => x.OnlineExamples.Count() > 0).Count();


            return NonNullItem > 0 ? EmptyVisibility : NotEmptyVisibility;

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
