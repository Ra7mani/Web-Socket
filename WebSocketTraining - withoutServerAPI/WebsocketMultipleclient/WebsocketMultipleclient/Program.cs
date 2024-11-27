using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using WebsocketMultipleclient.TokenServer;
using WebSocketServerProject.Ext;
using WebSocketServerProject.Modeles.Domaine;

namespace WebsocketMultipleclient
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static string password, login;
        static HttpRequestMessage requestMessage;
        static string _cridential;
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Authentification");
            
            requestMessage.Content = JsonContent.Create(new Utilisateur(login, password));

            String token = await Sauthentifier(login, password);
            _cridential = JsonSerializer.Deserialize<Token>(@token, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }).Value;
        }

        static async Task Main(string[] args)
        {



            "Login :".Dump(ConsoleColor.Yellow);
            login = Console.ReadLine();
            "Password :".Dump(ConsoleColor.Yellow);
            password = Console.ReadLine();
            await RunAsync();
            var webSocket = new ClientWebSocket();
            webSocket.Options.SetRequestHeader("Authorization", _cridential);
            CancellationTokenSource cs = new CancellationTokenSource();
            await webSocket.ConnectAsync(new Uri(@"ws://localhost:5000/"), cs.Token);
            webSocket.State.ToString().Dump(ConsoleColor.Green);
        }

        static async Task<String> Sauthentifier(string login, string pwd)
        {
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBody);
            return responseBody;
        }
        //static async Task<Token> Sauthentifier(string login, string pwd)
        //{
        //    Token token = null;
        //    HttpResponseMessage response = await client.GetAsync(@"api/Authentification/");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        token = await response.Content.ReadFromJsonAsync<Token>();
        //    }
        //    return token;
        //}
    }
}
