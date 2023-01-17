using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ireland2022
{
    public class APIClient
    {
        HttpClient _httpClient;
        ReqVars vars = new ReqVars();
        Weather intense = new Weather();

        public string varsyr { get; set; } = "Offline"; 
        public double intcarb { get; set; }

        public APIClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ReqVars> GetGBP()
        {

            Uri uri = new Uri("https://v6.exchangerate-api.com/v6/13190f03d445d8a2357ad591/pair/EUR/GBP");
            try
            {
                HttpResponseMessage rs = await _httpClient.GetAsync(uri);
                string rsStr = await rs.Content.ReadAsStringAsync();
                vars = JsonConvert.DeserializeObject<ReqVars>(rsStr);
                varsyr = Convert.ToString(vars.conversion_rate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return vars;
        }

        public async Task<Weather> GetWeather()
        {

            Uri uri = new Uri("https://api.open-meteo.com/v1/forecast?latitude=53.33&longitude=-6.25&current_weather=true");
            try
            {
                HttpResponseMessage rs = await _httpClient.GetAsync(uri);
                string rsStr = await rs.Content.ReadAsStringAsync();
                intense = JsonConvert.DeserializeObject<Weather>(rsStr);
                intcarb = intense.current_weather.temperature;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return intense;
        }


    }
}
