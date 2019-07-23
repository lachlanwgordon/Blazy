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
            var url = "https://blazort.azurewebsites.net/api/weather";
            Forecasts = await Http.GetJsonAsync<List<WeatherForecast>>(url);
        }
    }
}
