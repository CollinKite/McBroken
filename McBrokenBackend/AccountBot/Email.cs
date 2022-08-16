using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Responses;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace AccountBot
{
    public class Email
    {
        public Email(string email) => this.email = email;


        public string email { get; set; }
        public string EmailId { get; set; }
        public string VerifyKey { get; set; }

        MailinatorClient mailinatorClient = new MailinatorClient("e58fe65ca13548a39d178d65b4e374ea");





        public async Task GetInboxAsync()
        {
            FetchInboxRequest fetchInboxRequest = new FetchInboxRequest()
            {
                Domain = "mail.bigmac.social",
                Inbox = email,
                Skip = 0,
                Limit = 20,
                Sort = Sort.asc
            };
            FetchInboxResponse fetchInboxResponse = await mailinatorClient.MessagesClient.FetchInboxAsync(fetchInboxRequest);
            EmailId = fetchInboxResponse.Messages[0].Id;
        }
        
        public async Task VerifyEmail()
        {
            FetchMessageLinksRequest fetchLinksRequest = new FetchMessageLinksRequest()
            {
                Domain = "mail.bigmac.social",
                Inbox = email,
                MessageId = EmailId
            };
            FetchMessageLinksResponse fetchLinksResponse = await mailinatorClient.MessagesClient.FetchMessageLinksAsync(fetchLinksRequest);
            if(fetchLinksResponse.Links[2] == null)
            {
                throw new Exception("Getting Message Failed");
            }
            Regex rx = new Regex(@"(?<=Fml=)(.*?)(?===)");
            MatchCollection matches = rx.Matches(fetchLinksResponse.Links[2]);
            VerifyKey = matches[0].Value;
            if(VerifyKey == null)
            {
                throw new Exception("Failed Getting Email Key from Email");
            }

        }

       
    }
}
