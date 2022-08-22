using AccountBot;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace AccountAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapGet("/createAccount", async (HttpContext httpContext) =>
            {
                //Read from file
                string prefix = File.ReadAllLines("account.txt")[0];
                Email email = new("final" + prefix);
                //email.WipeEmail().Wait();

                //Take Prefix and Create account. returns tuple (bool, JWTtoken)
                var status = await InitialAccountCreate(email.email);

                //If account was created, return JWT token and verify the account
                if (status.Item1)
                {
                    //open file write stream
                    string newNum = (int.Parse(prefix) + 1).ToString();
                    File.WriteAllText("account.txt", newNum);
                    string JWT = status.Item2;
                    string id = status.Item3;
                    email.GetInboxAsync().Wait();
                    email.VerifyEmail().Wait();
                    if(email.VerifyKey == null)
                    {
                        throw new Exception("Error Getting Key From Email Message");
                    }
                    if(ConfirmAccount(email.VerifyKey, JWT, id).Result)
                    {
                        return "Account Created";
                    }
                    else
                    {
                        return "Error Confirming Account";
                    }
                    //ConfirmAccount(status.Result.Item2, );

                    
                }
                else
                {
                    throw new Exception("Error with Creating Account");

                }
                //Console.WriteLine(status.Result.Item2);


                
            })
            .WithName("McBroken");

            app.Run();
        }

        private static async Task<(bool, string, string)> InitialAccountCreate(string email)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://127.0.0.1:5000/createAccount/"+email),
            };
            using (var response = await client.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    string body = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("Response: " + body);
                    string token = (string)JObject.Parse(body)["token"];
                    string id = (string)JObject.Parse(body)["id"];
                    if(token != "Error")
                    {
                        return (true, token, id);
                    }
                    else
                    {
                        throw new Exception("Null Reponse From Account Creations API");
                    }

                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return (false, "", "");
                }
            }
        }

        private static async Task<bool> ConfirmAccount(string ConfirmToken, string JWTtoken, string id)
        {
            var clientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://us-prod.api.mcd.com/exp/v1/customer/activateandsignin"),
                Headers =
                    {
                        { "Host", "us-prod.api.mcd.com" },
                        { "Mcd-Clientid", "8cGckR5wPgQnFBc9deVhJ2vT94WhMBRL" },
                        { "Authorization", "Bearer " + JWTtoken },
                        { "Cache-Control", "true" },
                        { "Accept-Charset", "UTF-8" },
                        { "User-Agent", "MCDSDK/23.0.15 (Android; 31; en-US) GMA/7.5.0" },
                        { "Accept-Language", "en-US" },
                        { "Mcd-Sourceapp", "GMA" },
                        { "Mcd-Uuid", "748acfab-e896-43d0-bafe-f584747c06e0" },
                        { "Mcd-Marketid", "US" },
                        { "Newrelic", "eyJ2IjpbMCwyXSwiZCI6eyJkLnR5IjoiTW9iaWxlIiwiZC5hYyI6IjczNDA1NiIsImQuYXAiOiI0MzY5OTg0NjAiLCJkLnRyIjoiNGZjNWFjZWJiOTQ4NDU0MzlmOGJlMjhiZWMxNmVmMTEiLCJkLmlkIjoiMjdiYWZlMDVkMTZjNDlhNyIsImQudGkiOjE2NTk2NTU2MjQ0NzZ9fQ==" },
                        { "Tracestate", "@nr=0-2-734056-436998460-27bafe05d16c49a7----1659655624476" },
                        { "Traceparent", "00-4fc5acebb94845439f8be28bec16ef11-27bafe05d16c49a7-00" },
                        { "X-Newrelic-Id", "UwUDUVNVGwcDUlhbDwUBVg==" },
                    },
                    Content = new StringContent("{\"activationLink\":\"" + ConfirmToken + "\",\"clientInfo\":{\"device\":{\"deviceUniqueId\":\"" + id + "\",\"os\":\"android\",\"osVersion\":\"12\"}}}")
                        {
                            Headers =
                                {
                                    ContentType = new MediaTypeHeaderValue("application/json")
                                }
                        }
            };
            using (var response = await client.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}