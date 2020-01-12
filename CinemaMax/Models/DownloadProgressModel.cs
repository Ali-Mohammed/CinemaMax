using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMax.Models
{
    public class DownloadProgressModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Double Percent { get; set; }
        public long CompletedLength { get; internal set; }
        public string GID { get; internal set; }
        public string Status { get; internal set; }
        public long TotalLength { get; internal set; }
        public decimal DownloadSpeed { get; internal set; }
        public string Dir { get; internal set; }
        public string DownloadSpeedHuman { get; internal set; }
        public string downloadSizeHuman { get; internal set; }
        public string StartDownloadAt { get; internal set; }
        public string downloadStatus { get; internal set; }
        public Uri logo { get; internal set; }
    }
}
