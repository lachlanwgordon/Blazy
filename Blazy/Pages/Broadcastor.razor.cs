﻿using Blazor.Extensions;
using Blazy.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazy.Pages
{
    public class BoadcastorBase : ComponentBase
    {
        public string Log = "Hello Blazor";
        public string Message = "Hello Blazor";
        public List<String> Messages = new List<string>();
        HubConnection Connection;
        [Inject] private HttpClient Http { get; set; }
        [Inject] private HubConnectionBuilder HubConnectionBuilder { get; set; }
        public string BaseUrl { get; set; } = "https://blazort.azurewebsites.net/api";

        protected async override Task OnInitAsync()
        {
            var opt = new HttpConnectionOptions();
            Http.DefaultRequestHeaders.Add("Accept", "application/json");
            Http.DefaultRequestHeaders.Add("Accept", "text/plain");
            Http.DefaultRequestHeaders.Add("Accept", "");
            Connection = HubConnectionBuilder.WithUrl(BaseUrl).Build();
           
            Connection.On<Message>("newMessage", OnBroadCastMessage);
            await Connection.StartAsync();

            Log += "\nstart signalr Blazor";
            Messages.Add("Welcom to chat!, say something");
        }

        public Task OnBroadCastMessage(Message message)
        {
            Log += $"\n received {message}";
            Log += $"\n received {message.sender} {message.text}";
            Messages.Add($"{message.sender}: {message.text}");
            StateHasChanged();
            return Task.CompletedTask;
        }

        public async Task KeyUp(UIKeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SendMessage();
            }
        }

        public async Task SendMessage()
        {
            Log += $"\n send Blazy Client hellooo";
            StateHasChanged();
            //await Connection.InvokeAsync("SendMessage", "Blazy Client", Message);
            var mes = new Message { sender = "Admin Page", text = Message };
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(mes);
            await Http.PostAsync($"{BaseUrl}/messages", new StringContent(content, Encoding.UTF8, "application/json"));

            Log += $"\n sent content";

        }
    }
}
