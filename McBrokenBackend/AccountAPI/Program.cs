using Newtonsoft.Json.Linq;

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


            app.MapGet("/createAccount", (HttpContext httpContext) =>
            {
                string prefix = File.ReadAllLines("account.txt")[0];
                var status = InitialAccountCreate(prefix);
                //Console.WriteLine(status.Result.Item1);
                if(status.Result.Item1)
                {
                    //open file write stream
                    string newNum = (int.Parse(prefix) + 1).ToString();
                    File.WriteAllText("account.txt", newNum);
                }
                //Console.WriteLine(status.Result.Item2);


                return status.Result.Item2;
            })
            .WithName("McBroken");

            app.Run();
        }

        private static async Task<(bool, string)> InitialAccountCreate(string email)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://127.0.0.1:5000/createAccount/test"+email),
            };
            using (var response = await client.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    string body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + body);
                    string token = (string)JObject.Parse(body)["token"];
                    if(token != null)
                    {
                        return (true, token);
                    }
                    else
                    {
                        throw new Exception("Null Reponse From Account Creations API");
                    }

                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return (false, "");
                }
            }
        }
    }
}