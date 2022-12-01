using Ireland2022.Droid.Screens;
using Ireland2022.Screens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;
using Battery = Xamarin.Essentials.Battery;

namespace Ireland2022
{
    public partial class MainPage : ContentPage
    {

        
        public MainPage()
        {
            InitializeComponent();
            BackgroundColor = Color.White;
            HomeUpdate();
        }

        public void HomeUpdate()
        {
            string dt = "";
            
            



            AppVer.Text = Xamarin.Essentials.VersionTracking.CurrentVersion;

            if(Connectivity.NetworkAccess == NetworkAccess.Internet && !Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)) { DataOn.Text = "Data On"; } else { DataOn.Text = "Data Off"; }

            if(Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)) { WifiOn.Text = "Wifi On"; } else { WifiOn.Text = "Wifi Off"; }
           



            LOCTime.Text = "LOC: " + DateTime.Now.ToString("HH:mm");
            if (DateTime.Now.Month > 4 && DateTime.Now.Month < 11) { dt = DateTime.UtcNow.AddHours(1).ToString("HH:mm"); }
            else { dt = DateTime.UtcNow.ToString("HH:mm"); }
            DESTime.Text = "DUB:" + dt;
        }

        private async void FlightButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FlightPage());
        }

        private async void Currency_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CurrencyPage());
        }      
    }
}
