using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamathonDemo2.Data;
using XamathonDemo2.Data.Models;

namespace XamathonDemo2.Pages
{
    public partial class Login : ContentPage
    {
        readonly UserManager userManager;

        //public User User { get; set; }


        public Login()
        {
            InitializeComponent();

            userManager = UserManager.DefaultManager;

            this.BindingContext = this;
        }

        public async void OnLogin(object sender, EventArgs e)
        {
            //var movie = new Movie { Title = newItemName.Text };
            //await AddItem(movie);

            var user = await userManager.GetUserByName(enUserNameForLogin.Text);

            // az alábbi kódért nem válalok felelősséget
            // nem én voltam, nem is vagyok informatikus
            // hanem bölcsész. ha olvasnád... :)
            Globals.LoggedInUserId = user.Id;
            App.ChangeMain(new MovieRatingCarouselPage());
;        }
    }
}
