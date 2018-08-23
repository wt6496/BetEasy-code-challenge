using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge.Models.RaceFeeds
{
    public class WolferhamptonRaceFeed
    {
        [JsonProperty("RawData")]
        public WolferhamptonRawData RawData { get; set; }
    }

    public class WolferhamptonRawData
    {
        [JsonProperty("Tags")]
        public WolferhamptonRawDataTags Tags { get; set; }

        [JsonProperty("Markets")]
        public WolferhamptonRawDataMarket[] Markets { get; set; }
    }

    public class WolferhamptonRawDataTags
    {
        [JsonProperty("TrackCode")]
        public string Track { get; set; }

        [JsonProperty("Sport")]
        public string Sport { get; set; }
    }

    public class WolferhamptonRawDataMarket
    {
        [JsonProperty("Selections")]
        public WolferhamptonRawDataMarketSelection[] Selections { get; set; }
    }

    public class WolferhamptonRawDataMarketSelection
    {
        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("Tags")]
        public WolferhamptonRawDataMarketSelectionTags Tags { get; set; }
    }

    public class WolferhamptonRawDataMarketSelectionTags
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
