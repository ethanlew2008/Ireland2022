using Portugal_2023;
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
using System.Threading;

namespace Ireland2022
{
    public class BindingVM : BindableBase
    {
        public static MainPage mainPage;
        public BindingVM(MainPage mpage) 
        {
            HomeThread();
            SleepThread();
            FlightThread();
            mainPage = mpage;
        }     
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChange("Time");
                }
            }
        }
        private static string time;

        public string Dark
        {
            get { return dark; }
            set
            {
                if (dark != value)
                {
                    dark = value;
                    OnPropertyChange("Dark");
                }
            }
        }
        private static string dark;

        public string Wifi
        {
            get { return wifi; }
            set
            {
                if (wifi != value)
                {
                    wifi = value;
                    OnPropertyChange("Wifi");
                }
            }
        }
        private static string wifi;

        public string Data
        {
            get { return data; }
            set
            {
                if (data != value)
                {
                    data = value;
                    OnPropertyChange("Data");
                }
            }
        }
        private static string data;

        public string Wetr
        {
            get { return wetr; }
            set
            {
                if (wetr != value)
                {
                    wetr = value;
                    OnPropertyChange("Wetr");
                }
            }
        }
        private static string wetr;



        private void HomeThread()
        {
            // Start a new thread that updates the Time property every second
            new Thread(() =>
            {
                while (true)
                {
                   
                    string dt = "";
                    if (DateTime.Now.Month > 4 && DateTime.Now.Month < 11) { dt = DateTime.UtcNow.AddHours(1).ToString("HH:mm"); }
                    else { dt = DateTime.UtcNow.ToString("HH:mm"); }
                    Time = "DUB:" + dt;
                    if (AppInfo.RequestedTheme == AppTheme.Dark) { Dark = "DarkSlateGray"; } else { Dark = "White"; }
                    if (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)) { Wifi = "Wifi On"; } else { Wifi = "Wifi Off"; }
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet && !Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)) { Data = "Data On"; } else { Data = "Data Off"; }
                    if (Wetr == "" || Wetr == "DUB: 0℃" || Wetr == null) 
                    { 
                        Wetr = "DUB: " + mainPage.clnt.intcarb + "℃"; 
                    }
                    
                }
            }).Start();
        }

        public string Sleep
        {
            get { return sleep; }
            set
            {
                if (sleep != value)
                {
                    sleep = value;
                    OnPropertyChange("Sleep");
                }
            }
        }
        private static string sleep;

        public string Sleepbr
        {
            get { return sleepbr; }
            set
            {
                if (sleepbr != value)
                {
                    sleepbr = value;
                    OnPropertyChange("Sleepbr");
                }
            }
        }
        private static string sleepbr;

        public void SleepThread()
        {
            // Start a new thread that updates the Time property every second
          
           

            new Thread(() =>
            {
                while (true)
                {
                    if (mainPage.sleeptimer.IsRunning)
                    {
                      
                        Sleepbr = "" + Convert.ToInt32(mainPage.sleeptimer.Elapsed.TotalMinutes * 15) + " Breaths";
                        Sleep = string.Format("{0}:{1:00}", (int)(TimeSpan.FromMinutes(mainPage.sleeptimer.Elapsed.TotalMinutes)).TotalHours, (TimeSpan.FromMinutes(mainPage.sleeptimer.Elapsed.TotalMinutes)).Minutes);
                    }
                    else { Sleepbr = "Start Sleep"; Sleep = "Start Sleep"; }
                }
            }).Start();
        }

        public void FlightThread()
        {
            // Start a new thread that updates the Time property every second
            new Thread(() =>
            {
                while (true)
                {
                    if (mainPage.flighttimer.IsRunning)
                    {
                        FlightP = "" + Convert.ToInt32(100 - (mainPage.flighttimer.Elapsed.TotalMinutes / 85) * 100) + "% left";
                        FlightT = Convert.ToString(Convert.ToInt32(85 - mainPage.flighttimer.Elapsed.TotalMinutes)) + " Mins";
                    }
                    else
                    {
                        FlightT = "Start Flight";
                        FlightP = "Start Flight";
                    }

                }
            }).Start();
        }

        public string FlightT
        {
            get { return flightT; }
            set
            {
                if (flightT != value)
                {
                    flightT = value;
                    OnPropertyChange("FlightT");
                }
            }
        }
        private static string flightT;

        public string FlightP
        {
            get { return flightP; }
            set
            {
                if (flightP != value)
                {
                    flightP = value;
                    OnPropertyChange("FlightP");
                }
            }
        }
        private static string flightP;
    }
}

