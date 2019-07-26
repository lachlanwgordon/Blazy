# Blazy
Experimenting with Blazor Client Side. This site is hosted using Azure blob storage static website. You can view it at https://blazort.z26.web.core.windows.net/

I've just added in SignalR using the Azure SignalR service and Azure Functions. The Admin page is now a basic SignalR Chat page and the Twitch Player shows each message for 2 seconds on a transparent background and then disappears.

So far it is basically just the File>New Blazor Template, with an Admin page to post/monitor messages and a TwitchPlayer page to display each message for 10 seconds on a transparent background.

If you want to try it in your twitch stream add a browser source with the url https://blazort.z26.web.core.windows.net/twitch

As this project matures I will be adding instructions on how to set up hosting on Azure using Static Website Blobs and setting up the SignalR service and Azure Functions.

Pull requests are welcome. This is a playground for anyone wanting to experiment with Client Side Blazor.

## Functions and table storage
The fetch data page now connects to an Azure Function which has weather data stored in Table storage. The data is still rubbish but it was a an excuse to connect a function to table storage.

# Coding standard for contributors.
Pretty lax for now but:
## New pages
* Pages should be named .razor extension e.g. `AboutPage.razor`
* Code for a page should be placed in a code behind file, not in the `@code{}` block.
* Code behind file should have the same name as the page with .razor.cs extension - This enables nesting in the solution explorer. e.g. `AboutPage.razor.cs`
* The class in code behind's name should match the name of the page with `Base` on the end e.g. `AboutPageBase`
* Code behind needs to inhertit from ComponentBase. e.g. `AboutPageBase : ComponentBase`
* The razor page inherits from the code behind by adding an `@inherits` to the top of the file. e.g. `@inherits AboutPageBase`.



# Dev Enviornment setup if you want to contribute or build something similar(this changes frequently, accurate as of 2019-07-20)
I recommend working on Blazor on Windows rather than Mac until things go stable. Installing .NetCore 3 breaks nuget for Xamarin Apps in the current version of VSMac so you'll have to constantly upgrade and downgrade if you have other projects on the go.

## Required installation
To work on this project you will need:
* .Net Core 3.0 Preview 6 https://dotnet.microsoft.com/download/dotnet-core/3.0
* Visual Studio 2019 16.2 Preview 4(Preview 3 works but 4 is more stable) https://visualstudio.microsoft.com/vs/preview/

## Aditional setup
You need to tell VS to use .Net Core 3 Tools -> Options -> Projects and Solutions -> .NET Core -> Use Previews of the .NET Core SDK. https://visualstudiomagazine.com/articles/2019/03/08/vs-2019-core-tip.aspx

## Optional installation
* Blazor extensions. Search for Blazor in the Extensions gallery in Visual Studio. This adds a template for new pages and new projects.
* Blazor command line templates. These let you create a new blazor project from the command line if you're hardcore and would rather use VIM or VSCode. To install run `dotnet new -i Microsoft.AspNetCore.Blazor.Templates::3.0.0-preview6.19307.2` more info at https://docs.microsoft.com/en-us/aspnet/core/blazor/get-started?view=aspnetcore-3.0&tabs=visual-studio

## Local config
### Blazor
The Blazor app currently point to the live instance of the functions projects which is deployed to my Azure account. If you want to point to your own hosted functions or run functions locally change the BaseUrl in Broadcastor.razor.cs, your local instance will probably be on "http://localhost:7071". For now, feel free to point your dev environment at my functions/signalR service but if costs get out of hand I will have to remove this option.

### Functions
To run Functions locally you will need to create your own instance of a SignalR service on Azure(Free Teir will do the job, more details down in the Azure config section).

Then create a file called local.settings.json in the root of your functions project. This file is not included in the repo as it contains sensitive keys. The contents will look something like:
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AzureSignalRConnectionString": "YOUR_SIGNALR_SERVICE_CONNECTION_STRING"
  },
  "Host": {
    "LocalHttpPort": 7071,
    "CORS": "http://localhost:8080,http://localhost:8001",
    "CORSCredentials": true
  }
}
```
With "YOUR_SIGNALR_SERVICE_CONNECTION_STRING" updated to match the connection string in your Azure SignalR service.


## Azure config

This project is hosted using three Azure services which are all free or very cheap(I'm currently paying 1.5c a day total). If you only want to contribute to the Blazor client you don't need Azure at all. 

If you want to contribute to or run local functions you will need to deploy your own SignalR service(FREE). 

If you want to deploy a full instance that is independant of mine you will also need to deploy your own Azure Functions(First 1,000,000 calls per month are free, and then cost nothing) and some storage to host the Blazor Static Website and hold your Functions.

### SignalR Service

Create a SignalR service on Azure, the free tier will be just fine unless you're using this for something big. Even the paid teir is pretty cheap give what it does.

 Once deployed, go to the Settings page and change the `Service Mode` to `Serverless`.
 
 In the keys page you can find your connections string, you'll need this later.
 
 ### Functions
 Create a new Functions App. 
 In the setup wizard make the following selections:
 * Use the same resource group you created your SignalR service in
 * Choose windows over linux(unless you can convinve me otherwise. it seems to make no diference with Functions but for most azure services windows is cheaper)
 * Chose consumption plan - this means you're billed a tiny fraction of a cent per call instead of several dollars. If you're very big, you like wasting money or you have a boss who likes to know a flat rate per month you might chose the other option.
 * If you've already created a storage account to host your own blazor app, reuse that. If you haven't, create a new one and reuse if you decide to host your own blazor app.
 * Open the Platform features tab.
 * Click on "Configuration" in the "General Settings area"
 * Add a "New application setting" (not a connection string - deal with it.)
 * Give the setting the name "AzureSignalRConnectionString"
 * The value is found in the Keys page in your SignalR service.
 * Back in the Platform features page open "CORS" in the API section.
 * Tick "Enable Access-Control-Allow-Credentials"
 * Add entries for anywhere you plan to host your Blazor app. For me I have "https://blazort.z26.web.core.windows.net" for production, "https://localhost:53580" for debuggin in VS and "https://localhost:8001" for hosting in XAMPP. I host in xampp if I want to test a local copy within the house. e.g. test it on my phone or TV without deploying to Azure.
 

 ### Storage for hosting Blazor app.
 
 * Create a storage account(or open the one you created when setting up functions)
 * Go to the `Staic Website` page in settings
 * click Enable
 * set the index document name and error document path to "index.html". Yep, sounds odd but it's what you do.
 * You now have  folder called $web, put all the files from your publish directory in here. I use the VSCode extension "Azure Storage" to do this but I really need to get devopsy.
 
 
 
 
 
 
