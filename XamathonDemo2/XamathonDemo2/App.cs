using System;

using Xamarin.Forms;
using XamathonDemo2.Pages;

namespace XamathonDemo2
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new MovieList();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts

            Globals.LoggedInUserId = "50534869-116d-46ad-8b6a-5ca0a5fe6036";
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

