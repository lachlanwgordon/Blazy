using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace BlazyFunctions
{
    public static class Function1
    {

        ///This function is called from an page that wants to subscribe to this hub
        ///Once subscribed, the client will receive all messages send to the SendMessage function
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "chat")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        /// <summary>
        /// This function called when ever a client wants to send a message to signalR. The signalR hub will then broadcast to all subscribed clients.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="signalRMessages"></param>
        /// <returns></returns>
        [FunctionName("messages")]
        public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] object message,
            [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] { message }
                });
        }
    }
}
