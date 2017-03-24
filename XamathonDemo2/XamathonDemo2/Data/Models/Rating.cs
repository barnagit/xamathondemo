using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
	public class Rating : EntityBase
	{
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }
		
		[JsonProperty(PropertyName = "movieid")]
        public string MovieId { get; set; }
		
		[JsonProperty(PropertyName = "value")]
        public int OldValue { get; set; }

        [JsonIgnore]
        public int Value { get; set; }

        [JsonIgnore]
        public bool ValueChanged
        {
            get { return this.OldValue != this.Value; }
        }
	}
}