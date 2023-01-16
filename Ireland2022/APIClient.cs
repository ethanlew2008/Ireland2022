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
        CarbonIntensity intense = new CarbonIntensity();

        



        public string varsyr { get; set; } = "Offline"; 
        public int intcarb { get; set; }

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

        public async Task<CarbonIntensity> GetCarbon()
        {

            Uri uri = new Uri("https://api.carbonintensity.org.uk/intensity");
            try
            {
                HttpResponseMessage rs = await _httpClient.GetAsync(uri);
                string rsStr = await rs.Content.ReadAsStringAsync();
                intense = JsonConvert.DeserializeObject<CarbonIntensity>(rsStr);
                intcarb = intense.data[0].intensity.actual;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return intense;
        }


    }
}
