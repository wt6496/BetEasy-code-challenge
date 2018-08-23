using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge.Models
{
    public class HorseRace
    {
        public string Track { get; set; }

        public IList<Horse> Horses { get; set; }
    }
}
