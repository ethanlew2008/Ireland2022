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
using System.Diagnostics;
using System.Data;
using Xamarin.Forms.PlatformConfiguration;

namespace Ireland2022
{
    public partial class MainPage : ContentPage
    {
        APIClient clnt = new APIClient();
        Stopwatch flighttimer = new Stopwatch();
        Stopwatch sleeptimer = new Stopwatch();

        public MainPage()
        {
            InitializeComponent();
            BackgroundColor = Color.White;
            clnt.GetWeather();
            HomeUpdate();
            clnt.GetGBP();          
            
            if (AppInfo.RequestedTheme == AppTheme.Dark) { BackgroundColor = Color.DarkSlateGray; }          
        }

        public void HomeUpdate()
        {
            string dt = "";

            AppVer.Text = Xamarin.Essentials.VersionTracking.CurrentVersion;

            if(Connectivity.NetworkAccess == NetworkAccess.Internet && !Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)) { DataOn.Text = "Data On"; } else { DataOn.Text = "Data Off"; }

            if(Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)) { WifiOn.Text = "Wifi On"; } else { WifiOn.Text = "Wifi Off"; }
            
            if (DateTime.Now.Month > 4 && DateTime.Now.Month < 11) { dt = DateTime.UtcNow.AddHours(1).ToString("HH:mm"); }
            else { dt = DateTime.UtcNow.ToString("HH:mm"); }
            DESTime.Text = "DUB:" + dt;

            LOCTime.Text = "DUB: " + clnt.intcarb + "℃";
        }

        public void FlightButton_Clicked(object sender, EventArgs e) { FlightUpdate();}
        public void SleepButton_Clicked(object sender, EventArgs e) { SleepUpdate(); }

        public void FlightUpdate()
        {
            if (!flighttimer.IsRunning) { flighttimer.Start(); }

            try { if (Convert.ToInt32(FlightTime.Text) <= 0) { flighttimer.Reset(); return; } } catch (Exception) { }
            FlightPer.Text = "" + Convert.ToInt32(100 - (flighttimer.Elapsed.TotalMinutes / 85) * 100) + "% left";
            FlightTime.Text = Convert.ToString(Convert.ToInt32(85 - flighttimer.Elapsed.TotalMinutes)) + " Mins";
        }

        public void SleepUpdate()
        {
            if (!sleeptimer.IsRunning) { sleeptimer.Start(); }

            try { if (Convert.ToInt32(SleepTime.Text) <= 0) { sleeptimer.Reset(); return; } } catch (Exception) { }
            SleepTime.Text = string.Format("{0}:{1:00}", (int)(TimeSpan.FromMinutes(sleeptimer.Elapsed.TotalMinutes)).TotalHours, (TimeSpan.FromMinutes(sleeptimer.Elapsed.TotalMinutes)).Minutes);
            SleepBreaths.Text = "" + Convert.ToInt32(sleeptimer.Elapsed.TotalMinutes * 15) + " Breaths";

            //SleepBreaths.Text = "" + clnt.intcarb;
        }
       
        private void CurrencyEUR_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(clnt.varsyr == "Offline") { CurrencyEUR.Text = clnt.varsyr; }
            try {  CurrencyGBP.Text = "£" + Math.Round(Convert.ToDouble(CurrencyEUR.Text) * Convert.ToDouble(clnt.varsyr), 2);} catch (Exception) {}
        }

        private void UpdateButton_Clicked(object sender, EventArgs e) { if (sleeptimer.IsRunning) { SleepUpdate(); } if (flighttimer.IsRunning) { FlightUpdate(); } HomeUpdate(); }       
    }
}
