using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
	public class User
	{
		[JsonProperty(PropertyName = "id")]
		public String Id { get; set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [Version]
        public string Version { get; set; }
	}
}