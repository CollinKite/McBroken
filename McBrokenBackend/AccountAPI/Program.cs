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
            Email email = new();

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


            app.MapGet("/authAccount", async (HttpContext httpContext) =>
            {
                //Read from file
                string prefix = File.ReadAllLines("account.txt")[0];
                email.email = "mcbroken" + prefix;

                //Take Prefix and Create account. returns tuple (bool, JWTtoken)
                var status = await InitialAccountCreate(email.email);

                //If account was created, get confirm link
                if (status)
                {
                    //update email list
                    string newNum = (int.Parse(prefix) + 1).ToString();
                    File.WriteAllText("account.txt", newNum);

                    email.GetInboxAsync().Wait();
                    email.VerifyEmail().Wait();
                    email.WipeEmail();
                    return email.VerifyKey;

                    
                }
                else
                {
                    throw new Exception("Error with Creating Account");

                }


                
            })
            .WithName("auth");

            app.MapGet("/loginToken", async (HttpContext httpContext) =>
            {
                if (email.email == null)
                {
                    throw new Exception("No email found");
                }
                else
                {
                    email.GetLoginToken().Wait();
                    email.VerifyEmail().Wait();
                    email.WipeEmail().Wait();
                    return email.VerifyKey;
                }
            })
            .WithName("login");

            app.Run();
        }

        private static async Task<bool> InitialAccountCreate(string email)
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
                    return true;
                    
                  
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}