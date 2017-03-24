/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using XamathonDemo2.Data.Models;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace XamathonDemo2.Data
{
    public partial class MovieRatingManager
    {
        static MovieRatingManager defaultInstance = new MovieRatingManager();

        public static MovieRatingManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public bool IsOfflineEnabled
        {
            get { return MovieManager.DefaultManager.IsOfflineEnabled || RatingManager.DefaultManager.IsOfflineEnabled; }
        }

        public async Task<ObservableCollection<MovieRating>> GetUnratedAsync(bool syncItems = false)
        {
            // getting ratings done by the user
            var ratings = await RatingManager.DefaultManager.GetRatingsAsync(syncItems);
            HashSet<string> ratedMovieIds = new HashSet<string>();
            foreach (var rating in ratings)
            {
                if (string.IsNullOrEmpty(rating.Id))
                    continue;

                if (!ratedMovieIds.Contains(rating.MovieId))
                    ratedMovieIds.Add(rating.MovieId);
            }

            // getting uncompleted movies
            var movies = await MovieManager.DefaultManager.GetMoviesAsync(syncItems);
            ObservableCollection<MovieRating> movieRatings = new ObservableCollection<MovieRating>();
            foreach (var movie in movies)
            {
                if (string.IsNullOrEmpty(movie.Id))
                    continue;

                // if it does not have a rating by the user
                if (!ratedMovieIds.Contains(movie.Id))
                {
                    movieRatings.Add(new Models.MovieRating() { Movie = movie, Rating = new Models.Rating() { MovieId = movie.Id, UserId = Globals.LoggedInUserId } });
                }
            }

            return movieRatings;
        }

        public async Task SaveItemAsync(MovieRating item)
        {
            await RatingManager.DefaultManager.SaveItemAsync(item.Rating);
        }
    }
}
