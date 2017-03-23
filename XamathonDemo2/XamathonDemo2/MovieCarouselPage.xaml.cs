using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamathonDemo2
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieCarouselPage : CarouselPage
    {
        public MovieCarouselPage()
        {
            InitializeComponent();
        }
    }
}
