using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace dotnet_code_challenge.Models.RaceFeeds
{
    [XmlRoot("meeting")]
    public class CaulfieldRaceFeed
    {
        [XmlElement("MeetingType")]
        public string MeetingType { get; set; }

        [XmlElement("track")]
        public CaulfieldTrack Track { get; set; }

        [XmlArray("races")]
        [XmlArrayItem("race")]
        public CaulfieldRace[] Races { get; set; }
    }

    public class CaulfieldTrack
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
    
    public class CaulfieldRace
    {
        [XmlArray("horses")]
        [XmlArrayItem("horse")]
        public CaulfieldHorse[] Horses { get; set; }

        [XmlArray("prices")]
        [XmlArrayItem("price")]
        public CaulfieldPrice[] Prices { get; set; }
    }

    public class CaulfieldHorse
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("number")]
        public int Number { get; set; }
    }

    public class CaulfieldPrice
    {
        [XmlArray("horses")]
        [XmlArrayItem("horse")]
        public CaulfieldHorsePrice[] HorsePrices { get; set; }
    }

    public class CaulfieldHorsePrice
    {
        [XmlAttribute("number")]
        public int Number { get; set; }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
