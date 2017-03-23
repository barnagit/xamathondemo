using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
	public class Movie
	{
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "director")]
        public string Director { get; set; }

        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }

        [JsonProperty(PropertyName = "releasedate")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Done { get; set; }

        [Version]
        public string Version { get; set; }
	}
}

