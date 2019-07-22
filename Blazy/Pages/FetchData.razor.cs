using BlazyDomain.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazy.Pages
{
    public class FetchDataBase : ComponentBase
    {
        public List<WeatherForecast> Forecasts;
        [Inject] private HttpClient Http { get; set; }

        protected override async Task OnInitAsync()
        {
            //Forecasts = await Http.GetJsonAsync<List<WeatherForecast>>("sample-data/weather.json");
            var url = "http://localhost:7071/api/weather";
            Debug.WriteLine("Gettings data from {url}");
            Forecasts = await Http.GetJsonAsync<List<WeatherForecast>>(url);

        }
    }
}
