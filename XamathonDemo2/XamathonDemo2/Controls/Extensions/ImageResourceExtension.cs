using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfoFrame.Controls.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Source != null ? ImageSource.FromResource(Source) : null;
        }
    }
}
