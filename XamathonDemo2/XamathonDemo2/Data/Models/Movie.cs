using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
    public class Movie : EntityBase
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "director")]
        public string Director { get; set; }

        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }

        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "releasedate")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "complete")]
        protected string Complete { get; set; }

        [JsonIgnore]
        public bool Done
        {
            get
            {
                return (Complete == "1");
            }
            set { Complete = value ? "1" : "0"; }
        }
    }
}