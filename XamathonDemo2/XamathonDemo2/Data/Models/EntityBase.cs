using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace XamathonDemo2.Data.Models
{
	public class EntityBase
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

        [Version]
        public string Version { get; set; }
	}
}

