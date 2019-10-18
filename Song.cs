using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hTunes
{
    class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public string AlbumImage { get; set; }
    }
}
