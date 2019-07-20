using Blazor.Extensions;
using BlazyDomain.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazy.Pages
{
    public class TwitchPlayerBase  : ComponentBase
    {
        public string Message = "This is the Twitch Player. You can add it to OBS to display chat messages. This message will disappear and be replaced with anything entered on the chat page.";
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
            Message = arg.Text;
            StateHasChanged();
            await Task.Delay(3000);
            Message = string.Empty;
            StateHasChanged();
        }
    }
}
