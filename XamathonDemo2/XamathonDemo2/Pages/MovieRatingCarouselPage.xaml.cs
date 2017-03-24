using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamathonDemo2.Data.Models;
using XamathonDemo2.Data;

namespace XamathonDemo2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieRatingCarouselPage : CarouselPage
    {
        readonly MovieRatingManager movieRatingManager;

        public MovieRatingCarouselPage()
        {
            InitializeComponent();

            movieRatingManager = MovieRatingManager.DefaultManager;

            //this.ItemsSource = new List<MovieRating>()
            //    {
            //        new MovieRating() { Movie = new Movie() { Id = "1", Title = "" }, Rating = new Data.Models.Rating() { MovieId = "1", UserId = Globals.LoggedInUserId } }
            //    };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await RefreshItems(true, syncItems: true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                var unratedMovies = await movieRatingManager.GetUnratedAsync(syncItems);

                this.ItemsSource = unratedMovies;
            }
        }

        private class ActivityIndicatorScope : IDisposable
        {
            private bool showIndicator;
            private ActivityIndicator indicator;
            private Task indicatorDelay;

            public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
            {
                this.indicator = indicator;
                this.showIndicator = showIndicator;

                if (showIndicator)
                {
                    indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                this.indicator.IsVisible = isActive;
                this.indicator.IsRunning = isActive;
            }

            public void Dispose()
            {
                if (showIndicator)
                {
                    indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }
    }
}
