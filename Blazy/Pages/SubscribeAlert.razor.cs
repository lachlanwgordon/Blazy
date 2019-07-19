using Blazor.Extensions;
using Blazy.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazy.Pages
{
    public class SubscribeAlertBase  : ComponentBase
    {
  


        public string Message = "Hello Blazor";
        HubConnection Connection;
        [Inject] private HttpClient Http { get; set; }
        [Inject] private HubConnectionBuilder HubConnectionBuilder { get; set; }
        public string BaseUrl { get; set; } = "https://blazort.azurewebsites.net/api";

        protected override async Task OnInitAsync()
        {


            var opt = new HttpConnectionOptions();
            Http.DefaultRequestHeaders.Add("Accept", "application/json");
            Http.DefaultRequestHeaders.Add("Accept", "text/plain");
            Http.DefaultRequestHeaders.Add("Accept", "");
            Connection = HubConnectionBuilder.WithUrl(BaseUrl).Build();

            Connection.On<Message>("newMessage", OnBroadCastMessage);
            await Connection.StartAsync();


        }

        private async Task OnBroadCastMessage(Message arg)
        {
            Message = arg.text;
            StateHasChanged();
            await Task.Delay(3000);
            Message = string.Empty;
            StateHasChanged();
        }
    }
}
