using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
	public class User : EntityBase
	{
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

	}
}