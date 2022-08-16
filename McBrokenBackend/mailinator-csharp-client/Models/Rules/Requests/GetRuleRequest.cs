﻿using Newtonsoft.Json;

namespace mailinator_csharp_client.Models.Rules.Requests
{
    public class GetRuleRequest
    {
        /// <summary>
        /// This must be the Domain name or the Domain id
        /// </summary>
        [JsonProperty("domain_id")]
        public string DomainId { get; set; }

        /// <summary>
        /// This must be the rule name or the Rule id
        /// </summary>
        [JsonProperty("rule_id")]
        public string RuleId { get; set; }
    }
}