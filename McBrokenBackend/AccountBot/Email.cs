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
using System.Collections;

namespace AccountBot
{
    public class Email
    {
        public Email(string email) => this.email = email;
        public Email() {;}


        public string email { get; set; }
        public string EmailId { get; set; }
        public string VerifyKey { get; set; }

        MailinatorClient mailinatorClient = new MailinatorClient("86d349d0d99b4bca8e37e2b86fbc76dd");





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
            bool waitingForEmail = true;
            while(waitingForEmail)
            {
                FetchInboxResponse fetchInboxResponse = await mailinatorClient.MessagesClient.FetchInboxAsync(fetchInboxRequest);
                if(fetchInboxResponse.Messages.Count != 0)
                {
                    EmailId = fetchInboxResponse.Messages[0].Id;
                    break;
                }
                
            }
        }

        public async Task GetLoginToken()
        {
            FetchInboxRequest fetchInboxRequest = new FetchInboxRequest()
            {
                Domain = "mail.bigmac.social",
                Inbox = email,
                Skip = 0,
                Limit = 20,
                Sort = Sort.asc
            };
            bool waitingForEmail = true;
            while (waitingForEmail)
            {
                FetchInboxResponse fetchInboxResponse = await mailinatorClient.MessagesClient.FetchInboxAsync(fetchInboxRequest);
                if (fetchInboxResponse.Messages.Count != 0)
                {
                    waitingForEmail = false;
                    for (int i = 0; i < fetchInboxResponse.Messages.Count; i++)
                    {
                        if (fetchInboxResponse.Messages[i].Subject == "Use this email to allow log in on a new device")
                        {
                            EmailId = fetchInboxResponse.Messages[0].Id;
                            break;
                        }
                    }
                }

            }
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
            for(int i = 0; i < fetchLinksResponse.Links.Count; i++)
            {
                Regex rx = new Regex(@"(?<=Fml=)(.*?)(?===)");
                MatchCollection matches = rx.Matches(fetchLinksResponse.Links[i]);
                if(matches.Count != 0)
                {
                    VerifyKey = matches[0].Value;
                    return;
                }
            }
            throw new Exception("Getting Message Failed");
        }

        public async Task WipeEmail()
        {
            DeleteAllDomainMessagesRequest deleteAllDomainMessagesRequest = new DeleteAllDomainMessagesRequest()
            {
                Domain = "mail.bigmac.social"
            };
            DeleteAllDomainMessagesResponse deleteAllDomainMessagesResponse =
                await mailinatorClient.MessagesClient.DeleteAllDomainMessagesAsync(deleteAllDomainMessagesRequest);
        }

       
    }
}
