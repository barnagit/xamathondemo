using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
	public class Rating
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }
		
		[JsonProperty(PropertyName = "movieid")]
        public string MovieId { get; set; }
		
		[JsonProperty(PropertyName = "value")]
        public int Value { get; set; }

        [Version]
        public string Version { get; set; }
	}
}