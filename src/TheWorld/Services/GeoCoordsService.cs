﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        private ILogger<GeoCoordsService> logger;
        private IConfigurationRoot config;

        public GeoCoordsService(ILogger<GeoCoordsService> logger, IConfigurationRoot config)
        {
            this.logger = logger;
            this.config = config;
        }

        public async Task<GeoCoordsAsyncResult> GetCoordsAsync(string name)
        {
            var result = new GeoCoordsAsyncResult()
            {
                Success = false,
                Message = "Failed to get coordinates"
            };

            var apiKey = config["Keys:BingKey"];
            var encodedName = WebUtility.UrlDecode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            // Fragile, might change if the Bing API changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                var confidence = (string) resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{name}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double) coords[0];
                    result.Longitude = (double) coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }

            }

            return result;
        }
    }
}
