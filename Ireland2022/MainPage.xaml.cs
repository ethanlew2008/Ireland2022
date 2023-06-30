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
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Ireland2022
{
    public partial class MainPage : ContentPage
    {
        public APIClient clnt = new APIClient();
        public Stopwatch flighttimer = new Stopwatch();
        public Stopwatch sleeptimer = new Stopwatch();


        public MainPage()
        {
            InitializeComponent();
            BackgroundColor = Color.White;
            clnt.GetWeather();
            clnt.GetGBP();
            BindingContext = new BindingVM(this);
            AppVer.Text = Xamarin.Essentials.VersionTracking.CurrentVersion;

        }

        public void FlightButton_Clicked(object sender, EventArgs e) { if (!flighttimer.IsRunning) { flighttimer.Start(); } }
        public void SleepButton_Clicked(object sender, EventArgs e) { if (!sleeptimer.IsRunning) { sleeptimer.Start(); } }
       
        private void CurrencyEUR_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(clnt.varsyr == "Offline") { CurrencyEUR.Text = clnt.varsyr; }
            try { CurrencyGBP.Text = "£" + Math.Round(Convert.ToDouble(CurrencyEUR.Text) * Convert.ToDouble(clnt.varsyr), 2).ToString("F2"); } catch (Exception) { }
        }      
    }
}
