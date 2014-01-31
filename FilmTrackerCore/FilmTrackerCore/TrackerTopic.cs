using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmTrackerCore
{
    class TrackerTopic
    {
        public string SourceUri { get; set; }
        public string MagnetLink { get; set; }
        public string TorrentFile { get; set; }
        public string Size { get; set; }
        public string TopicName { get; set; }

        public TrackerTopic(string name, string uri, string torfile, string size, string magnet)
        {
            TopicName = name;
            SourceUri = uri;
            MagnetLink = magnet;
            TorrentFile = torfile;
            Size = size;
        }
    }
}
