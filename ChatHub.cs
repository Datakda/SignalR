using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalR
{
    public class ChatHub : Hub
    {

        public async Task Send(string message)
        {

            MinMax rnd = new MinMax();

            WebRequest GETrequest = WebRequest.Create("http://185.195.26.249:7777/GetPost");
            WebResponse GETresponse = GETrequest.GetResponse();
            using (Stream stream = GETresponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    line = reader.ReadLine();

                    rnd = JsonSerializer.Deserialize<MinMax>(line); ;

                }
            }
            GETresponse.Close();
            await this.Clients.All.SendAsync("Send", $"Min = {rnd.Min}, Max = {rnd.Max}");
            
        }

      
    }
}
