using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamathonDemo2.Data.Models;
using XamathonDemo2.Data;

namespace XamathonDemo2.Pages
{
    public partial class MovieList : ContentPage
    {
        //readonly MovieManager movieManager;
        //readonly RatingManager ratingManager;
        readonly MovieRatingManager movieRatingManager;

        public MovieList()
        {
            InitializeComponent();

            //movieManager = MovieManager.DefaultManager;
            //ratingManager = RatingManager.DefaultManager;
            movieRatingManager = MovieRatingManager.DefaultManager;

            // OnPlatform<T> doesn't currently support the "Windows" target platform, so we have this check here.
            if (movieRatingManager.IsOfflineEnabled &&
                (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone))
            {
                var syncButton = new Button
                {
                    Text = "Sync items",
                    HeightRequest = 30
                };
                syncButton.Clicked += OnSyncItems;

                buttonsPanel.Children.Add(syncButton);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await RefreshItems(true, syncItems: true);
        }

        // Data methods
        async Task AddItem(MovieRating item)
        {
            await movieRatingManager.SaveItemAsync(item);
            movieList.ItemsSource = await movieRatingManager.GetUnratedAsync();
        }

        async Task CompleteItem(MovieRating item)
        {
            item.Movie.Done = true;
            await movieRatingManager.SaveItemAsync(item);
            movieList.ItemsSource = await movieRatingManager.GetUnratedAsync();
        }

        public async void OnAdd(object sender, EventArgs e)
        {
            //var movie = new Movie { Title = newItemName.Text };
            //await AddItem(movie);

            newItemName.Text = string.Empty;
            newItemName.Unfocus();
        }

        // Event handlers
        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var movieRating = e.SelectedItem as MovieRating;
            if (Device.OS != TargetPlatform.iOS && movieRating != null)
            {
                // Not iOS - the swipe-to-delete is discoverable there
                if (Device.OS == TargetPlatform.Android)
                {
                    await DisplayAlert(movieRating.Movie.Title, "Press-and-hold to complete task " + movieRating.Movie.Title, "Got it!");
                }
                else
                {
                    // Windows, not all platforms support the Context Actions yet
                    if (await DisplayAlert("Mark completed?", "Do you wish to complete " + movieRating.Movie.Title + "?", "Complete", "Cancel"))
                    {
                        await CompleteItem(movieRating);
                    }
                }
            }

            // prevents background getting highlighted
            movieList.SelectedItem = null;
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#context
        public async void OnComplete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var movie = mi.CommandParameter as MovieRating;
            await CompleteItem(movie);
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        public async void OnSyncItems(object sender, EventArgs e)
        {
            await RefreshItems(true, true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                var unratedMovies = await movieRatingManager.GetUnratedAsync(syncItems);

                movieList.ItemsSource = unratedMovies;
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

