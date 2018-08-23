using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dotnet_code_challenge.Models.RaceFeeds
{
    public class RawRaceFeed : IDisposable
    {
        public string FileName { get; set; }

        public MemoryStream FeedStream { get; set; }

        public void Dispose()
        {
            if (FeedStream != null)
            {
                FeedStream.Dispose();
            }
        }
    }
}
